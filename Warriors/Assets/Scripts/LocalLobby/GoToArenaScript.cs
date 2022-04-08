using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToArenaScript : MonoBehaviour {

    private GameObject loadScreenObj;

    private GetKeyPressed getK;

    private Text timerTxt;
    private GameObject playersGroup;

    private bool loadingLevel;

    private float time;

	void Start ()
    {
        getK = GameObject.Find("GetButtonPressed").GetComponent<GetKeyPressed>();
        playersGroup = GameObject.Find("Players");
        timerTxt = GameObject.Find("Canvas").transform.Find("Countdown").GetComponent<Text>();
        loadScreenObj = GameObject.Find("LoadingScreenController");
        loadingLevel = false;
        time = 5;
	}
	
	void Update ()
    {
        GoToArena();
	}

    bool CheckIfReady()
    {
        bool tempR = false;
        if (playersGroup.transform.childCount >= 1)
        {
            tempR = true;
            for (int i = 0; i < playersGroup.transform.childCount; i++)
            {
                tempR = tempR && getK.PlayersReady[int.Parse(playersGroup.transform.GetChild(i).transform.name.Substring(1, 1)) - 1];
            }
        }
        return tempR;
    }

    void GoToArena()
    {
        if (CheckIfReady())
        {
            timerTxt.gameObject.SetActive(true);
            timerTxt.text = Mathf.Ceil(time) + "";
            time -= Time.deltaTime;
        }
        else
        {
            timerTxt.gameObject.SetActive(false);
            time = 5;
        }
        if (time <= 0.0f)
        {
            for (int i = 0; i < 4; i++)
            {
                PlayerPrefs.SetString("ControlP" + (i + 1), getK.PlayerControl[i]);
                PlayerPrefs.SetInt("WarriorP" + (i + 1), getK.PlayerChild[i]);
            }
            if (!loadingLevel)
            {
                loadingLevel = true;
                loadScreenObj.SendMessage("LoadScreen", 2);
            }
        }
    }
}
