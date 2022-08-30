using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
   public float force;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        force = 10;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * force;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        
        if (coll.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
        if (coll.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
        if (coll.gameObject.tag == "Box")
        {
            Destroy(this.gameObject);
            //coll.attachedRigidbody.AddForce(transform.right * 600);
        }
        if (coll.gameObject.tag == "Rope")
        {
            //coll.attachedRigidbody.AddForce(transform.right * 600);
            //Destroy(coll.transform.parent.gameObject, 5);
            //Destroy(coll.gameObject);
        }

    }
}
