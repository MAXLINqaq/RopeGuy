using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 verticalOffset;
    private Vector3 targetPostion;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(playerTransform.gameObject.GetComponent<Rigidbody2D>().velocity.x) < 0.01f)
        {
            if (playerTransform.localScale.x > 0.1f)
            {
                targetPostion = playerTransform.position + verticalOffset;
            }
            else
            {
                targetPostion = playerTransform.position - verticalOffset;
            }
        }
        else
        {
            targetPostion = playerTransform.position;
        }


    }
    private void FixedUpdate()
    {
        Vector3 temp = Vector3.Lerp(transform.position, targetPostion, 0.05f);
        transform.position = new Vector3(temp.x, temp.y, -10);
    }
}
