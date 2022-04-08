using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenController : MonoBehaviour {

    [SerializeField] private GameObject loadingScreenObj;

    [SerializeField] private Slider slider;

    AsyncOperation aSync;
	
	public void LoadScreen(int i)
    {
        StartCoroutine(LoadingScreen(i));
    }

    IEnumerator LoadingScreen(int i)
    {
        loadingScreenObj.SetActive(true);
        aSync = SceneManager.LoadSceneAsync(i);
        aSync.allowSceneActivation = false;

        while (aSync.isDone == false)
        {
            slider.value = aSync.progress;
            if (aSync.progress == 0.9f)
            {
                slider.value = 1f;
                aSync.allowSceneActivation = true;
            }
             yield return null;
        }

    }
}
