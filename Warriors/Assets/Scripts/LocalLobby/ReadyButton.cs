using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadyButton : MonoBehaviour {

    private GetKeyPressed getK;

    void Start()
    {
        getK = GameObject.Find("GetButtonPressed").GetComponent<GetKeyPressed>();
    }

    void Update()
    {

    }

    public void Ready()
    {
        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetString("ControlP" + (i + 1), getK.PlayerControl[i]);
            PlayerPrefs.SetInt("WarriorP" + (i + 1), getK.PlayerChild[i]);
        }
        SceneManager.LoadScene("Arena");
    }
}
