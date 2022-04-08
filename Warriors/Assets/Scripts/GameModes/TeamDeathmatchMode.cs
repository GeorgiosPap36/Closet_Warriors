using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamDeathmatchMode : GameModes
{

    private bool firstTimeIn;

    void Start()
    {
        Initialization();
        firstTimeIn = true;
        endScore = 0;
        startingScore = PlayerPrefs.GetInt("RoundsScore");
    }

    void LateUpdate()
    {
        if (firstTimeIn)
        {
            PlayersAlive = 2;
            maxPlayersAlive = PlayersAlive;
            score = new int[2];
            roundsWon = new int[2];
            SetStartingScore();
            for (int i = 0; i < 2; i++)
            {
                GameObject temp = Instantiate(livesLeft);
                temp.transform.SetParent(scorepPanel.transform, false);
            }
            FurUpdata();
            firstTimeIn = false;
        }
        RoundEnd();
        for (int i = 0; i < 2; i++)
        {
            scoreUI.transform.GetChild(i).GetComponent<Image>().sprite = team[i];
            scoreUI.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = " : " + score[i];
        }
    }

    void SetStartingScore()
    {
        for (int i = 0; i < score.Length; i++)
        {
            score[i] = startingScore;
            roundsWon[i] = 0;
        }
    }

    public void Respawn(string[] messagedString)
    {
        if (messagedString != null)
        {
            StartCoroutine(Spawn(messagedString));
        }
    }

    IEnumerator Spawn(string[] str)
    {
        if (score[int.Parse(str[2]) - 1] - 1 > endScore)
        {
            yield return new WaitForSeconds(1.5f);
            score[int.Parse(str[2]) - 1] -= 1;
            GameObject temp = Instantiate(player[int.Parse(str[1])]);
            temp.transform.SetParent(playersGroup.transform.Find(str[0]));
            temp.transform.localPosition = spawnPos[Random.Range(0, 3)];
            temp.transform.tag = str[0];
        }
        else
        {
            score[int.Parse(str[2]) - 1] = 0;
        }
    }
}
