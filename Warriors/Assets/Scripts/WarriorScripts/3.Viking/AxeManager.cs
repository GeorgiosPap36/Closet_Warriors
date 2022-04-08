using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeManager : MonoBehaviour
{

    private Rigidbody2D rb2d;

    private GameObject parentz;

    private GameObject viking;

    private Vector4 k;

    private float damageMult;

    void Start()
    {
        parentz = transform.parent.gameObject;
        viking = parentz.transform.Find("Viking(Clone)").gameObject;
        transform.localScale = new Vector3(viking.transform.localScale.x, 1, 1);
        transform.localPosition = viking.transform.localPosition + new Vector3(transform.localScale.x * 1, 0, 0);
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.right * transform.localScale.x * 10;
        k = new Vector4(7, 0, 0.3f, 70);
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.name == "HitBox")
        {
            if (viking.transform.tag != col.transform.parent.parent.parent.tag)
            {
                if (transform.position.x > col.transform.parent.parent.parent.position.x)
                {
                    k.x = -k.x;
                }
                k.w = k.w * DamageMult;
                col.transform.parent.parent.parent.gameObject.SendMessage("GetHit", k);
                col.transform.parent.parent.parent.gameObject.SendMessage("GetAttacker", int.Parse(viking.transform.tag.Substring(viking.transform.tag.Length - 1)) - 1);
                Destroy(gameObject);
            }
        }
        if (col.transform.tag == "Wall")
        {
            Destroy(gameObject);
        }
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
}