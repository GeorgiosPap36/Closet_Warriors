using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeMaster : MonoBehaviour {

    public Sprite[] maps = new Sprite[2];

    private GetKeyPressed getK;

    private GameObject canvas;
    private GameObject gModePanel;

    private Text modeTxt;

    private Slider scoreInput;
    private Slider timeInput;
    private Slider roundsInput;

    private Text score;
    private Text roundNumber;
    private Text roundTime;

    private Toggle fountainsToggle;
    private Toggle buffsToggle;
    private Toggle invisibilityToggle;

    private Image mapImage;

    private string[] timeOptions = new string[5];

    [SerializeField] private string[] gameModes = new string[6];
    
    [SerializeField] private int selectedMode;
    [SerializeField] private int selectedMap;

    void Start ()
    {
        getK = GameObject.Find("GetButtonPressed").GetComponent<GetKeyPressed>();
        SetUpModesString();
        SetTimeOptions();
        canvas = GameObject.Find("Canvas");
        gModePanel = canvas.transform.Find("GameModePanel").gameObject;
        modeTxt = gModePanel.transform.Find("Mode").GetComponent<Text>();
        mapImage = gModePanel.transform.Find("Map").GetComponent<Image>();
        scoreInput = gModePanel.transform.Find("Score").Find("Slider").GetComponent<Slider>();
        score = scoreInput.gameObject.transform.parent.GetChild(1).GetComponent<Text>();
        scoreInput.value = 20;
        score.text = scoreInput.value.ToString();
        roundsInput = gModePanel.transform.Find("RoundsNumber").Find("Slider").GetComponent<Slider>();
        roundNumber = roundsInput.gameObject.transform.parent.GetChild(1).GetComponent<Text>();
        roundsInput.value = 1;
        roundNumber.text = roundsInput.value.ToString();
        timeInput = gModePanel.transform.Find("RoundTime").Find("Slider").GetComponent<Slider>();
        roundTime = timeInput.gameObject.transform.parent.GetChild(1).GetComponent<Text>();
        timeInput.value = 0;
        roundTime.text = timeOptions[(int)timeInput.value];
        fountainsToggle = gModePanel.transform.Find("FountainsToggle").GetComponent<Toggle>();
        buffsToggle = gModePanel.transform.Find("BuffsToggle").GetComponent<Toggle>();
        invisibilityToggle = gModePanel.transform.Find("InvisibilityToggle").GetComponent<Toggle>();
        invisibilityToggle.isOn = false;
        selectedMode = 0;
        selectedMap = 0;
    }

	void Update ()
    {
        modeTxt.text = gameModes[selectedMode];
        mapImage.sprite = maps[selectedMap];
        score.text = scoreInput.value.ToString();
        roundTime.text = timeOptions[(int)timeInput.value];
        roundNumber.text = roundsInput.value.ToString();
        ActivateDeactivateModePanel();
        SetPlayerPrefs();
    }

    void ActivateDeactivateModePanel()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (gModePanel.activeInHierarchy)
            {
                gModePanel.SetActive(false);
            }
            else
            {
                gModePanel.SetActive(true);
            }
        }
    }

    void SetUpModesString()
    {
        gameModes[0] = "FFA";
        gameModes[1] = "Team Deathmatch";
        gameModes[2] = "Random Controls";
        gameModes[3] = "Random Warrior";
        gameModes[4] = "KDA";
        gameModes[5] = "Change Score";
        gameModes[6] = "Juggernaught";
    }

    public void LeftArrowForModes()
    {
        if (!CheckNumberOfReadyPlayers())
        {
            selectedMode -= 1;
            if (selectedMode < 0)
            {
                selectedMode = gameModes.Length - 1;
            }
        }
    }

    public void RightArrowForModes()
    {
        if (!CheckNumberOfReadyPlayers())
        {
            selectedMode += 1;
            if (selectedMode > gameModes.Length - 1)
            {
                selectedMode = 0;
            }
        }
    }

    bool CheckNumberOfReadyPlayers()
    {
        bool shouldChangeMode = getK.PlayersReady[0] || getK.PlayersReady[1] || getK.PlayersReady[2] || getK.PlayersReady[3];
        return shouldChangeMode;
    }

    public void LeftArrowForMap()
    {
        selectedMap -= 1;
        if (selectedMap < 0)
        {
            selectedMap = maps.Length - 1;
        }
    }

    public void RightArrowForMap()
    {
        selectedMap += 1;
        if (selectedMap > maps.Length - 1)
        {
            selectedMap = 0;
        }
    }

    void SetPlayerPrefs()
    {
        PlayerPrefs.SetInt("RoundsNumber", (int) roundsInput.value);
        PlayerPrefs.SetInt("RoundsTime", (int) timeInput.value);
        PlayerPrefs.SetInt("RoundsScore", (int) scoreInput.value);
        PlayerPrefs.SetInt("Mode", selectedMode);
        PlayerPrefs.SetInt("Map", selectedMap);
        if (fountainsToggle.isOn)
        {
            PlayerPrefs.SetInt("FountainsToggle", 1);
        }
        else
        {
            PlayerPrefs.SetInt("FountainsToggle", 0);
        }

        if (buffsToggle.isOn)
        {
            PlayerPrefs.SetInt("BuffsToggle", 1);
        }
        else
        {
            PlayerPrefs.SetInt("BuffsToggle", 0);
        }

        if (invisibilityToggle.isOn)
        {
            PlayerPrefs.SetInt("InvisibilityToggle", 1);
        }
        else
        {
            PlayerPrefs.SetInt("InvisibilityToggle", 0);
        }
    }

    void SetTimeOptions()
    {
        timeOptions[0] = "No time limt";
        timeOptions[1] = "1 : 30";
        timeOptions[2] = "3 : 00";
        timeOptions[3] = "4 : 30";
        timeOptions[4] = "6 : 00";
    }
}
