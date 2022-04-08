using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUpWinnerPanel : GameModes{

    [SerializeField] private GameObject winnerText;
    private GameModes mod;
	
	void OnEnable ()
    {
        mod = GameObject.FindGameObjectWithTag("Mode").GetComponent<GameModes>();
        DisplayWinners();
    }
	
	void DisplayWinners()
    {
        int maxRoundsWon = 0;
        for (int i = 0; i < mod.roundsWon.Length; i++)
        {
            maxRoundsWon = Mathf.Max(maxRoundsWon, mod.roundsWon[i]);
        }
        for (int i = 0; i < mod.roundsWon.Length; i++)
        {
            if (mod.roundsWon[i] == maxRoundsWon)
            {
                if (PlayerPrefs.GetInt("Mode") == 1)
                {
                    Instantiate(winnerText, transform.Find("Winners"), false).GetComponent<Text>().text = "Team" + (i + 1);
                }
                else
                {
                    Instantiate(winnerText, transform.Find("Winners"), false).GetComponent<Text>().text = GameObject.Find("Players").transform.GetChild(i).name;
                }
            }
        }
    }
}
