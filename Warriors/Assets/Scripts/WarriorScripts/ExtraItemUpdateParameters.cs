using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraItemUpdateParameters : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sprtRend;

    private UpdateAnimationParemeters gBot;

    private bool moving;
    private bool grounded;
    private bool attacking;
    private bool ability;
    private bool ultimate;

    void Start()
    {
        anim = GetComponent<Animator>();
        sprtRend = GetComponent<SpriteRenderer>();
        gBot = transform.parent.GetComponent<UpdateAnimationParemeters>();
        sprtRend.color = new Color(255, 255, 255, 255 * (1 - PlayerPrefs.GetInt("InvisibilityToggle")));
    }

    void Update()
    {
        Moving = gBot.Moving;
        Grounded = gBot.Grounded;
        Attacking = gBot.Attacking;
        Ability = gBot.Ability;
        Ultimate = gBot.Ultimate;
        anim.SetBool("Moving", Moving);
        anim.SetBool("Grounded", Grounded);
        anim.SetBool("Attacking", Attacking);
        anim.SetBool("Ability", Ability);
        anim.SetBool("Ultimate", Ultimate);
        sprtRend.color = new Color(255, 255, 255, 255 * (1 - PlayerPrefs.GetInt("InvisibilityToggle")));
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
