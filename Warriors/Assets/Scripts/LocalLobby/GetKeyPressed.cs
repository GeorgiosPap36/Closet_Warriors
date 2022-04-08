using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GetKeyPressed : MonoBehaviour
{
    public string s1;
    public string S;

    public GameObject obj;

    private GameObject[] players = new GameObject[4];
    private string[] playerControl = new string[4];
    private bool[] playersReady = new bool[4];
    private int[] playerChild = new int[4];

    private GameObject playersGroup;

    void Start()
    {
        Cursor.visible = true;
        playersGroup = GameObject.Find("Players");
        for (int i = 0; i < 4; i++)
        {
            playerChild[i] = -1;
            PlayersReady[i] = false;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Spawn"))
        {
            detectPressedKeyOrButton();
        }
    }

    public GameObject[] Players
    {
        get
        {
            return players;
        }

        set
        {
            players = value;
        }
    }

    public string[] PlayerControl
    {
        get
        {
            return playerControl;
        }

        set
        {
            playerControl = value;
        }
    }

    public int[] PlayerChild
    {
        get
        {
            return playerChild;
        }

        set
        {
            playerChild = value;
        }
    }

    public bool[] PlayersReady
    {
        get
        {
            return playersReady;
        }

        set
        {
            playersReady = value;
        }
    }

    public void detectPressedKeyOrButton()
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {        
                if (kcode.ToString().Length >= 14)//elegxei to megethos tou string gt to megethos tou substring borei na bgainei <0
                {      
                    if (kcode.ToString().Substring(0, kcode.ToString().Length - 1) != "JoystickButton")//elegxei an dinei arithmo xeirhsthriou (p.x. Joystick2Button0) xwris to poio koubi paththike
                    {
                        if (S == "")
                        {
                            //metaferei to poio xeirhsthrio prepei na xrhsimopoihthei
                            s1 = kcode.ToString();
                            S = s1.Substring(8, 1);
                            Spawn(S);
                        }
                    }
                }
                else
                {
                    S = "K";
                    Spawn(S);    
                }
                
            }
        }
    }

    void Spawn(string control)
    {
        bool controlNotUsed = ((PlayerControl[0] != control) && (PlayerControl[1] != control) && (PlayerControl[2] != control) && (PlayerControl[3] != control));
        for (int i = 0; i < 4; i++)
        {
            if (controlNotUsed)
            {
                if (Players[i] == null)
                {
                    PlayerChild[i] = -1;
                    PlayerControl[i] = control;
                    Players[i] = Instantiate(obj);
                    Players[i].transform.position = new Vector3(3.35f, -3.23f, 0);
                    Players[i].transform.SetParent(playersGroup.transform);
                    Players[i].transform.name = "P" + (i + 1);
                    Players[i].GetComponent<ControllerForCharacterSelection>().Control = S;       
                    break;
                }
            }        
        }
        s1 = "";
        S = "";
    }

}
