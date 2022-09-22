using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class volumemanager : MonoBehaviour
{
    public AudioMixer audiomix;
    public Slider masterslider;
    public Slider musicslider;
    public Slider fxslider;
    void Start()
    {
        if (!PlayerPrefs.HasKey("masterVolume"))
        {
            PlayerPrefs.SetFloat("masterVolume", 0);
        }
        else
        {
            LoadMasterVolume();
        }

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0);

        }
        else
        {
            LoadMusicVolume();
        }

        if (!PlayerPrefs.HasKey("fxVolume"))
        {
            PlayerPrefs.SetFloat("fxVolume", 0);
        }
        else
        {
            LoadFXVolume();
        }
	//check the existence of all volume sliders and load their value
    }


    public void ChangeMasterVolume()
    {
        audiomix.SetFloat("Master", masterslider.value);
        SaveMasterVolume();
    }

    public void ChangeMusicVolume()
    {
        audiomix.SetFloat("Music", musicslider.value);
        SaveMusicVolume();
    }

    public void ChangeFXVolume()
    {
        audiomix.SetFloat("FX", fxslider.value);
        SaveFXVolume();
    }

    public void LoadMasterVolume()
    {
        masterslider.value = PlayerPrefs.GetFloat("masterVolume");
        audiomix.SetFloat("Master", PlayerPrefs.GetFloat("masterVolume"));
    }

    public void LoadMusicVolume()
    {
        musicslider.value = PlayerPrefs.GetFloat("musicVolume");
        audiomix.SetFloat("Music", PlayerPrefs.GetFloat("musicVolume"));
    }

    public void LoadFXVolume()
    {
        fxslider.value = PlayerPrefs.GetFloat("fxVolume");
        audiomix.SetFloat("FX", PlayerPrefs.GetFloat("fxVolume"));
    }

    public void SaveMasterVolume()
    {
        PlayerPrefs.SetFloat("masterVolume", masterslider.value);
        
    }
    public void SaveMusicVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", musicslider.value);
        
    }

    public void SaveFXVolume()
    {
        PlayerPrefs.SetFloat("fxVolume", fxslider.value);
    }
}
