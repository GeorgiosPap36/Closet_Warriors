using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

    private GameObject panel;

    public int paused;

	void Start ()
    {
        paused = 1;
        panel = transform.Find("PausePanel").gameObject;
	}
	
	void Update ()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused *= -1;
        }
        if (paused == -1)
        {
            Time.timeScale = 0;
            panel.SetActive(true);
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            panel.SetActive(false);
            Cursor.visible = false;
        }
	}

    public void Restart()
    {
        SceneManager.LoadScene("Arena");
        paused = 1;
    }

    public void CharacterSelection()
    {  
        SceneManager.LoadScene("LocalLobby");
        paused = 1;
    }

    public void MainMenu()
    {     
        SceneManager.LoadScene("Menu");
        paused = 1;
    }

}
