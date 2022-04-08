using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMess : MonoBehaviour
{

    public Vector4 k;
    private GameObject parentz;
    private int number;

    private Vector4[] knockArray = new Vector4[28];

    private float damageMult;

    public enum warriorName { Samurai, Musketeer, Viking, Pirate, Ninja, Hoplite, Crusader, Zweihander, Mage }

    public warriorName wN;

    void Start()
    {
        PickParent();
        number = transform.GetSiblingIndex() + 1;
        number = number + 3 * (int) wN;
        SetArray();
        k = knockArray[number];
        DamageMult = 1;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.name == "HitBox")
        {
            if (parentz.transform.tag != col.transform.parent.parent.parent.tag)
            {
                if (!col.transform.parent.parent.parent.GetComponent<Warrior>().cced)
                {
                    if (parentz.transform.position.x > col.transform.parent.parent.parent.position.x)
                    {
                        k.x = -knockArray[number].x;
                    }
                    else
                    {
                        k.x = knockArray[number].x;
                    }
                    k.w = knockArray[number].w * DamageMult;
                    col.transform.parent.parent.parent.gameObject.SendMessage("GetHit", k);
                    col.transform.parent.parent.parent.gameObject.SendMessage("GetAttacker", int.Parse(parentz.transform.tag.Substring(parentz.transform.tag.Length - 1)) - 1);
                    parentz.GetComponent<Warrior>().SendMessage("GetEnergy", knockArray[number].w);
                }               
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

    void PickParent()
    {
        parentz = transform.parent.parent.parent.gameObject;
    }

    void SetArray()
    {
        //Samurai
        knockArray[1] = new Vector4(2, 0, 0.3f, 40);
        knockArray[2] = new Vector4(3, 0, 0.3f, 60);
        knockArray[3] = new Vector4(0, 0, 0.3f, 70);

        //Musketeer
        knockArray[4] = new Vector4(2, 0, 0.3f, 40);
        knockArray[5] = new Vector4(4.75f, 0, 0.3f, 40);
        knockArray[6] = new Vector4(0, 0, 0.1f, 80);

        //Viking
        knockArray[7] = new Vector4(3, 0, 0.3f, 50);
        knockArray[8] = new Vector4(1, 0, 0.5f, 20);
        knockArray[9] = new Vector4(7, 0, 0.3f, 70);

        //Pirate
        knockArray[10] = new Vector4(3, 0, 0.3f, 40);
        knockArray[11] = new Vector4(1, 0, 0.4f, 60);
        knockArray[12] = new Vector4(0, 0, 0.1f, 70);

        //Ninja
        knockArray[13] = new Vector4(1, 0, 0.1f, 30);
        knockArray[14] = new Vector4(2, 0, 0.5f, 60);
        knockArray[15] = new Vector4(0, 0, 0.1f, 0);

        //Hoplite
        knockArray[16] = new Vector4(1, 0, 0.3f, 30);
        knockArray[17] = new Vector4(4, 0, 0.3f, 40);
        knockArray[18] = new Vector4(0, 5, 0.5f, 50);

        //Crusader
        knockArray[19] = new Vector4(1f, 1.5f, 0.3f, 30);
        knockArray[20] = new Vector4(1, 0, 0.4f, 0);
        knockArray[21] = new Vector4(1, 0, 0.4f, 90);

        //Zweihander
        knockArray[22] = new Vector4(1, 0, 0.3f, 50);
        knockArray[23] = new Vector4(2, 0, 0.5f, 0);
        knockArray[24] = new Vector4(1, 0, 0.3f, 50);

        //Mage
        knockArray[25] = new Vector4(1, 0, 0.1f, 30);
        knockArray[26] = new Vector4(4, 0, 0.25f, 40);
        knockArray[27] = new Vector4(0, 0, 0.5f, 50);
    }

}
