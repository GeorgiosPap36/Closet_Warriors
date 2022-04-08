using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageIceSpike : MonoBehaviour {

    private GameObject parentz;

    private PolygonCollider2D pC2D;
    private Rigidbody2D rb2d;

    private Vector4 k;

    private float speed;
    [SerializeField] private float maxSpeed;

	void Start ()
    {
        pC2D = GetComponent<PolygonCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        parentz = transform.parent.Find("Mage(Clone)").gameObject;
        k = new Vector4(1, 1, 0.3f, 60);
        maxSpeed = 10f;
        speed = maxSpeed;
        StartCoroutine(MoveBack());
	}
	
	void Update ()
    {
        rb2d.velocity = MoveSpike() * speed;
	}

    Vector2 MoveSpike()
    {
        if (Mathf.FloorToInt(transform.eulerAngles.z) == 90)
        {
            return new Vector2(0, -1);
        }
        else if (Mathf.FloorToInt(transform.eulerAngles.z) == 270)
        {
            return new Vector2(0, 1);
        }
        else if (Mathf.FloorToInt(transform.eulerAngles.z) == 0)
        {
            return new Vector2(-1, 0);
        }
        else if (Mathf.FloorToInt(transform.eulerAngles.z) == 180)
        {
             return new Vector2(1, 0);
        }
        return Vector2.zero;
    }

    IEnumerator MoveBack()
    {
        yield return new WaitForSeconds(0.95f);
        Destroy(gameObject, 3);
        speed = -maxSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.name == "HitBox")
        {
            pC2D.enabled = false;
            speed = -maxSpeed * 3/4;
            Destroy(gameObject, 3);
            col.transform.parent.parent.parent.gameObject.SendMessage("GetHit", k);
            col.transform.parent.parent.parent.gameObject.SendMessage("GetAttacker", int.Parse(parentz.transform.tag.Substring(parentz.transform.tag.Length - 1)) - 1);
        }
    }


}
