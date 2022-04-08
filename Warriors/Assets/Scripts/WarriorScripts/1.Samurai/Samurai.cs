using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samurai : Warrior {

    public const float HP = 100;
    public const float AR = 20;
    public const float EN = 100;
    public const float MS = 6f;

    // Use this for initialization
    void Start ()
    {
        //health,armor,energy,movement_speed
        Initialization(HP,AR,EN,MS);
        identity[0] = transform.parent.name;
        identity[1] = "0";
        identity[2] = transform.tag.Substring(transform.tag.Length - 1);
    }
	
	// Update is called once per frame
	void Update()
    {
        FurUpdata(Attack(),Ability(),Ultimate());
        Interupt = UUlt;
	}

    int Attack()
    { 
        if (Sprt.sprite.name == "SamAnimTop_Attack_4")
        {
            DamageMult = 1;
            return 3;
        }
        return 0;
    }

    int Ability()
    {  
        if (Sprt.sprite.name == "SamAnimTop_Ability_7")
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
            Rb2d.velocity = new Vector2(transform.localScale.x * 20, 0);
            Energy = 0;
        }
        if (Sprt.sprite.name == "SamAnimTop_Ulti_5")
        {
            DamageMult = 1;
            return 2;
        }
        return 0;
    }

}
