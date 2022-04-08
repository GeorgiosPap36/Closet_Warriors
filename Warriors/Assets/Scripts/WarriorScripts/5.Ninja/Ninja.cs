using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : Warrior
{
    public GameObject shuriken;
    public GameObject bunshin;
    public GameObject smokeObj;

    private GameObject shurTemp;
    private GameObject bunTemp;

    private bool raycastsend;

    public const float HP = 110;
    public const float AR = 10;
    public const float EN = 50;
    public const float MS = 6.5f;

    // Use this for initialization
    void Start()
    {
        //health,armor,energy,movement_speed
        Initialization(HP, AR, EN, MS);
        identity[0] = transform.parent.name;
        identity[1] = "4";
        identity[2] = transform.tag.Substring(transform.tag.Length - 1);
        raycastsend = true;
    }

    // Update is called once per frame
    void Update()
    {
        FurUpdata(Attack(), Ability(), Ultimate());
        Interupt =  UUlt;
    }

    int Attack()
    {
        if (Sprt.sprite.name == "NinjaTop_Attack_5")
        {
            if (shurTemp == null)
            {
                shurTemp = Instantiate(shuriken);
                shurTemp.SendMessage("PickParent", gameObject);
                shurTemp.GetComponent<ShurikenManager>().DamageMult = DamageMult;
            }      
        }
        if (Sprt.sprite.name == "NinjaTop_Attack_6")
        {
            DamageMult = 1;
            return 2;
        }
        return 0;
    }

    int Ability()
    {
        if (Sprt.sprite.name == "NinjaTop_Ability_7")
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
            
            if (raycastsend)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(transform.localScale.x * 0.5f, 0, 0), Vector2.right * transform.localScale.x, 7);
                bunTemp = Instantiate(bunshin);
                bunTemp.transform.position = transform.position;
                bunTemp.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                raycastsend = false;
                if (hit.collider != null)
                {
                    if (hit.collider.transform.name == "HitBox")
                    {
                        transform.position = hit.collider.transform.position - hit.collider.transform.transform.parent.parent.parent.localScale.x * new Vector3(0.75f, 0, 0);
                        transform.localScale = new Vector3(hit.collider.transform.transform.parent.parent.parent.localScale.x, transform.localScale.y, transform.localScale.z);
                        gameObject.SendMessage("GetDamageBuff", 3);
                    }
                    if (hit.collider.transform.tag == "Wall")
                    {
                        transform.position = hit.point - transform.localScale.x * new Vector2(1, 0);
                    }
                }
                else
                {
                    transform.position = transform.position + transform.localScale.x * new Vector3(5, 0, 0);
                }
                Instantiate(smokeObj).transform.position = transform.position;
                Energy = 0;
            }
        }
        if (!raycastsend)
        {
            DamageMult = 1;
            raycastsend = true;
            Energy = 0;
            return 1;
        }
        return 0;
    }

}

