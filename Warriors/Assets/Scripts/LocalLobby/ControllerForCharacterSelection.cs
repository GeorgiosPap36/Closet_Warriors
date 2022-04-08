using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerForCharacterSelection : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;

    private UpdateAnimParamForCharSelect updateAnim;

    private GroundCheck ground;

    private GetKeyPressed getK;

    private GameObject child;

    private GameObject readyIndicator;

    private bool grounded;
    private bool moving;
    private bool firstEnter;

    private bool ready;

    private float maxspeed;
    private float jumpForce;

    private int playerNumber;

    private string control;
    private string team;

    void Start()
    {
        Initialization();
    }

    void Update()
    {
        FurUpdata();
        Ready();
        ChangeTeam();
        if (PlayerPrefs.GetInt("Mode") == 1)
        {
            readyIndicator.SetActive(true);
            readyIndicator.GetComponent<TextMesh>().text = team;
        }
        else
        {
            readyIndicator.GetComponent<TextMesh>().text = "Ready";
            readyIndicator.SetActive(ready);
        }
        readyIndicator.transform.position = child.transform.position + new Vector3(0, 1, 0);
    }

    void Initialization()
    {  
        moving = false;
        grounded = true;
        maxspeed = 5;
        jumpForce = 350;
        child = transform.GetChild(0).gameObject;
        updateAnim = child.transform.GetChild(0).GetComponent<UpdateAnimParamForCharSelect>();
        rb2d = child.transform.GetComponent<Rigidbody2D>();
        anim = child.GetComponentInChildren<Animator>();
        ground = child.transform.Find("GroundCheck").GetComponent<GroundCheck>();
        getK = GameObject.Find("GetButtonPressed").GetComponent<GetKeyPressed>();
        PlayerNumber = int.Parse(transform.name.Substring(1, 1)) - 1;//thesh ston pinaka
        anim.SetInteger("Warrior", getK.PlayerChild[PlayerNumber]);
        ready = getK.PlayersReady[PlayerNumber];
        team = "Team1";
        readyIndicator = transform.GetChild(1).gameObject;
    }

    void FurUpdata()
    {
        PlayerNumber = int.Parse(transform.name.Substring(1, 1)) - 1;
        grounded = ground.GetGrounded();
        MovementController();
        Jump();
        CheckIfMoving();
        ready = getK.PlayersReady[PlayerNumber];
        updateAnim.Moving = moving;
        updateAnim.Grounded = grounded;
        updateAnim.Warrior = getK.PlayerChild[playerNumber];
    }

    public string Control
    {
        get
        {
            return control;
        }

        set
        {
            control = value;
        }
    }

    public int PlayerNumber
    {
        get
        {
            return playerNumber;
        }

        set
        {
            playerNumber = value;
        }
    }

    void MovementController()
    {
        float hor = Input.GetAxis("Movement" + Control);
        if (hor != 0)
        {
            rb2d.velocity = new Vector2(hor * maxspeed, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        if (hor > 0)
        {
            child.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (hor < 0)
        {
            child.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump" + Control) && grounded)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            grounded = false;
        }
    }

    void Gravity()
    {
        if ((rb2d.velocity.y <= 2) && (!grounded))
        {
            rb2d.AddForce(new Vector2(0, -500 * Time.deltaTime));
        }
    }

    void CheckIfMoving()
    {
        if (!((rb2d.velocity.x == 0) || (Input.GetAxis("Movement" + Control) == 0)))
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
    }

    void Ready()
    {
        if (Input.GetButtonDown("Ability" + Control))
        {
            if (!getK.PlayersReady[PlayerNumber])
            {
                if (getK.PlayerChild[playerNumber] != -1)
                {
                    getK.PlayersReady[playerNumber] = true;
                    if (PlayerPrefs.GetInt("Mode") == 1)
                    {
                        PlayerPrefs.SetString("TagP" + (PlayerNumber + 1), team);
                    }
                    else
                    {
                        PlayerPrefs.SetString("TagP" + (PlayerNumber + 1), "P" + (playerNumber + 1));
                    }
                    readyIndicator.GetComponent<TextMesh>().color = new Color(0, 1, 0);
                }
            }
            else
            {
                getK.PlayersReady[playerNumber] = false;
                readyIndicator.GetComponent<TextMesh>().color = new Color(1, 0, 0);
            }
        } 
    }

    void ChangeTeam()
    {
        if (Input.GetButtonDown("Ultimate" + Control))
        {
            if (PlayerPrefs.GetInt("Mode") == 1)
            {
                if (team == "Team1")
                {
                    team = "Team2";
                }
                else
                {
                    team = "Team1";
                }
                PlayerPrefs.SetString("TagP" + (PlayerNumber + 1), team);
            }
        }
    }

    private void OnDestroy()
    {
        getK.PlayerControl[PlayerNumber] = "0";
        getK.PlayerChild[PlayerNumber] = -1;
        getK.PlayersReady[playerNumber] = false;
    }
}
