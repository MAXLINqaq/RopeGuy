using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBoxController : Box
{
    public int brokeHp;
    public int destroyHp;
    private BoxCollider2D boxCollider2D;
    private bool wasBroken;

    // Start is called before the first frame update
    void Start()
    {
        BoxInit(brokeHp, destroyHp);
        boxCollider2D = GetComponent<BoxCollider2D>();
        wasBroken = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (BoxBroken() && !wasBroken)
        {
            boxCollider2D.offset = new Vector2(0, -0.65f);
            boxCollider2D.size = new Vector2(3.4f, 1.85f);
            GetComponent<SpriteRenderer>().sprite = brokenBox;
            PartNormal(1);
            wasBroken = true;
        }
        if (BoxDestroy())
        {
            PartNormal(2);
            Destroy(this.gameObject);
        }
        if (WasHit())
        {
            effcet.Play();
            HitEffectProcessed();
        }
    }
    private void PartNormal(int mode)
    {
        if (mode == 1)
        {
            for (int i = 0; i < parts.Length; i++)
            {
                GameObject newPart = Instantiate(parts[i]);
                newPart.transform.position = transform.position;
                newPart.GetComponent<Rigidbody2D>().AddForce(400 * new Vector2(Random.Range(-1, 1), Random.Range(0.5f, 1)));
            }
        }
        if (mode == 2)
        {
            for (int i = 0; i < parts.Length * 2; i++)
            {
                GameObject newPart = Instantiate(parts[i % 5]);
                newPart.transform.position = transform.position;
                newPart.GetComponent<Rigidbody2D>().AddForce(400 * new Vector2(Random.Range(-1, 1), Random.Range(0.5f, 1)));
            }
        }

    }

}
