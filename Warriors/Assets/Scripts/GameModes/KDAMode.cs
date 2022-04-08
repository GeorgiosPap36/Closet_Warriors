using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KDAMode : GameModes
{

    private bool firstTimeIn;

    void Start()
    {
        Initialization();
        firstTimeIn = true;
        endScore = PlayerPrefs.GetInt("RoundsScore"); ;
        startingScore = 0;
    }

    void LateUpdate()
    {
        if (firstTimeIn)
        {
            PlayersAlive = playersGroup.transform.childCount;
            maxPlayersAlive = PlayersAlive;
            score = new int[playersGroup.transform.childCount];
            roundsWon = new int[playersGroup.transform.childCount];
            SetStartingScore();
            for (int i = 0; i < score.Length; i++)
            {
                GameObject temp = Instantiate(livesLeft);
                temp.transform.SetParent(scorepPanel.transform, false);
            }
            FurUpdata();
            int internalCounter = 0;
            for (int i = 0; i < 4; i++)
            {
                if (playerBase[i, 1] != null)
                {
                    scoreUI.transform.GetChild(internalCounter).GetComponent<Image>().sprite = warriorHead[int.Parse(playerBase[i, 1])];
                    internalCounter++;
                }
            }
            firstTimeIn = false;
        }
        RoundEnd();
        for (int i = 0; i < playersGroup.transform.childCount; i++)
        {
            scoreUI.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = "P" + (i + 1) + " : " + score[i];
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

    void Attacker(int at)
    {
        StartCoroutine(IncreaseScore(at));
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
        if (score[int.Parse(str[2]) - 1] - 1 < endScore)
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
            endo = true;
        }
    }

    IEnumerator IncreaseScore(int a)
    {
        Debug.Log(0);
        score[a] += 1;
        yield return new WaitForSeconds(0);
    }
}
