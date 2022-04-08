using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenManager : MonoBehaviour {

    private GameObject parentz;
    private Rigidbody2D rb2d;
    private Animator anim;

    private float damageMult;

    private Vector4 k;
   
    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        transform.position = parentz.transform.position + parentz.transform.localScale.x * new Vector3(0.5f, 0.1f, 0);
        transform.localScale = parentz.transform.localScale;
        rb2d.velocity = new Vector2(parentz.GetComponent<Rigidbody2D>().velocity.x, 0);
        k = new Vector4(1, 0, 0.1f, 30);
        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
    }

    public float DamageMult
    {
        get
        {
            return damageMult;
        }

        set
        {
            damageMult = value;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.name == "HitBox")
        {
            if (parentz != null)
            {
                if (parentz.transform.tag != col.transform.parent.parent.parent.tag)
                {
                    if (!col.transform.parent.parent.parent.GetComponent<Warrior>().cced)
                    {
                        if (parentz.transform.position.x > col.transform.parent.parent.parent.position.x)
                        {
                            k.x = -k.x;
                        }
                        k.w = k.w * DamageMult;
                        col.transform.parent.parent.parent.gameObject.SendMessage("GetHit", k);
                        parentz.GetComponent<Warrior>().SendMessage("GetEnergy", k.w);
                    }
                }
            }
            else
            {
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }
    }

    void PickParent(GameObject gO)
    {
        parentz = gO;
    }
}
