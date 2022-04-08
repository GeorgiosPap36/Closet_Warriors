using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAnimationParemeters : MonoBehaviour {

    private Animator anim;
    
    private bool moving;
    private bool grounded;
    private bool attacking;
    private bool ability;
    private bool ultimate;

    void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
        anim.SetBool("Moving", Moving);
        anim.SetBool("Grounded", Grounded);
        anim.SetBool("Attacking", Attacking);
        anim.SetBool("Ability", Ability);
        anim.SetBool("Ultimate", Ultimate);
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
