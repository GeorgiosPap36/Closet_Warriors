using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameModes : MonoBehaviour { 

    public GameObject[] player = new GameObject[8];

    public Sprite[] warriorHead = new Sprite[8];

    public Sprite[] team = new Sprite[2];

    public GameObject livesLeft;

    private GameObject roundText;

    protected Vector3[] spawnPos = new Vector3[4];
    private Vector3[] mapSpawnPos = new Vector3[16];
    [SerializeField]  protected string[,] playerBase;

    protected GameObject scorepPanel;
    protected GameObject playersGroup;
    protected GameObject canvas;
    protected GameObject scoreUI;
    protected GameObject timer;

    private GameObject winnerPanel;

    [SerializeField] protected int[] score;
    [SerializeField] public int[] roundsWon;

    [SerializeField] protected int startingScore;
    [SerializeField] protected int endScore;
    [SerializeField] protected int playersAlive;
    protected int maxPlayersAlive;

    [SerializeField] protected int maxRounds;
    [SerializeField] protected int currentRound;

    [SerializeField] protected float timeLeft;

    protected bool endo;

    protected float roundTime;

    [SerializeField] public bool restarting;

    protected void Initialization()
    { 
        timeLeft = roundTime;
        winnerPanel = GameObject.Find("Canvas").transform.Find("WinnerPanel").gameObject;
        playersGroup = GameObject.Find("Players");
        canvas = GameObject.Find("Canvas");
        scoreUI = canvas.transform.Find("ScoresPanel").gameObject;
        scorepPanel = canvas.transform.Find("ScoresPanel").gameObject;
        timer = canvas.transform.Find("Timer").gameObject;
        currentRound = 1;
        maxRounds = PlayerPrefs.GetInt("RoundsNumber");
        roundTime = PlayerPrefs.GetInt("RoundsTime") * 90;
        timeLeft = roundTime;
        PlayersAlive = scoreUI.transform.childCount;
        SpawnPosForMap();
        SetSpawnPos();
        roundText = canvas.transform.Find("RoundText").gameObject;
        endo = false;
    }

    protected void FurUpdata()
    {
        playerBase = new string[4, 3];
        for (int i = 0; i < playersGroup.transform.childCount; i++)
        {
            if (playersGroup.transform.GetChild(i).GetChild(0).gameObject != null)
            {
                int tempInt = int.Parse(playersGroup.transform.GetChild(i).name.Substring(playersGroup.transform.GetChild(i).name.Length - 1)) - 1;
                playerBase[tempInt, 0] = playersGroup.transform.GetChild(i).name;
                playerBase[tempInt, 1] = playersGroup.transform.GetChild(i).GetChild(0).name;
                for (int j = 0; j < player.Length; j++)
                {
                    if (playerBase[tempInt, 1] == player[j].transform.name + "(Clone)")
                    {
                        playerBase[tempInt, 1] = j.ToString();
                    }
                }
                playerBase[tempInt, 2] = playersGroup.transform.GetChild(i).GetChild(0).tag;
            }
        }
    }

    public int PlayersAlive
    {
        get
        {
            return playersAlive;
        }

        set
        {
            playersAlive = value;
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

    protected bool Timer()
    {
        if (roundTime != 0)
        {
            timeLeft -= Time.deltaTime;
            timer.GetComponent<Text>().text = Mathf.CeilToInt(timeLeft) + "";
            if (timeLeft <= 0)
            {
                timeLeft = 0;
                return false;
            }
        }
        else
        {
            timer.GetComponent<Text>().text = "";
        }
        return true;
    }

    protected void RoundEnd()
    {
        if (!Timer() || (CheckLives() || endo))
        {
            if (currentRound < maxRounds)
            {
                if (!restarting)
                {
                    PlayersAlive = maxPlayersAlive;
                    StartCoroutine(RestartRound());
                }
            }
            else
            {
                if (!restarting)
                {
                    StartCoroutine(ToLocalLobby());
                }
            }
        }  
    }

    bool CheckLives()
    {
        if (!restarting)
        {
            playersAlive = maxPlayersAlive;
            foreach (int player in score)
            {
                if (startingScore > endScore)
                {
                    if (player <= endScore)
                    {
                        PlayersAlive -= 1;
                    }
                }
                else
                {
                    if (player >= endScore)
                    {
                        endo = true;
                    }
                }
            }
            if (PlayersAlive <= 1 && PlayersAlive < maxPlayersAlive)
            {
                return true;
            }
        }
            return false;
            
    }

    protected IEnumerator RestartRound()
    {
        restarting = true;
        Time.timeScale = 0;
        yield return new WaitForSeconds(2);
        DesroyBuffsAndPlayersFromLastRound();
        yield return new WaitForSeconds(1);
        SetUpNextRound();
        yield return new WaitForSeconds(2);
        roundText.SetActive(true);
        roundText.GetComponent<Text>().text = "Round" + currentRound;
        Time.timeScale = 1;
        yield return new WaitForSeconds(1f);
        roundText.SetActive(false);
        restarting = false;
    }

    void DesroyBuffsAndPlayersFromLastRound()
    {
        if (GameObject.FindGameObjectWithTag("Buffs") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Buffs"));
        }
        for (int i = 0; i < playersGroup.transform.childCount; i++)
        {
            if (playersGroup.transform.GetChild(i).childCount > 0)
            {
                Destroy(playersGroup.transform.GetChild(i).GetChild(0).gameObject);
            }
        }
    }

    void SetUpNextRound()
    {
        endo = false;
        SetUpRoundsWon();
        for (int i = 0; i < playersGroup.transform.childCount; i++)
        {
            int tempInt = int.Parse(playersGroup.transform.GetChild(i).name.Substring(playersGroup.transform.GetChild(i).name.Length - 1)) - 1;
            Debug.Log(tempInt);
            GameObject temp = Instantiate(player[int.Parse(playerBase[tempInt, 1])]);
            temp.transform.parent = playersGroup.transform.Find(playerBase[tempInt, 0]);
            temp.transform.position = spawnPos[i];
            temp.transform.tag = playerBase[tempInt, 2];
        }
        for (int i = 0; i < score.Length; i++)
        {
            score[i] = startingScore;
        }
        timeLeft = roundTime;
        currentRound += 1;
    }

    void SetUpRoundsWon()
    {
        int length = FindRoundWinner().Length;
        for (int i = 0; i < length; i++)
        {
            if (FindRoundWinner()[i])
            {
                roundsWon[i]++;
                scoreUI.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = "." + scoreUI.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>().text;
            }
        }
    }

    bool[] FindRoundWinner()
    {
        bool[] winner = new bool[score.Length];
        int maxScore = 0;
        for (int i = 0; i < score.Length; i++)
        {
            winner[i] = false;
            maxScore = Mathf.Max(maxScore, score[i]);
        }
        for (int i = 0; i < score.Length; i++)
        {
            if (maxScore == score[i])
            {
                winner[i] = true;
            }
        }
        return winner;
    }

    IEnumerator ToLocalLobby()
    {
        restarting = true;
        SetUpRoundsWon();
        yield return new WaitForSeconds(2);
        winnerPanel.SetActive(true);
    }
}
