using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject gun;
    public GameObject bullet;
    
    void Start()
    {

    }

    
    void Update()
    {
        ChangeFace();
        Aim();
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }
    private void ChangeFace()
    {
        Vector3 obj = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 direction = Input.mousePosition - obj;
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void Aim()
    {
        Vector3 obj = Camera.main.WorldToScreenPoint(gun.transform.position);
        Vector2 direction = Input.mousePosition - obj;
        gun.transform.LookAt(direction);
        gun.transform.Rotate(0, -90, 0);
    }
    private void Shoot()
    {
         Vector3 obj = Camera.main.WorldToScreenPoint(gun.transform.position);
        Vector2 direction = Input.mousePosition - obj;
        gun.transform.LookAt(direction);
        gun.transform.Rotate(0, -90, 0);
        if (this.transform.localScale.x > 0 && direction.x > 0)
        {
            Instantiate(bullet, gun.transform.position, gun.transform.rotation);
        }
        else if (this.transform.localScale.x <= 0 && direction.x < 0)
        {
            Transform tempTransform;
            tempTransform= gun.transform;
            tempTransform.Rotate(0, 0, 180);
            Instantiate(bullet, tempTransform.position, tempTransform.rotation);

        }
    }

}
