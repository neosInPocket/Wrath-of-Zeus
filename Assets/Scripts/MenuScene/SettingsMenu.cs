using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	[SerializeField] private Slider volumeSlider;
	
	public void RefreshVolume()
	{
		volumeSlider.value = SaveLoad.Volume;
	}
}
