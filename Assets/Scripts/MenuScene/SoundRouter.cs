using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRouter : MonoBehaviour
{
	[SerializeField] private AudioSource musicAudioSource;
	[SerializeField] private AudioSource boosterTouched;
	
	private void Start()
	{
		SaveLoad.Load();
		musicAudioSource.volume = SaveLoad.Volume;
	}
	
	public void SaveVolumeValue()
	{
		SaveLoad.Volume = musicAudioSource.volume;
		SaveLoad.Save();
	}
	
	public void ChangeVolume(float volume)
	{
		musicAudioSource.volume = volume;
	}
	
	public void PlaySound()
	{
		boosterTouched.Play();
	}
}
