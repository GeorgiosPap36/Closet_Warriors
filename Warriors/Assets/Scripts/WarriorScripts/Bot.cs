using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Warrior
{
    public GameObject curEnemyTarget;
    private Vector2 curMoveTarget;

    private float movHor;

    delegate void Behaviour();
    Behaviour BehaviourDel;

    void Awake()
    {
        Initialization(100, 100, 100, 5);
    }

    void Update()
    {
        FurUpdata(1, 1, 1);
        BehaviourController();
        //Debug.Log(GameObject.FindGameObjectsWithTag("Feet"));
        BehaviourDel();
    }

    protected void BehaviourController()
    {
        if (BehaviourDel == null)
        {
            BehaviourDel += HpCheck;
        }
    }

    private void FindTarget()
    {

        //
        // 333333333333333333333333333
        // Finds the closest warrior
        //

        BehaviourDel -= Move;
        BehaviourDel += Move;
    }

    private void Move()
    {
        FindMoveTarget();
        if (curMoveTarget.x - transform.position.x > 0)
        {
            movHor = 1;
        }
        else
        {
            movHor = -1;
        }
    }

    private void FindMoveTarget()
    {
        if (Mathf.Abs(curEnemyTarget.transform.position.y - transform.position.y) < 1)
        {
            curMoveTarget = curEnemyTarget.transform.position;
        }
        else if (transform.position.y > curEnemyTarget.transform.position.y)
        {
            //fall from platform
        }
        else
        {
            //go to jumpPos
        }
    }


    ///
    /// 222222222222222222222222222222
    /// **on trigger gia na kanei attacks**
    /// (check if can attack)
    /// -move + attack
    ///

    private void OnTriggerEnter2D(Collider2D col)
    {
        //
        //BehaviourDel += AttackTarget;
    }

    private void AttackTarget()
    {
        //call Attack or ability or Ult
        //change localscale based on attack
    }

    private void ChooseAction(GameObject victim)
    {
        ///
        /// check HP and choose attack based on remaining hp
        /// check if you have the mana to use ult
        ///
    }

    /// 111111111111111111111111111111
    ///**low hp**
    ///
    ///RUN, NIGGA RUN
    ///To healing point ->find move target
    private void HpCheck()
    {
        if ((maxHealth * 0.4 >= Health) && (HpFountainCheck()))
        {

            Vector2 fountPos = GameObject.Find("HealthFountain").transform.position;
            curMoveTarget = fountPos;
        }
        else
        {
            BehaviourDel += FindTarget;
        }
    }

    private bool HpFountainCheck()
    {

        if (PlayerPrefs.GetInt("FountainsToggle") != 0)
        {
            return true;
        }
        return false;
    }


    protected override void Jump()
    {
        if (Input.GetButtonDown(ControlJump) && Grounded && (Mathf.Abs(rb2d.velocity.y) <= 0.5f))
        {
            Rb2d.AddForce(new Vector2(0, jumpForce));
            Grounded = false;
        }
    }

    protected override void Attack(int x)
    {
        if ((!cced && !Recharge()))
        {
            if (Input.GetButtonDown(ControlAttack))
            {
                if ((!UAbil && !UUlt && !Attacking))
                {
                    Attacking = true;
                }
            }
            int temp = x;
            SetRecharge(temp);
        }
    }

    protected override void Ability(int x)
    {
        if ((!cced && !Recharge()))
        {
            if (Input.GetButtonDown(ControlAbility))
            {
                if ((!Attacking && !UUlt && !UAbil))
                {
                    UAbil = true;
                }
            }
            int temp = x;
            SetRecharge(temp);
        }
    }

    protected override void Ultimate(int x)
    {
        if ((!cced && !Recharge()))
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
    }
}
