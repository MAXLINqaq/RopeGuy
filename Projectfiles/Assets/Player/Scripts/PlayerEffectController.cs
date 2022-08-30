using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectController : MonoBehaviour
{
    public ParticleSystem jumpDust;
    private PlayerController playerController;
    private bool lastGroundCheckResult;
    // Start is called before the first frame update
    void Start()
    {
        playerController =GetComponent<PlayerController>();
        lastGroundCheckResult= true;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(lastGroundCheckResult!=playerController.onGround)
        {
            CreateEffectJumpDust();
        }
        lastGroundCheckResult = playerController.onGround;
    }

    private void CreateEffectJumpDust()
    {
        jumpDust.Play();
    }
}
