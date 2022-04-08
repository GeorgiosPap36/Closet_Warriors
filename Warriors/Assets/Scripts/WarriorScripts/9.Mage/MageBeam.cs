using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBeam : MonoBehaviour
{
    private GameObject parentz;

    private Vector4 k;

    void Start()
    {
        parentz = transform.parent.parent.parent.Find("Mage(Clone)").gameObject;
        k = new Vector4(1, 1, 0.3f, 60);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.name == "HitBox")
        {
            if (parentz != null)
            {
                col.transform.parent.parent.parent.gameObject.SendMessage("GetHit", k);
                parentz.SendMessage("GetEnergy", k.w);
                col.transform.parent.parent.parent.gameObject.SendMessage("GetAttacker", int.Parse(parentz.transform.tag.Substring(parentz.transform.tag.Length - 1)) - 1);
            }
            else
            {
                Destroy(gameObject);
            }
            if (parentz.transform.position.x > col.transform.parent.parent.parent.position.x)
            {
                k.x = -k.x;
            }
            
        }
    }


}
