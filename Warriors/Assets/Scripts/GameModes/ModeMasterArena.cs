using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeMasterArena : MonoBehaviour {

    public GameObject[] modes = new GameObject[2];

    public GameObject[] maps = new GameObject[2];

    void Awake ()
    {
        if (PlayerPrefs.GetInt("Mode") == 0 || PlayerPrefs.GetInt("Mode") == 2 || PlayerPrefs.GetInt("Mode") == 3)
        {
            Instantiate(modes[0]);
        }
        else
        {
            Instantiate(modes[PlayerPrefs.GetInt("Mode")]);
        }
       
        Instantiate(maps[PlayerPrefs.GetInt("Map")]).transform.position = Vector3.zero;
    }

}
