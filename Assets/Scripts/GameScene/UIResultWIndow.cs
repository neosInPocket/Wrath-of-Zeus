using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIResultWIndow : MonoBehaviour
{
	[SerializeField] private GameObject panelContainer;
	[SerializeField] private TMP_Text resultText;
	[SerializeField] private TMP_Text coinsText;
	[SerializeField] private GameObject coinContainer;
	[SerializeField] private TMP_Text buttonText;
	
	
	public void Show(bool win, int coins)
	{
		panelContainer.SetActive(true);
		
		if (!win)
		{
			resultText.text = "Lose";
			coinContainer.gameObject.SetActive(false);
			buttonText.text = "try again";
		}
		else
		{
			coinContainer.gameObject.SetActive(true);
			resultText.text = "Win";
			coinsText.text = "+" + coins;
			buttonText.text = "next level";
		}
	}
	
	public void Hide()
	{
		panelContainer.SetActive(false);
	}
}
