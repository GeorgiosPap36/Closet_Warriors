using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonsScript : MonoBehaviour
{
    private GameObject canvas;
    private GameObject mainPanel;
    private GameObject optionsPanel;
    private GameObject creditsPanel;
    private GameObject graphicsPanel;
    private GameObject soundPanel;
    private GameObject optionsButtonsPanel;

    private GameObject cureMatchMaker;
    
    private Slider musicVolume;
    private Slider sfVolume;
    private Text resolutuonText;
    private Toggle fullToggle;

    private GameObject eventS;

    private int currResol;

    Resolution[] r;
    Resolution[] resolutions;
    Resolution[] resol = new Resolution[50];

    void Start()
    {
        Cursor.visible = true;
        Initialization();
        SetFirstSelected();
        checkResolutions();
        currResol = resolutions.Length - 1;
    }

    private void Update()
    {
        Screen.fullScreen = !fullToggle.isOn;
        BackController();
        PlayerPrefs.SetFloat("MusicVolume", musicVolume.value);
        PlayerPrefs.SetFloat("SFVolume", sfVolume.value);
    }

    private void Initialization()
    {
        canvas = GameObject.Find("Canvas");
        mainPanel = canvas.transform.Find("PlayOptionsPanel").gameObject;
        optionsPanel = canvas.transform.Find("OptionsPanel").gameObject;
        creditsPanel = canvas.transform.Find("CreditsPanel").gameObject;
        graphicsPanel = optionsPanel.transform.Find("GraphicsPanel").gameObject;
        soundPanel = optionsPanel.transform.Find("SoundPanel").gameObject;
        optionsButtonsPanel = optionsPanel.transform.Find("ButtonsPanel").gameObject;
        resolutuonText = graphicsPanel.transform.Find("ResolutionText").GetComponent<Text>();
        fullToggle = graphicsPanel.transform.Find("FullscreenToggle").GetComponent<Toggle>();
        musicVolume = soundPanel.transform.Find("MusicVolume").GetComponent<Slider>();
        sfVolume = soundPanel.transform.Find("SFVolume").GetComponent<Slider>();
        musicVolume.value = PlayerPrefs.GetFloat("MusicVolume");
        sfVolume.value = PlayerPrefs.GetFloat("SFVolume");
        eventS = GameObject.Find("EventSystem");
    }

    void SetFirstSelected()
    {
        if (mainPanel.activeInHierarchy)
        {
            eventS.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(mainPanel.transform.GetChild(0).gameObject);
        }
        else if (graphicsPanel.activeInHierarchy)
        {
            eventS.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(graphicsPanel.transform.GetChild(0).gameObject);
        }
        else if (soundPanel.activeInHierarchy)
        {
            eventS.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(soundPanel.transform.GetChild(0).gameObject);
        }
        else if (optionsButtonsPanel.activeInHierarchy)
        {
            eventS.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(optionsButtonsPanel.transform.GetChild(0).gameObject);
        }
        else if (creditsPanel.activeInHierarchy)
        {
            eventS.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(creditsPanel.transform.GetChild(0).gameObject);
        }
    }

    void BackController()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (graphicsPanel.activeInHierarchy)
            {
                BackToOptionsButton();
            }
            else if (soundPanel.activeInHierarchy)
            {
                BackToOptionsButton();
            }
            else if (optionsButtonsPanel.activeInHierarchy)
            {
                BackToMainButton();
            }
            else if (creditsPanel.activeInHierarchy)
            {
                BackToMainButton();
            }
        }
    }

    ///MainMenu

    public void LocalButton()
    {
        SceneManager.LoadScene("LocalLobby");
    }

    public void OnlineButton()
    {
        if (cureMatchMaker == null)
        {
            
        }
        else
        {
            SceneManager.LoadScene("OnlineLobby");
        }
        
    }

    public void CreditsButton()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
        SetFirstSelected();
    }

    public void OptionsButton()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
        SetFirstSelected();
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void BackToMainButton()
    {
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
        SetFirstSelected();
    }

    //OptionsMenu

    public void GraphicsButton()
    {
        optionsButtonsPanel.SetActive(false);
        graphicsPanel.SetActive(true);
        SetFirstSelected();
    }

    public void SoundButton()
    {
        optionsButtonsPanel.SetActive(false);
        soundPanel.SetActive(true);
        SetFirstSelected();
    }

    public void BackToOptionsButton()
    {
        graphicsPanel.SetActive(false);
        soundPanel.SetActive(false);
        optionsButtonsPanel.SetActive(true);
        SetFirstSelected();
    }

    //GraphicsMenu

    public void LeftButton()
    {
        currResol -= 1;
        if (currResol < 0)
        {
            currResol = resolutions.Length - 1;
        }
        resolutuonText.text = ResToString(resolutions[currResol]);
        Screen.SetResolution(resolutions[currResol].width, resolutions[currResol].height, Screen.fullScreen);

    }

    public void RightButton()
    {
        currResol += 1;
        if (currResol > resolutions.Length - 1)
        {
            currResol = 0;
        }
        resolutuonText.text = ResToString(resolutions[currResol]);
        Screen.SetResolution(resolutions[currResol].width, resolutions[currResol].height, Screen.fullScreen);
    }

    void checkResolutions()
    {
        string resol1 = "";
        string resol2 = "";
        int temp =0;
        r = Screen.resolutions;
        for (int i = 0; i < r.Length; i++)
        {
            resol1 = ResToString(r[i]);
            if (resol2 != resol1)
            {
                temp = temp + 1;
                resol2 = resol1;
                resol[i] = r[i];
            }    
        }
        resolutions = new Resolution[temp];
        int temp2 = 0;
        for (int i = 0; i < resol.Length; i++)
        { 
            if (ResToString(resol[i]) != "0 x 0")
            {
                resolutions[temp2] = resol[i];
                temp2 += 1;
                if (temp2 == temp)
                {
                    break;
                }
            }
        }
    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }
}
