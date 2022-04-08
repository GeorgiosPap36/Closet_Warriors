using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : Warrior
{
    public GameObject rumBottle;

    private GameObject rumTemp;

    public const float HP = 110;
    public const float AR = 20;
    public const float EN = 100;
    public const float MS = 4.5f;

    // Use this for initialization
    void Start()
    {
        //health,armor,energy,movement_speed
        Initialization(HP, AR, EN, MS);
        identity[0] = transform.parent.name;
        identity[1] = "3";
        identity[2] = transform.tag.Substring(transform.tag.Length - 1);
    }

    // Update is called once per frame
    void Update()
    {
        FurUpdata(Attack(), Ability(), Ultimate());
        Interupt = UAbil || UUlt;
    }

    int Attack()
    {
        if (Sprt.sprite.name == "PirAnim1_Attack_4")
        {
            DamageMult = 1;
            return 3;
        }
        return 0;
    }

    int Ability()
    {
        if (UAbil)
        {
            if (Grounded)
            {
                Rb2d.velocity = new Vector2(0, Rb2d.velocity.y);
            }
        }
        if (Sprt.sprite.name == "PirAnim1_Ability_7")
        {
            DamageMult = 1;
            return 4;
        }
        return 0;
    }

    int Ultimate()
    {
        if (UUlt)
        {
            Rb2d.velocity = new Vector2(0, Rb2d.velocity.y);
        }
        if (Sprt.sprite.name == "PirAnim1_Ulti_2")
        {
            if (rumTemp == null)
            {
                rumTemp = Instantiate(rumBottle);
                rumTemp.transform.SetParent(transform.parent, false);
                rumTemp.GetComponent<RumBottleManager>().DamageMult = DamageMult;
                Energy = 0;
            }
            else
            {
                if (rumTemp.transform.GetChild(1).gameObject.activeInHierarchy)
                {
                    Destroy(rumTemp.gameObject);
                    rumTemp = Instantiate(rumBottle);
                    rumTemp.transform.SetParent(transform.parent, false);
                    rumTemp.GetComponent<RumBottleManager>().DamageMult = DamageMult;
                    Energy = 0;
                }
            }
        }
        if (Sprt.sprite.name == "PirAnim1_Ulti_5")
        {
            DamageMult = 1;            
            return 1;
        }
        return 0;
    }

}

