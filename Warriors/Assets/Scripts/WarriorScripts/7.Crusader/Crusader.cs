using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusader : Warrior
{

    public const float HP = 90;
    public const float AR = 100;
    public const float EN = 70;
    public const float MS = 4.25f;

    // Use this for initialization
    void Start()
    {
        //health,armor,energy,movement_speed
        Initialization(HP, AR, EN, MS);
        identity[0] = transform.parent.name;
        identity[1] = "6";
        identity[2] = transform.tag.Substring(transform.tag.Length - 1);
    }

    // Update is called once per frame
    void Update()
    {
        FurUpdata(Attack(), Ability(), Ultimate());
        Interupt = UUlt;
    }

    protected override void UpdateDamageMult()
    {
        atCol.GetComponent<SendMess>().DamageMult = DamageMult;
        abCol.GetComponent<CrusaderAbility>().DamageMult = DamageMult;
        ulCol.GetComponent<SendMess>().DamageMult = DamageMult;
    }

    int Attack()
    {
        if (Sprt.sprite.name == "CrusaderTop_Attack_5")
        {
            DamageMult = 1;
            return 4;
        }
        return 0;
    }

    int Ability()
    {
        if (Sprt.sprite.name == "CrusaderTop_Ability_5")
        {
            DamageMult = 1;
            return 6;
        }
        return 0;
    }

    int Ultimate()
    {
        if (UUlt)
        {
            Rb2d.velocity = Vector2.zero;
            Energy = 0;
        }
        if (Sprt.sprite.name == "CrusaderTop_Ulti_6")
        {
            DamageMult = 1;
            return 2;
        }
        return 0;
    }

}
