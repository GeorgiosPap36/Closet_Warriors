using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking : Warrior
{
    public GameObject axe;

    private GameObject axeTemp;

    public const float HP = 140;
    public const float AR = 40;
    public const float EN = 90;
    public const float MS = 5.25f;

    // Use this for initialization
    void Start()
    {
        //health,armor,energy,movement_speed
        Initialization(HP, AR, EN, MS);
        identity[0] = transform.parent.name;
        identity[1] = "2";
        identity[2] = transform.tag.Substring(transform.tag.Length - 1);
    }

    // Update is called once per frame
    void Update()
    {
        FurUpdata(Attack(), Ability(), Ultimate());
        Interupt = UUlt;
    }

    int Attack()
    {
        if (Sprt.sprite.name == "VikAnim1_Attack_5")
        {
            DamageMult = 1;
            return 4;
        }
        return 0;
    }

    int Ability()
    {
        if (Sprt.sprite.name == "VikAnim1_Ability_5")
        {
            DamageMult = 1;
            return 3;
        }
        return 0;
    }

    int Ultimate()
    {
        if (UUlt)
        {
            Rb2d.velocity = new Vector2(0, Rb2d.velocity.y);
        }
        if (Sprt.sprite.name == "VikAnim1_Ulti_5")
        {
            if (axeTemp == null)
            {
                axeTemp = Instantiate(axe);          
                axeTemp.transform.SetParent(transform.parent, false);
                axeTemp.GetComponent<AxeManager>().DamageMult = DamageMult;
                Energy = 0;
            }        
        }
        if (Sprt.sprite.name == "VikAnim1_Ulti_6")
        {
            DamageMult = 1;          
            return 1;
        }
        return 0;
    }

}
