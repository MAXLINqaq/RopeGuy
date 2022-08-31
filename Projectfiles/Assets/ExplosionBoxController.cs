using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBoxController : Box
{
    public int destroyHp;
    public float damageRadius;



    // Start is called before the first frame update
    void Start()
    {
        BoxInit(0, destroyHp);
    }

    // Update is called once per frame
    void Update()
    {
        if (BoxDestroy())
        {
            ExplosionDamage(this.transform.position, damageRadius);
            effcet.Play();
            PartExplode();
            Destroy(this.gameObject);
        }
    }
    private void PartExplode()
    {
        for (int i = 0; i < parts.Length * 2; i++)
        {
            GameObject newPart = Instantiate(parts[i % 5]);
            newPart.transform.position = transform.position;
            newPart.GetComponent<Rigidbody2D>().AddForce(1000 * new Vector2(Random.Range(-1, 1), Random.Range(0.5f, 1)));
        }
    }
    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            float dis = Vector3.Distance(hitCollider.gameObject.transform.position, transform.position);
            if (dis < radius / 3)
            {
                hitCollider.SendMessage("ExploreHitDistance1", null, SendMessageOptions.DontRequireReceiver);
            }
            else if (radius / 3 <= dis && dis <= radius * 2 / 3)
            {
                hitCollider.SendMessage("ExploreHitDistance2", null, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                hitCollider.SendMessage("ExploreHitDistance3", null, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
