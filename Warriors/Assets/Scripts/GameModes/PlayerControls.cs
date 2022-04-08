using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    [SerializeField] public string[] controlsForPlayers = new string[16];

    [SerializeField] public int[] warrior = new int[4];

    [SerializeField] List<int> availablePositions = new List<int>();

    [SerializeField] private string selected;
    [SerializeField] private string newSelected;

    [SerializeField] private int a;
    [SerializeField] private int b;

    [SerializeField] private int listLenght;
    [SerializeField] private int index;

    void Awake()
    {
        SetUpWarrior();
        SetUpControlForPlayers();
        if (PlayerPrefs.GetInt("Mode") == 2)
        {
            ShuffleControls();
        }
        else if (PlayerPrefs.GetInt("Mode") == 3)
        {
            ShuffleWarriors();
        }
    }




    //!DANGER! ONLY SPECIALLY TRAINED STAFF ALLOWED  !DANGER!
    //DO NOT OPEN UNDER ANY CIRCUMSTANCIES 
    void SetUpControlForPlayers()
    {
        for (int i = 0; i < 16; i++)
        {
            if (i >= 0 && i < 4)
            {
                if (PlayerPrefs.GetString("ControlP" + 1) != "")
                {
                    if (i % 4 == 0)
                    {
                        controlsForPlayers[i] = "Jump";
                    }
                    else if (i % 4 == 1)
                    {
                        controlsForPlayers[i] = "Attack";
                    }
                    else if (i % 4 == 2)
                    {
                        controlsForPlayers[i] = "Ability";
                    }
                    else if (i % 4 == 3)
                    {
                        controlsForPlayers[i] = "Ultimate";
                    }
                    controlsForPlayers[i] += PlayerPrefs.GetString("ControlP" + 1);
                }
            }
            else if (i >= 4 && i < 8)
            {
                if (PlayerPrefs.GetString("ControlP" + 2) != "")
                {
                    if (i % 4 == 0)
                    {
                        controlsForPlayers[i] = "Jump";
                    }
                    else if (i % 4 == 1)
                    {
                        controlsForPlayers[i] = "Attack";
                    }
                    else if (i % 4 == 2)
                    {
                        controlsForPlayers[i] = "Ability";
                    }
                    else if (i % 4 == 3)
                    {
                        controlsForPlayers[i] = "Ultimate";
                    }
                    controlsForPlayers[i] += PlayerPrefs.GetString("ControlP" + 2);
                }
            }
            else if (i >= 8 && i < 12)
            {
                if (PlayerPrefs.GetString("ControlP" + 3) != "")
                {
                    if (i % 4 == 0)
                    {
                        controlsForPlayers[i] = "Jump";
                    }
                    else if (i % 4 == 1)
                    {
                        controlsForPlayers[i] = "Attack";
                    }
                    else if (i % 4 == 2)
                    {
                        controlsForPlayers[i] = "Ability";
                    }
                    else if (i % 4 == 3)
                    {
                        controlsForPlayers[i] = "Ultimate";
                    }
                    controlsForPlayers[i] += PlayerPrefs.GetString("ControlP" + 3);
                }
            }
            else if (i >= 12 && i < 16)
            {
                if (PlayerPrefs.GetString("ControlP" + 4) != "")
                {
                    if (i % 4 == 0)
                    {
                        controlsForPlayers[i] = "Jump";
                    }
                    else if (i % 4 == 1)
                    {
                        controlsForPlayers[i] = "Attack";
                    }
                    else if (i % 4 == 2)
                    {
                        controlsForPlayers[i] = "Ability";
                    }
                    else if (i % 4 == 3)
                    {
                        controlsForPlayers[i] = "Ultimate";
                    }
                    controlsForPlayers[i] += PlayerPrefs.GetString("ControlP" + 4);
                }
            }
        }
    }
    //WE HAVE HIM SURROUNDED
    //I REPEAT!! DO NOT ENTER!!




    void SetUpWarrior()
    {
        for (int i = 0; i < 4; i++)
        {
            warrior[i] = PlayerPrefs.GetInt("WarriorP" + (i + 1));
        }
    }

    void ShuffleWarriors()
    {
        FindAvailabeIndexesWarrior();

        listLenght--;

        a = warrior[availablePositions[0]];
        availablePositions.RemoveAt(0);
        b = 0;

        index = Random.Range(0, listLenght - 1);

        do
        {
            b = warrior[availablePositions[index]];
            warrior[availablePositions[index]] = a;
            availablePositions.RemoveAt(index);
            a = b;
            index = Random.Range(0, listLenght - 1);
            listLenght--;
        } while (listLenght > 1);
        warrior[0] = b;
    }

    void FindAvailabeIndexesWarrior()
    {
        listLenght = 0;
        for (int i = 0; i < 4; i++)
        {
            if (warrior[i] != -1)
            {
                availablePositions.Add(i);
                listLenght++;
            }
        }
    }

    void ShuffleControls()
    {
        FindAvailableIndexesControls();
        listLenght--;

        selected = controlsForPlayers[availablePositions[0]];
        availablePositions.RemoveAt(0);
        newSelected = "";

        index = Random.Range(0, listLenght - 1);

        do
        {
            newSelected = controlsForPlayers[availablePositions[index]];
            controlsForPlayers[availablePositions[index]] = selected;
            availablePositions.RemoveAt(index);
            selected = newSelected;
            index = Random.Range(0, listLenght - 1);
            listLenght--;
        } while (listLenght > 1);
        controlsForPlayers[0] = newSelected;
    }

    void FindAvailableIndexesControls()
    {
        listLenght = 0;
        for (int i = 0; i < 16; i++)
        {
            if (controlsForPlayers[i] != "")
            {
                availablePositions.Add(i);
                listLenght++;
            }
        }
    }

}
