using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopliteTopParameterUpdate : MonoBehaviour
{

    private Animator anim;

    private Hoplite hop;

    private bool moving;
    private bool grounded;
    private bool attacking;
    private bool ability;
    private bool ultimate;

    void Start()
    {
        anim = GetComponent<Animator>();
        hop = transform.parent.parent.GetComponent<Hoplite>();
    }

    void Update()
    {
        anim.SetBool("Moving", Moving);
        anim.SetBool("Grounded", Grounded);
        anim.SetBool("Attacking", Attacking);
        anim.SetBool("Ability", Ability);
        anim.SetBool("Ultimate", Ultimate);
        anim.SetInteger("Weapon", hop.weaponInt);
    }

    public bool Moving
    {
        get
        {
            return moving;
        }

        set
        {
            moving = value;
        }
    }

    public bool Grounded
    {
        get
        {
            return grounded;
        }

        set
        {
            grounded = value;
        }
    }

    public bool Attacking
    {
        get
        {
            return attacking;
        }

        set
        {
            attacking = value;
        }
    }

    public bool Ability
    {
        get
        {
            return ability;
        }

        set
        {
            ability = value;
        }
    }

    public bool Ultimate
    {
        get
        {
            return ultimate;
        }

        set
        {
            ultimate = value;
        }
    }

}
