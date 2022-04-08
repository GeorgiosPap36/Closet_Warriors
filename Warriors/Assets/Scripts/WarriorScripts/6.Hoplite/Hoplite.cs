using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoplite : Warrior
{

    public const float HP = 150;
    public const float AR = 30;
    public const float EN = 100;
    public const float MS = 5;

    [SerializeField] private string weapon;
    [SerializeField] public int weaponInt;

    // Use this for initialization
    void Start()
    {
        //health,armor,energy,movement_speed
        Initialization(HP, AR, EN, MS);
        weapon = "Sword";
        weaponInt = 0;
        identity[0] = transform.parent.name;
        identity[1] = "5";
        identity[2] = transform.tag.Substring(transform.tag.Length - 1);
    }

    // Update is called once per frame
    void Update()
    {
        FurUpdata(Attack(), Ability(), Ultimate());
        Interupt = UUlt;
    }

    protected override void UpdateAnimParam()
    {
        graphicsBot.GetComponent<UpdateAnimationParemeters>().Moving = moving;
        graphicsBot.GetComponent<UpdateAnimationParemeters>().Grounded = Grounded;
        graphicsBot.GetComponent<UpdateAnimationParemeters>().Attacking = Attacking;
        graphicsBot.GetComponent<UpdateAnimationParemeters>().Ability = UAbil;
        graphicsBot.GetComponent<UpdateAnimationParemeters>().Ultimate = UUlt;

        graphicsTop.GetComponent<HopliteTopParameterUpdate>().Moving = moving;
        graphicsTop.GetComponent<HopliteTopParameterUpdate>().Grounded = Grounded;
        graphicsTop.GetComponent<HopliteTopParameterUpdate>().Attacking = Attacking;
        graphicsTop.GetComponent<HopliteTopParameterUpdate>().Ability = UAbil;
        graphicsTop.GetComponent<HopliteTopParameterUpdate>().Ultimate = UUlt;
    }

    int Attack()
    {
        if (Sprt.sprite.name == "HopliteTop_Attack" + weapon + "_" + (6 - weaponInt))
        {
            DamageMult = 1;
            if (weapon == "Sword")
            {
                return 3;
            }
            return 5;
        }
        return 0;
    }

    int Ability()
    {
        if (Sprt.sprite.name == "HopliteTop_Ability" + weapon + "_6")
        {
            DamageMult = 1;
            if (weapon == "Sword")
            {
                weapon = "Spear";
                weaponInt = 1;
            }
            else
            {
                weapon = "Sword";
                weaponInt = 0;
            }
            return 1;
        }
        return 0;
    }

    int Ultimate()
    {
        if (UUlt)
        {
            Rb2d.velocity = new Vector2(transform.localScale.x * 15, 0);
            weapon = "Spear";
            weaponInt = 1;
            Energy = 0;
        }
        if (Sprt.sprite.name == "HopliteTop_Ulti_8")
        {
            DamageMult = 1;
            return 4;
        }
        return 0;
    }

}
