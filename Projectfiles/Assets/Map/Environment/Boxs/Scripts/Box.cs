using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Sprite brokenBox;
    

    public GameObject[] dropItems;
    //private float damageRadius;
    public GameObject[] parts;
    public ParticleSystem effcet;

    private bool wasHit;
    private int boxHp1;
    private int boxHp2;
    public int hitCount;
    private int DropMode;
    void Start()
    {
        hitCount = 0;
        //damageRadius = 6;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("BoxWasHit");
            Hit();
        }
    }
    public void BoxInit(int hp1, int hp2)
    {
        boxHp1 = hp1;
        boxHp2 = hp2;
    }

    public void Hit()
    {
        wasHit = true;
        hitCount++;

    }
    public bool BoxBroken()
    {
        if (hitCount >= boxHp1)
            return true;
        else
            return false;
    }
    public bool BoxDestroy()
    {
        if (hitCount >= boxHp2)
            return true;
        else
            return false;
    }
    public bool WasHit()
    {
        if (wasHit == true)
            return true;
        else
            return false;
    }
    public void HitEffectProcessed()
    {
        wasHit = false;
    }


    
    
    /*
    public void PartExplode(){
        for(int i=0;i<parts.Length;i++){
            GameObject newPart = Instantiate(parts[i]);
            newPart.transform.position = transform.position;
            newPart.GetComponent<Rigidbody2D>().AddForce(1000*new Vector2(Random.Range(-1,1),Random.Range(0.5f,1)));
        }
    }
     public void PartNormal(){
        for(int i=0;i<parts.Length;i++){
            GameObject newPart = Instantiate(parts[i]);
            newPart.transform.position = transform.position;
            newPart.GetComponent<Rigidbody2D>().AddForce(400*new Vector2(Random.Range(-1,1),Random.Range(0.5f,1)));
        }
    }
    */
    
    public void ExploreHitDistance1()
    {
        hitCount += 11;
    }
    public void ExploreHitDistance2()
    {
        hitCount += 6;
    }
    public void ExploreHitDistance3()
    {
        hitCount += 3;
    }
}
