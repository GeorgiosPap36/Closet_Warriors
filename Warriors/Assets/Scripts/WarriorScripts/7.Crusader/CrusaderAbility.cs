using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusaderAbility : MonoBehaviour {

    public Vector4 k;
    private GameObject parentX;

    public float DamageMult;

    void Start ()
    {
        PickParent();
        k = new Vector4(1, 0, 0.5f, 0);
	}
	
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Attack")
        {
            if (parentX.transform.tag != col.transform.parent.parent.parent.tag)
            {
                if (!col.transform.parent.parent.parent.GetComponent<Warrior>().cced)
                {
                    if (parentX.transform.position.x > col.transform.parent.parent.parent.position.x)
                    {
                        k.x = -k.x;
                    }
                    col.transform.parent.parent.parent.gameObject.SendMessage("GetHit", k);
                    parentX.GetComponent<Warrior>().SendMessage("GetEnergy", 20);
                }
            }
        }
    }

    void PickParent()
    {
        parentX = transform.parent.parent.parent.gameObject;
    }
}
