using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMessages : MonoBehaviour {

    //1-Armor
    //2-Health
    //3-MovementSpeed
    //4-Damage

    public GameObject[] animations = new GameObject[4];

    public enum buffNumber { Armor, Health, Mana, Damage }

    public buffNumber bN;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.name == "HitBox")
        {
            if ((int)bN == 0)
            {
                col.transform.parent.parent.parent.gameObject.SendMessage("GetArmor", 30);
            }
            else if ((int)bN == 1)
            {
                col.transform.parent.parent.parent.gameObject.SendMessage("GetHealthPickUp", 30);
            }
            else if ((int)bN == 2)
            {
                col.transform.parent.parent.parent.gameObject.SendMessage("GetEnergy", 20f);
            }
            else if ((int)bN == 3)
            {
                col.transform.parent.parent.parent.gameObject.SendMessage("GetDamageBuff", 1.5f);
            }
            GameObject temp = Instantiate(animations[(int)bN]);
            temp.SendMessage("SetPlayer", col.transform.parent.parent.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
