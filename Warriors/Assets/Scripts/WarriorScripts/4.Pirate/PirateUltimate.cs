using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateUltimate : MonoBehaviour {

    [SerializeField] private GameObject pirate;

    private RumBottleManager rum;

    private Vector4 k;

    private float damageMult;

    void Start ()
    {
        rum = transform.parent.parent.GetComponent<RumBottleManager>();
        pirate = rum.pirate;
        k = new Vector4(0, 0, 0.1f, 70);
        DamageMult = transform.parent.parent.GetComponent<RumBottleManager>().DamageMult;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.name == "HitBox")
        {
            if (pirate.transform.tag != col.transform.parent.parent.parent.tag)
            {
                if (transform.position.x > col.transform.parent.parent.parent.position.x)
                {
                    k.x = -k.x;
                }
                k.w = k.w * DamageMult;
                col.transform.parent.parent.parent.gameObject.SendMessage("GetHit", k);
                col.transform.parent.parent.parent.gameObject.SendMessage("GetAttacker", int.Parse(pirate.transform.tag.Substring(pirate.transform.tag.Length - 1)) - 1);
            }
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
