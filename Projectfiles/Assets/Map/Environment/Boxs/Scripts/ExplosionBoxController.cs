using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBoxController : Box
{
    public int brokeHp;
    public int destroyHp;
    public float damageRadius;
    public ParticleSystem hitEffcet;

    private bool wasBroken;
    private BoxCollider2D boxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        BoxInit(brokeHp, destroyHp);

    }

    // Update is called once per frame
    void Update()
    {
        if (BoxBroken() && !wasBroken)
        {
            effcet.Play();
            GetComponent<SpriteRenderer>().sprite = brokenBox;
            boxCollider2D.offset = new Vector2(0, -0.22f);
            boxCollider2D.size = new Vector2(1.05f, 0.53f);
            ExplosionDamage(this.transform.position, damageRadius);
            PartExplode();
            wasBroken = true;
        }
        if (BoxDestroy())
        {
            PartExplode();
            Destroy(this.gameObject);
        }
        if (WasHit())
        {
            hitEffcet.Play();
            HitEffectProcessed();
        }
    }

    private void PartExplode()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            GameObject newPart = Instantiate(parts[i]);
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
