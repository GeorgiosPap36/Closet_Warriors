using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScoreMode : GameModes
{

    private bool firstTimeIn;

    private float time;

    void Start()
    {
        Initialization();
        time = 10;
        firstTimeIn = true;
        endScore = 0;
        startingScore = PlayerPrefs.GetInt("RoundsScore");
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
            scoreUI.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = " : " + score[i];
        }
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            ShuffleScores();
            time = 10;
        }
    }

    void ShuffleScores()
    {
        for (int i = score.Length - 1; i > 0; i--)
        {
            int r = Random.Range(0, i);
            int tmp = score[i];
            score[i] = score[r];
            score[r] = tmp;
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
