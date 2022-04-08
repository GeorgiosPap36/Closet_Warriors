using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSoundEffectsVolume : MonoBehaviour {

    private AudioSource audioZ;

    void Start()
    {
        audioZ = GetComponent<AudioSource>();

    }

    void Update()
    {
        audioZ.volume = PlayerPrefs.GetFloat("SFVolume");
    }
}
