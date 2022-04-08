using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuggernaughtMode : GameModes {

    [SerializeField] private GameObject mage;

    [SerializeField] public GameObject mageObj;

    private Vector3 oGPos;
    private Vector3 oGScale;

    [SerializeField] private string[] oGIdentity = new string[3];

    private string attackerN;
    private string deadMan;

    private bool firstTimeIn;

    void Start()
    {
        Initialization();
        firstTimeIn = true;
        endScore = PlayerPrefs.GetInt("RoundsScore");
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

    void Attacker(int a)
    {
        SpawnMage(a);
    }

    void Killer(string aName)
    {
        attackerN = aName;
    }

    void DeadManName(string name)
    {
        deadMan = name;
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
            yield return new WaitForSeconds(1.25f);
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

    void SpawnMage(int a)
    {
        GameObject original = GameObject.FindGameObjectWithTag("P" + (a + 1));
        if (attackerN == "Mage(Clone)" || deadMan == "Mage(Clone)")
        {
            score[a] += 1;
        }
        if (mageObj == null)
        {
            mageObj = Instantiate(mage, original.transform.parent);
            oGPos = original.transform.position;
            oGScale = original.transform.localScale;
            for (int i = 0; i < 3; i++)
            {
                oGIdentity[i] = original.GetComponent<Warrior>().identity[i];
            }
            Destroy(original);
            mageObj.transform.tag = "P" + (a + 1);
            mageObj.transform.position = oGPos;
            mageObj.transform.localScale = oGScale;
            for (int i = 0; i < 3; i++)
            {
                mageObj.transform.GetComponent<Warrior>().identity[i] = oGIdentity[i];
            }
        }
    }
}
