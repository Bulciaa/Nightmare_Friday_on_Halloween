using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	public AudioMixer audioMixer;
	[SerializeField] public Slider musicSlider;
	[SerializeField] public Slider sfxSlider;	

	public void Start()
	{

		if (PlayerPrefs.HasKey("musicVolume"))
		{
		LoadVolume();
		}

		else
		{
		SetMusicVolume();
		SetSFXVolume();
		}
	
	}

  	public void SetMusicVolume ()
	{
	float volume = musicSlider.value;
	audioMixer.SetFloat("Music", volume);
	

	PlayerPrefs.SetFloat("musicVolume", volume);
	
	}

	public void SetSFXVolume ()
	{
	float volume = sfxSlider.value;
	audioMixer.SetFloat("SFX", volume);
	

	PlayerPrefs.SetFloat("SFXVolume", volume);
	
	}

	private void LoadVolume()
	{
	musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
	sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
	SetMusicVolume();
	SetSFXVolume();
	}

}

