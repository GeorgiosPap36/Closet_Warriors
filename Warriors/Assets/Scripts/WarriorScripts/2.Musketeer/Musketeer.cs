using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musketeer : Warrior
{
      
    public const float HP = 110;
    public const float AR = 30;
    public const float EN = 80;
    public const float MS = 5.5f;

    private bool raycastsend;

    // Use this for initialization
    void Start()
    {
        //health,armor,energy,movement_speed
        Initialization(HP, AR, EN, MS);
        identity[0] = transform.parent.name;
        identity[1] = "1";
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
        if (Sprt.sprite.name == "MusAnim1_Attack_5")
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
        if (Sprt.sprite.name == "MusAnim1_Ability_4")
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
            raycastsend = true;
            Rb2d.velocity = new Vector2(0, Rb2d.velocity.y);
        }
        if (Sprt.sprite.name == "MusAnim1_Ulti_5")
        {
            if (raycastsend)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(transform.localScale.x * 0.5f, 0, 0), Vector2.right * transform.localScale.x,float.PositiveInfinity);
                raycastsend = false;
                if (hit.collider != null)
                {
                    if (hit.collider.transform.name == "HitBox")
                    {
                        if (transform.tag != hit.collider.transform.parent.parent.parent.tag)
                        {
                            hit.collider.transform.parent.parent.parent.gameObject.SendMessage("GetHit", new Vector4(0, 0, 0.3f, 80 * DamageMult));
                            hit.collider.transform.parent.parent.parent.gameObject.SendMessage("GetAttacker", int.Parse(transform.tag.Substring(transform.tag.Length - 1)) - 1);
                        }  
                    }
                }
                Energy = 0;
            }
        }
        if (Sprt.sprite.name == "MusAnim1_Ulti_7")
        {
            DamageMult = 1;           
            return 1;
        }
        return 0;
    }

}
