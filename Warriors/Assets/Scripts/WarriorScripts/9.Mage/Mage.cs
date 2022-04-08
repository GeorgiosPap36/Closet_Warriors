using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Warrior {

    [SerializeField] private GameObject mageShield;
    [SerializeField] private GameObject mageBeam;
    [SerializeField] private GameObject mageSpike;

    private JuggernaughtMode jugger;

    private GameObject activeBeam;

    private int depletedMana;

    private bool canAttack;

    [SerializeField] private float ultiCounter;

    [SerializeField] private float attackTimer;

    public const float HP = 100;
    public const float AR = 20;
    public const float EN = 100;
    public const float MS = 6f;

    // Use this for initialization
    void Start()
    {
        //health,armor,energy,movement_speed
        Initialization(HP, AR, EN, MS);
        jugger = GameObject.FindGameObjectWithTag("Mode").GetComponent<JuggernaughtMode>();
        depletedMana = 20;
        ultiCounter = 0f;
        attackTimer = 0;
        Energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        FurUpdata(Attack(), Ability(), Ultimate());
        Interupt = UUlt || UAbil;
        canAttack = CanAttack();
    }

    protected override void Ultimate(int x)
    {
        if ((!cced && !Recharge()))
        {
            if (Input.GetButtonDown(ControlUltimate))
            {
                if (Energy >= depletedMana)
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

    protected override void GetEnergy(float i)
    {
        if (!UUlt)
        {
            base.GetEnergy(i);
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
                    if (canAttack)
                    {
                        Attacking = true;
                    } 
                }
            }
            int temp = x;
            SetRecharge(temp);
        }
    }

    bool CanAttack()
    {
        attackTimer = Mathf.Clamp(attackTimer - Time.deltaTime, 0, 5);
        if (attackTimer <= 0)
        {
            return true;
        }
        return false;
    }

    int Attack()
    {
        if (Grounded)
        {
            if (Attacking)
            {
                GameObject temp = Instantiate(mageShield, transform.parent);
                temp.transform.position = transform.position;
                attackTimer = 5;
                DamageMult = 1;
                return 5;
            }
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
        if (Sprt.sprite.name == "MageTop_Ability_7")
        {
            if (activeBeam == null)
            {
                activeBeam = Instantiate(mageBeam, transform.parent);
                activeBeam.transform.position = transform.position + new Vector3(transform.localScale.x * 0.75f, 0, 0);
                activeBeam.transform.localScale = transform.localScale;
            }
            DamageMult = 1;
            return 1;
        }
        return 0;
    }

    int Ultimate()
    {
        if (UUlt)
        {
            if (Grounded)
            {
                Rb2d.velocity = Vector2.zero;
            }
            ultiCounter -= Time.deltaTime;
            if (ultiCounter <= 0)
            {
                GameObject temp = Instantiate(mageSpike,transform.parent);
                PickSpikeSpawnLocation(temp);
                Energy -= depletedMana;
                ultiCounter = 1f;
            }
            if (Energy <= 0)
            {
                DamageMult = 1;
                return 2;
            }
        }
        return 0;
    }

    void PickSpikeSpawnLocation(GameObject t)
    {
        float randomV = Random.value;
        if (randomV >= 0.75f)
        {
            t.transform.eulerAngles = new Vector3(0, 0, -90);
            t.transform.position = new Vector3(Random.Range(-8, 8), -9.5f, 0);
        }
        else if (randomV >= 0.5f)
        {
            t.transform.eulerAngles = new Vector3(0, 0, 180);
            t.transform.position = new Vector3(-13.5f, Random.Range(-4.2f, 4.2f), 0);
        }
        else if (randomV >= 0.25f)
        {
            t.transform.eulerAngles = new Vector3(0, 0, 90);
            t.transform.position = new Vector3(Random.Range(-8, 8), 9.5f, 0);
        }
        else
        {
            t.transform.eulerAngles = new Vector3(0, 0, 0);
            t.transform.position = new Vector3(13.5f, Random.Range(-4.2f, 4.2f), 0);
        }
    }

    private new void OnDestroy()
    {
        if (DeathController())
        {
            if (mode == 6)
            {
                modeObject.SendMessage("Killer", attackerName);
                modeObject.SendMessage("DeadManName", transform.name);
            }
            jugger.mageObj = null;
            Instantiate(deathBlood).transform.position = transform.position;
            Instantiate(deathAnim).transform.position = transform.position + new Vector3(0, 1, 0);
            modeObject.SendMessage("Respawn", identity);
            if (mode == 4 || mode == 6)
            {
                modeObject.SendMessage("Attacker", lastGotHitFrom);
            }
        }
    }
}
