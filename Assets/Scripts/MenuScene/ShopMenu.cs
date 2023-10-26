using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
	[SerializeField] private Image[] jumpAmplitudesUpgradesStars;
	[SerializeField] private Image[] lifesUpgradesStars;
	[SerializeField] private Button jumpButton;
	[SerializeField] private Button lifesButton;
	[SerializeField] private TMP_Text jumpCoinText;
	[SerializeField] private TMP_Text healthCoinText;
	[SerializeField] private TMPro.TMP_Text coinsAmount;
	
	public void Refresh()
	{
		SaveLoad.Load();
		RefreshUpgrades();
		coinsAmount.text = SaveLoad.Coins.ToString();
		
		if (SaveLoad.Coins < 50 || SaveLoad.JumpAmplitudeUpgrade == 3)
		{
			jumpButton.interactable = false;
		}
		
		if (SaveLoad.Coins < 100 || SaveLoad.MaximumLifesUpgrade == 3)
		{
			lifesButton.interactable = false;
		}
	}
	
	public void MaximumLifesAmountUpgrade()
	{
		SaveLoad.Coins -= 100;
		SaveLoad.MaximumLifesUpgrade++;
		SaveLoad.Save();
		Refresh();
	}
	
	public void JumpAmplitudeUpgrade()
	{
		SaveLoad.Coins -= 50;
		SaveLoad.JumpAmplitudeUpgrade++;
		SaveLoad.Save();
		Refresh();
	}
	
	private void RefreshUpgrades()
	{
		foreach (var gravity in jumpAmplitudesUpgradesStars)
		{
			gravity.color = new Color(1, 1, 1, 0);
		}
		
		for (int i = 0; i < SaveLoad.JumpAmplitudeUpgrade; i++)
		{
			jumpAmplitudesUpgradesStars[i].color = new Color(1, 1, 1, 1);
		}
		
		foreach (var life in lifesUpgradesStars)
		{
			life.color = new Color(1, 1, 1, 0);
		}
		
		for (int i = 0; i < SaveLoad.MaximumLifesUpgrade; i++)
		{
			lifesUpgradesStars[i].color = new Color(1, 1, 1, 1);
		}
	}
}
