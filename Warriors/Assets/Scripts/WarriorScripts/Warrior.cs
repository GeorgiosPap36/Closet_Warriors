using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warrior : MonoBehaviour {

    public GameObject bloodParticles;
    public GameObject hpRegenParticles;
    public GameObject enRegenParticles;

    public GameObject deathAnim;
    public GameObject deathBlood;

    private GameObject cantAttackParticles;
    private GameObject movingParticles;

    private GameObject fountParticles;

    private GameObject parentz;
    private GameObject feetCol;
    private GameObject barsGroup;
    protected GameObject hitbox;

    [SerializeField] protected int lastGotHitFrom;

    private PauseMenuManager pM;

    protected GameObject atCol;
    protected GameObject abCol;
    protected GameObject ulCol;
    
    protected GameObject graphicsBot;
    protected GameObject graphicsTop;

    protected GameObject modeObject;

    protected Rigidbody2D rb2d;
    private SpriteRenderer sprt;

    private AudioSource getDamagedSound;

    private ControlsAndChild cAC;

    private GroundCheck ground;

    private Vector2 knocSpeed;

    protected GameModes gMode;

    //Checking Variables
    private bool grounded;
    public bool cced;
    protected bool moving;
    private bool attacking;
    private bool uAbil;
    private bool uUlt;
    private bool interupt;

    protected int mode;

    //Timer Variables
    private float startingTime;
    private float ccStartingTime;
    private float ccendTime;
    private float ccTime;
    private float reChargeTime;

    //Player Stats
    private float health;
    protected float maxHealth;
    private float armor;
    private float energy;
    protected float maxEnergy;
    protected float maxspeed;
    private float originalMS;
    protected float jumpForce;
    private float damageMult;

    private string movementControls;
    private string controlJump;
    private string controlAttack;
    private string controlAbility;
    private string controlUltimate;

    [SerializeField] protected string attackerName;

    [SerializeField] public string[] identity = new string[3];


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

    public bool UAbil
    {
        get
        {
            return uAbil;
        }

        set
        {
            uAbil = value;
        }
    }

    public bool UUlt
    {
        get
        {
            return uUlt;
        }

        set
        {
            uUlt = value;
        }
    }

    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public float Armor
    {
        get
        {
            return armor;
        }

        set
        {
            armor = value;
        }
    }

    public float Energy
    {
        get
        {
            return energy;
        }

        set
        {
            energy = value;
        }
    }

    public bool Interupt
    {
        get
        {
            return interupt;
        }

        set
        {
            interupt = value;
        }
    }

    public Rigidbody2D Rb2d
    {
        get
        {
            return rb2d;
        }

        set
        {
            rb2d = value;
        }
    }

    public SpriteRenderer Sprt
    {
        get
        {
            return sprt;
        }

        set
        {
            sprt = value;
        }
    }

    public string ControlAttack
    {
        get
        {
            return controlAttack;
        }

        set
        {
            controlAttack = value;
        }
    }

    public string ControlAbility
    {
        get
        {
            return controlAbility;
        }

        set
        {
            controlAbility = value;
        }
    }

    public string ControlUltimate
    {
        get
        {
            return controlUltimate;
        }

        set
        {
            controlUltimate = value;
        }
    }

    public float DamageMult
    {
        get
        {
            return damageMult;
        }

        set
        {
            damageMult = value;
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

    public string ControlJump
    {
        get
        {
            return controlJump;
        }

        set
        {
            controlJump = value;
        }
    }

    public string MovementControls
    {
        get
        {
            return movementControls;
        }

        set
        {
            movementControls = value;
        }
    }

    public void Initialization(float hp, float arm, float en, float ms)
    {
        pM = GameObject.Find("Canvas").GetComponent<PauseMenuManager>();
        damageMult = 1;
        parentz = transform.parent.gameObject;
        graphicsBot = transform.Find("GraphicsBot").gameObject;
        graphicsTop = graphicsBot.transform.Find("GraphicsTop").gameObject;
        cAC = parentz.GetComponent<ControlsAndChild>();
        MovementControls = cAC.MovementControl;
        ControlJump = cAC.ControlJump;
        ControlAttack = cAC.ControlAttack;
        ControlAbility = cAC.ControlAbility;
        ControlUltimate = cAC.ControlUltimate;
        moving = false;
        Attacking = false;
        Grounded = false;
        cced = false;
        Health = hp;
        maxHealth = hp;
        Armor = arm;
        Energy = 0;
        maxEnergy = en;
        maxspeed = ms;
        originalMS = maxspeed;
        jumpForce = 350;
        Rb2d = GetComponent<Rigidbody2D>();
        Sprt = graphicsTop.GetComponent<SpriteRenderer>();
        barsGroup = transform.Find("BarsGroup").gameObject;
        ground = transform.Find("GroundCheck").GetComponent<GroundCheck>();
        hitbox = graphicsTop.transform.Find("HitBox").gameObject;
        cantAttackParticles = transform.Find("CantAttackParticleSystem").gameObject;
        movingParticles = transform.Find("MovingParticleSystem").gameObject;
        atCol = graphicsTop.transform.Find("Attack").gameObject;
        abCol = graphicsTop.transform.Find("Ability").gameObject;
        ulCol = graphicsTop.transform.Find("Ultimate").gameObject;
        getDamagedSound = GetComponent<AudioSource>();
        SetUpBars();
        graphicsBot.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255 * (1 - PlayerPrefs.GetInt("InvisibilityToggle")));
        graphicsTop.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255 * (1 - PlayerPrefs.GetInt("InvisibilityToggle")));
        modeObject = GameObject.FindGameObjectWithTag("Mode");
        mode = PlayerPrefs.GetInt("Mode");
    }

    public void FurUpdata(int a, int b, int c)
    {
        if (pM.paused == 1)
        {
            Grounded = ground.GetGrounded();
            UpdateAnimParam();
            Action(a, b, c);
            CheckIfMoving(Input.GetAxis(MovementControls));
            Gravity();
            ShowCantAttackParticles();
            UpdateBars();
            ChangeSpriteOrder();
            MovingParticles();
            if (DeathController())
            {
                Destroy(gameObject);
            }
        }     
    }

    void MovingParticles()
    {
        movingParticles.SetActive(moving && Grounded);
        movingParticles.transform.localScale = new Vector3(1, transform.localScale.x, 1);
        movingParticles.transform.eulerAngles = new Vector3(0, 0, movingParticles.transform.localScale.x * 90);
    }

    protected virtual void UpdateDamageMult()
    {
        atCol.GetComponent<SendMess>().DamageMult = DamageMult;
        abCol.GetComponent<SendMess>().DamageMult = DamageMult;
        ulCol.GetComponent<SendMess>().DamageMult = DamageMult;
    }

    void Action(int at, int ab, int ul)
    {
        CheckIfGrounded();

        Attack(at);
        Ability(ab);
        Ultimate(ul);

        CCed(ccTime, knocSpeed);
        MovementController(Input.GetAxis(MovementControls));
        Jump();
    }

   protected virtual float InputController(float h)
    {
        return h;
    }

    protected void MovementController(float hor)
    {

        if (!cced)
        {
            if (!Interupt)
            {
                if (hor != 0)
                {
                    Rb2d.velocity = new Vector2(hor * maxspeed, Rb2d.velocity.y);
                }
                else
                {
                    Rb2d.velocity = new Vector2(0, Rb2d.velocity.y);
                }
            }           
            if (hor > 0.05f)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else if (hor < -0.05f)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }
        } 
    }

    protected virtual void Jump()
    {
        if (Input.GetButtonDown(ControlJump) && Grounded && (Mathf.Abs(rb2d.velocity.y) <= 0.5f))
        {
            Rb2d.AddForce(new Vector2(0, jumpForce));
            Grounded = false;
        }       
    }

    protected virtual void Attack(int x)
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

    protected virtual void Ability(int x)
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

    protected virtual void Ultimate(int x)
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

    protected virtual void UpdateAnimParam()
    {
        graphicsBot.GetComponent<UpdateAnimationParemeters>().Moving = moving;
        graphicsBot.GetComponent<UpdateAnimationParemeters>().Grounded = Grounded;
        graphicsBot.GetComponent<UpdateAnimationParemeters>().Attacking = Attacking;
        graphicsBot.GetComponent<UpdateAnimationParemeters>().Ability = UAbil;
        graphicsBot.GetComponent<UpdateAnimationParemeters>().Ultimate = UUlt;

        graphicsTop.GetComponent<UpdateAnimationParemeters>().Moving = moving;
        graphicsTop.GetComponent<UpdateAnimationParemeters>().Grounded = Grounded;
        graphicsTop.GetComponent<UpdateAnimationParemeters>().Attacking = Attacking;
        graphicsTop.GetComponent<UpdateAnimationParemeters>().Ability = UAbil;
        graphicsTop.GetComponent<UpdateAnimationParemeters>().Ultimate = UUlt;
    }

    void CCed(float time, Vector2 speed)
    {
        if (cced)
        {
            ccendTime = Time.time;
            float timeDif = ccendTime - ccStartingTime;

            if ((timeDif < time))
            { 
                Rb2d.velocity = speed;
                speed *= 99 / 100;
                timeDif = ccendTime - ccStartingTime;            
            }
            else
            {
                cced = false;
            }
        }
    }

    void CheckIfGrounded()
    {
        if (cced)
        {
            Attacking = false;
            UAbil = false;
            UUlt = false;
        }
    }

    void Gravity()
    {
        if ((Rb2d.velocity.y <= 2) && (!Grounded))
        {
            Rb2d.AddForce(new Vector2(0, -500* Time.deltaTime));
        }
    }

    void ChangeSpriteOrder()
    {
        if (Grounded)
        {
            Sprt.sortingOrder = 8;
        }
        else
        {
            Sprt.sortingOrder = 6;
        }
    }

    void CheckIfMoving(float hor)
    {
        if (!((Rb2d.velocity.x == 0) || (hor == 0)))
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
    }

    protected void SetRecharge(int x)
    {
        if (x != 0)
        {
            Attacking = false;
            uAbil = false;
            UUlt = false;
            reChargeTime = x;
            startingTime = Time.time;
        }
    }

    void Damaged(float damage)
    {
        if (Armor - damage >= 0)
        {
            Armor -= damage;
        }
        else
        {
            damage = damage - Armor;
            Armor = 0;
            Health = Health - damage;
        }
        DeathController();
    }

    protected bool Recharge()
    {
        if (Time.time - startingTime < reChargeTime * 0.1f)
        {
            return true;
        }
        return false;
    }

    void ShowCantAttackParticles()
    {
        cantAttackParticles.SetActive(Recharge());
    }

    public bool DeathController()
    {
        if (Health > 0)
        {
            return false;
        }

        return true;
    }

    void CloseHitBoxCced()
    {
        hitbox.SetActive(!cced);
    }

    IEnumerator Knockback(float time)
    {
        graphicsBot.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 120);
        graphicsTop.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 120);
        yield return new WaitForSeconds(time);
        graphicsBot.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255 * (1 - PlayerPrefs.GetInt("InvisibilityToggle")));
        graphicsTop.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255 * (1 - PlayerPrefs.GetInt("InvisibilityToggle")));
    }

    void GetHit(Vector4 k)
    {
        if (!cced)
        {
            //Vector 4 --> x,y KB_Speed -//- z time -//- damage
            if (UAbil || Attacking || UUlt)
            {
                DamageMult = 1;
            }
            getDamagedSound.enabled = true;
            if (k.w != 0)
            {
                StartCoroutine(InstantiateParticle(bloodParticles));
            }
            ccStartingTime = Time.time;
            StartCoroutine(Knockback(0.05f));
            cced = true;
            if (k.y == 0)
            {
                k.y = -1;
            }
            knocSpeed = new Vector2(k.x, k.y);
            ccTime = k.z;
            Damaged(k.w);
        }     
    }

    void GetAttacker(int attacker)
    {
        if (mode != 1)
        {
            lastGotHitFrom = attacker;
            attackerName = GameObject.FindGameObjectWithTag("P" + (lastGotHitFrom + 1)).transform.name;
        }
        
    }

    protected virtual void GetEnergy(float i)
    {
        Energy = Energy + i;
        if (Energy > maxEnergy)
        {
            Energy = maxEnergy;
        }
    }

    void GetHealth(float hp)
    {
        Health += hp;
        if (Health > maxHealth)
        {
            Health = maxHealth;
        }
    }

    void GetArmor(float a)
    {
        Armor += a;
    }

    void GetHealthPickUp(float hp)
    {
        Health += hp;
        if (Health > maxHealth)
        {
            Health = maxHealth;
        }
        
    }

    void GetMS(float multiplyer)
    {
        StartCoroutine(MsBuff(multiplyer));
    }

    void GetDamageBuff(float mult)
    {
        DamageMult = mult;
    }

    IEnumerator MsBuff(float f)
    {
        maxspeed = maxspeed * f;
        yield return new WaitForSeconds(1f);
        maxspeed = originalMS;
    }

    void SetUpBars()
    {
        barsGroup.transform.Find("Health").Find("Back").localScale = new Vector3(Health / 100, 1, 1);
        barsGroup.transform.Find("Health").Find("Front").localScale = new Vector3(Health / 100, 1, 1);
        barsGroup.transform.Find("Energy").Find("Back").localScale = new Vector3(maxEnergy / 100, 1, 1);
        barsGroup.transform.Find("Energy").Find("Front").localScale = new Vector3(maxEnergy / 100, 1, 1);

    }

    void UpdateBars()
    {
        barsGroup.transform.Find("Health").Find("Bar").localScale = new Vector3(Health / 100 , 1, 1);
        barsGroup.transform.Find("Energy").Find("Bar").localScale = new Vector3(Energy / 100 , 1, 1);
        barsGroup.transform.Find("Armor").localScale = new Vector3(Armor / 100, 1, 1);
    }

    IEnumerator InstantiateParticle(GameObject part)
    {
        GameObject temp = Instantiate(part);
        temp.transform.SetParent(transform.parent);
        temp.transform.position = transform.position;
        yield return new WaitForSeconds(1);
        Destroy(temp);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Death")
        {
            transform.localPosition = new Vector3(0, 5, 0);
        }
    }

    protected void OnDestroy()
    {
        if (DeathController())
        {
            if (mode == 6)
            {
                modeObject.SendMessage("Killer", attackerName);
                modeObject.SendMessage("DeadManName", transform.name);
            }
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
