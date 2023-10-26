using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressRenderer : MonoBehaviour
{
    [SerializeField] private Image innerImage;
	[SerializeField] private TMP_Text caption;
	
	public void UpdateProgress(float currentProgress, float allProgress)
	{
		caption.text = "Level " + SaveLoad.Level;
		float value = currentProgress / allProgress;
		innerImage.fillAmount = value;
	}
}
