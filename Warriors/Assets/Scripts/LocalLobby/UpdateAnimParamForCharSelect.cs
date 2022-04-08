using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAnimParamForCharSelect : MonoBehaviour {

    private Animator anim;

    private bool moving;
    private bool grounded;

    private int warrior;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("Moving", Moving);
        anim.SetBool("Grounded", Grounded);
        anim.SetInteger("Warrior", Warrior);
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

    public int Warrior
    {
        get
        {
            return warrior;
        }

        set
        {
            warrior = value;
        }
    }
}
