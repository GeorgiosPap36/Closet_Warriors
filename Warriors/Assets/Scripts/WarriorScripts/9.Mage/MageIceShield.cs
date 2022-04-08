using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageIceShield : MonoBehaviour {

    private GameObject parentz;

    private float k;
    private float msSlow;

    private void Start()
    {
        parentz = transform.parent.Find("Mage(Clone)").gameObject;
        k = 20;
        msSlow = 0.75f;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.name == "HitBox")
        {
            col.transform.parent.parent.parent.gameObject.SendMessage("Damaged", k);
            col.transform.parent.parent.parent.gameObject.SendMessage("GetMS", msSlow);
            col.transform.parent.parent.parent.gameObject.SendMessage("GetAttacker", int.Parse(parentz.transform.tag.Substring(parentz.transform.tag.Length - 1)) - 1);
        }
        if (col.transform.name == "VikingAxe")
        {
            Destroy(col.gameObject);
        }
        if (col.transform.name == "RumDamageCollider")
        {
            Destroy(col.transform.parent.parent.gameObject);
        }
        else if (col.transform.name == "PirateRumBottle")
        {
            Destroy(gameObject);
        }
    }

}
