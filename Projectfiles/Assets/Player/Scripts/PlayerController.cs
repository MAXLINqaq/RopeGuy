using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //移动参数
    public float maxSpeed;//最大速度
    public float maxAcceleration;//最大地面加速
    public float maxAirAcceleration;//最大空中加速
    public float maxDeceleration;//最大地面减速
    public float maxAirDeceleration;//最大地面减速
    public float maxTurnSpeed;//最大转身速度
    public float maxAirTurnSpeed;//最大空中转身速度

    //跳跃参数
    public float jumpHeight;
    public float timeToJumpApex;
    public float defaultGravMultiplier;
    public float downwardMovementMultiplier;
    public float jumpCutOff;
    public float coyoteTime;
    public float jumpBuffer;


    //x轴移动
    float directionX;//x轴朝向
    float acceleration;//实时加速
    float deceleration;//实时减速
    float turnSpeed;//转向速度
    float maxSpeedChange;

    //y轴移动
    bool desiredJump;
    bool pressingJump;
    float gravMultiplier;
    bool currentlyJumping;
    float jumpSpeed;
    float coyoteTimeCounter;
    float jumpBufferCounter;

    Vector3 velocity;
    Rigidbody2D rb;
    public bool onGround;//地面检测
    Vector2 desiredVelocity;//期望水平速度


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        desiredVelocity = new Vector2(0, 0);
        directionX = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        OnMovement();
        OnJump();
        CoyoteTimer();
        Vector2 newGravity = new Vector2(0, (-2 * jumpHeight) / (timeToJumpApex * timeToJumpApex));
        rb.gravityScale = (newGravity.y / Physics2D.gravity.y) * gravMultiplier;
    }
    void FixedUpdate()
    {
        velocity = rb.velocity;
        Move();
        if (desiredJump)
        {
            DoAJump();
            rb.velocity = velocity;
            return;
        }
        if (rb.velocity.y > 0.01f)
        {
            if (pressingJump && currentlyJumping)
            {
                gravMultiplier = defaultGravMultiplier;
            }
            else
            {
                gravMultiplier = jumpCutOff;
            }

        }
        else if (rb.velocity.y < -0.01f) { gravMultiplier = downwardMovementMultiplier; }
        else { gravMultiplier = defaultGravMultiplier; }
        if (onGround)
        {
            currentlyJumping = false;
        }
    }


    private void OnMovement()
    {
        directionX = Input.GetAxisRaw("Horizontal");
        desiredVelocity = new Vector2(directionX, 0f) * maxSpeed;
    }

    private void OnJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            desiredJump = true;
            pressingJump = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            pressingJump = false;
        }
    }

    private void Move()
    {

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        deceleration = onGround ? maxDeceleration : maxAirDeceleration;
        turnSpeed = onGround ? maxTurnSpeed : maxAirTurnSpeed;

        if (directionX != 0)
        {
            if (Mathf.Sign(directionX) != Mathf.Sign(velocity.x))
            {
                maxSpeedChange = turnSpeed * Time.deltaTime;
            }
            else
            {
                maxSpeedChange = acceleration * Time.deltaTime;
            }
        }
        else
        {
            maxSpeedChange = deceleration * Time.deltaTime;
        }
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        rb.velocity = velocity;
    }
    private void DoAJump()
    {
        if (onGround || (coyoteTimeCounter > 0.03f && coyoteTimeCounter < coyoteTime))
        {
            desiredJump = false;
            coyoteTimeCounter = 0;
            jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * rb.gravityScale * jumpHeight);
            if (velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            else if (velocity.y < 0f)
            {
                jumpSpeed += Mathf.Abs(rb.velocity.y);
            }
            velocity.y += jumpSpeed;
            currentlyJumping = true;
        }
        desiredJump = false;
    }
    private void CoyoteTimer()
    {
        if (!currentlyJumping && !onGround)
        {
            coyoteTimeCounter += Time.deltaTime;
        }
        else
        {
            coyoteTimeCounter = 0;
        }
    }
    private void JumpBufferTimer()
    {
        if (desiredJump)
        {
            jumpBufferCounter += Time.deltaTime;
            if (jumpBufferCounter > jumpBuffer)
            {
                desiredJump = false;
                jumpBufferCounter = 0;
            }
        }
    }
    private void GroundCheck()
    {

        for (int i = 0; i < 3; i++)
        {
            onGround = Physics2D.Linecast(transform.position, transform.position + new Vector3(-0.5f + 0.5f * i, -0.54f, 0), 1 << LayerMask.NameToLayer("Ground"));
            if (onGround)break;
        }
    }

}
