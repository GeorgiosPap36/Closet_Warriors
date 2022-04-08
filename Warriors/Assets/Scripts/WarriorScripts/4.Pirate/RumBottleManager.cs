using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumBottleManager : MonoBehaviour {

    private Rigidbody2D rb2d;
    private Animator anim;

    private GameObject parentz;
    public GameObject pirate;

    private float damageMult;

    void Start()
    {
        anim = GetComponent<Animator>();
        parentz = transform.parent.gameObject;
        pirate = parentz.transform.Find("Pirate(Clone)").gameObject;
        transform.localScale = new Vector3(pirate.transform.localScale.x, 1, 1);
        transform.localPosition = pirate.transform.localPosition + new Vector3(transform.localScale.x * 0.7f, 0.75f, 0);
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(7 * transform.localScale.x, 2);
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Wall" || col.transform.tag == "Platform")
        {
            StartCoroutine(Explode());
            GetComponent<AudioSource>().enabled = false;
        }
        else if (col.transform.tag == "Floor")
        {
            anim.SetTrigger("Explode");
            rb2d.velocity = new Vector2(0, -2);
            GetComponent<AudioSource>().enabled = false;
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(0.75f);
        anim.SetTrigger("Explode");
        rb2d.velocity = new Vector2(0, -2);
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
