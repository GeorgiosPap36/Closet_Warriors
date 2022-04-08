using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsAndChild : MonoBehaviour
{

    public GameObject[] warriors = new GameObject[8];
    public GameObject livesLeft;

    private PlayerControls pC;

    private GameObject child;
    private GameObject livesLeftPanel;

    private Vector3[] spawnPos = new Vector3[4];
    private Vector3[] mapSpawnPos = new Vector3[16];

    [SerializeField] private string movementControl;
    [SerializeField] private string controlJump;
    [SerializeField] private string controlAttack;
    [SerializeField] private string controlAbility;
    [SerializeField] private string controlUltimate;

    private int number;


    void Start()
    {
        pC = GameObject.Find("PlayerControls").GetComponent<PlayerControls>();
        SpawnPosForMap();
        SetSpawnPos();
        ChildSetUp();
    }

    void ChildSetUp()
    {
        number = int.Parse(transform.name.Substring(1, 1)) - 1;
        if (PlayerPrefs.GetInt("Warrior" + transform.name) == -1)
        {
            Destroy(gameObject);
        }
        else
        {
            movementControl = "Movement" + PlayerPrefs.GetString("ControlP" + (number + 1));
            if (number == 0)
            {
                ControlJump = pC.controlsForPlayers[0];
                ControlAttack = pC.controlsForPlayers[1];
                ControlAbility = pC.controlsForPlayers[2];
                ControlUltimate = pC.controlsForPlayers[3];
            }
            else if (number == 1)
            {
                ControlJump = pC.controlsForPlayers[4];
                ControlAttack = pC.controlsForPlayers[5];
                ControlAbility = pC.controlsForPlayers[6];
                ControlUltimate = pC.controlsForPlayers[7];
            }
            else if (number == 2)
            {
                ControlJump = pC.controlsForPlayers[8];
                ControlAttack = pC.controlsForPlayers[9];
                ControlAbility = pC.controlsForPlayers[10];
                ControlUltimate = pC.controlsForPlayers[11];
            }
            else if (number == 3)
            {
                ControlJump = pC.controlsForPlayers[12];
                ControlAttack = pC.controlsForPlayers[13];
                ControlAbility = pC.controlsForPlayers[14];
                ControlUltimate = pC.controlsForPlayers[15];
            }

            child = Instantiate(warriors[pC.warrior[number]]);
            child.transform.tag = PlayerPrefs.GetString("Tag" + transform.name);
            child.transform.SetParent(transform);
            child.transform.localPosition = spawnPos[number];
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

    public string MovementControl
    {
        get
        {
            return movementControl;
        }

        set
        {
            movementControl = value;
        }
    }

    void SetSpawnPos()
    {
        for (int i = 0; i < 4; i++)
        {
            spawnPos[i] = mapSpawnPos[i + PlayerPrefs.GetInt("Map") * 4];
        }
    }

    void SpawnPosForMap()
    {
        //Japanese Map
        mapSpawnPos[0] = new Vector3(0, -3.8f, 0);
        mapSpawnPos[1] = new Vector3(5, -0.35f, 0);
        mapSpawnPos[2] = new Vector3(-4.8f, 1.3f, 0);
        mapSpawnPos[3] = new Vector3(0, 1.3f, 0);

        //Pirate Map
        mapSpawnPos[4] = new Vector3(-4.7f, -0.3f, 0);
        mapSpawnPos[5] = new Vector3(7.5f, -0.3f, 0);
        mapSpawnPos[6] = new Vector3(1.5f, -2.7f, 0);
        mapSpawnPos[7] = new Vector3(-1.5f, -2.7f, 0);

        //Ninja Map
        mapSpawnPos[8] = new Vector3(-4.7f, -0.3f, 0);
        mapSpawnPos[9] = new Vector3(7.5f, -0.3f, 0);
        mapSpawnPos[10] = new Vector3(1.5f, -2.7f, 0);
        mapSpawnPos[11] = new Vector3(-1.5f, -2.7f, 0);

        //Mage Map
        mapSpawnPos[12] = new Vector3(-4.7f, -0.3f, 0);
        mapSpawnPos[13] = new Vector3(7.5f, -0.3f, 0);
        mapSpawnPos[14] = new Vector3(1.5f, -2.7f, 0);
        mapSpawnPos[15] = new Vector3(-1.5f, -2.7f, 0);
    }
}
