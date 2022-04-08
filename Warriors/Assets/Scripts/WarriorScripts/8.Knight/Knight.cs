using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Warrior
{
    public const float HP = 80;
    public const float AR = 60;
    public const float EN = 80;
    public const float MS = 5.25f;

    // Use this for initialization
    void Start()
    {
        //health,armor,energy,movement_speed
        Initialization(HP, AR, EN, MS);
        identity[0] = transform.parent.name;
        identity[1] = "7";
        identity[2] = transform.tag.Substring(transform.tag.Length - 1);
    }

    // Update is called once per frame
    void Update()
    {
        FurUpdata(Attack(), Ability(), Ultimate());
        Interupt = UAbil;
    }

    protected override void Ultimate(int x)
    {
        if (Input.GetButtonDown(ControlUltimate))
        {
            if (Energy >= maxEnergy)
            {
                if ((!Attacking && !UAbil && !UUlt))
                {
                    UUlt = true;
                }
            }
        }
        int temp = x;
        SetRecharge(temp);
    }

    int Attack()
    {
        if (Sprt.sprite.name == "KnightTop_Attack_5")
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
            Rb2d.velocity = new Vector2(Rb2d.velocity.x / 2 + transform.localScale.x * 3, Rb2d.velocity.y);
        }
        if (Sprt.sprite.name == "KnightTop_Ability_7")
        {
            DamageMult = 1;
            return 5;
        }
        return 0;
    }

    int Ultimate()
    {
        if (UUlt)
        {

            Energy = 0;
        }
        if (Sprt.sprite.name == "KnightTop_Ulti_3")
        {

            DamageMult = 1;
            return 1;
        }
        return 0;
    }

}
