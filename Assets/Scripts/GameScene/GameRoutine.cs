using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRoutine : MonoBehaviour
{
	[SerializeField] private TMP_Text levelText;
	[SerializeField] private UIHealthRender uiHealth;
	[SerializeField] private UIProgressRenderer uiLevelProgressBar;
	[SerializeField] private Player player;
	[SerializeField] private GameTutorialWindow tutorial;
	[SerializeField] private UIPreambleWindow uiCountDownPanel;
	[SerializeField] private UIResultWIndow uiGameWinLose;
	[SerializeField] private Transform objectsContainer;
	private int currentLifesAmount;
	private int currentPoints;
	private int levelMaxPoints;
	private int levelMaxCoins;
	
	private void Start()
	{
		Init();
	}
	
	public void Init()
	{
		player.DamageTriggerEntered += OnPlayerGotDamageHandler;
		player.CoinCollected += OnPlayerAddedCoin;
		ClearObstaclesContainer();
		
		CheckValidLevelValue();
		currentLifesAmount = SaveLoad.MaximumLifesUpgrade;
		currentPoints = 0;
		levelMaxPoints = CalculateLevelMaxPoints();
		levelMaxCoins = CalculateLevelCoins();
		
		levelText.text = "Level " + SaveLoad.Level;
		GUIRefresh();
		
		if (SaveLoad.FirstEntered == "y")
		{
			SaveLoad.FirstEntered = "n";
			SaveLoad.Save();
			tutorial.OnTutorialWindowEnd += OnTutorialEnd;
			tutorial.Play();
		}
		else
		{
			CountDown();
		}
	}
	
	private void OnTutorialEnd()
	{
		CountDown();
	}
	
	private void CountDown()
	{
		uiCountDownPanel.OnCountEndAction += OnCountDownEnd;
		uiCountDownPanel.Play();
	}
	
	private void OnCountDownEnd()
	{
		uiCountDownPanel.OnCountEndAction -= OnCountDownEnd;
		player.Enable();
	}
	
	private void OnPlayerGotDamageHandler()
	{
		currentLifesAmount--;
		GUIRefresh();
		CheckIsLoseResult();
	}
	
	private void OnPlayerAddedCoin()
	{
		currentPoints += 3;
		GUIRefresh();
		CheckIsWinResult();
	}
	
	private void CheckIsWinResult()
	{
		if (currentPoints >= levelMaxPoints)
		{
			currentPoints = levelMaxPoints;
			GUIRefresh();
			SaveLoad.Level += 1;
			SaveLoad.Coins += levelMaxCoins;
			SaveLoad.Save();
			uiGameWinLose.Show(true, levelMaxCoins);
			
			player.Disable();
			
			player.DamageTriggerEntered -= OnPlayerGotDamageHandler;
			player.CoinCollected -= OnPlayerAddedCoin;
		}
	}
	
	private void GUIRefresh()
	{
		uiHealth.UpdateHealth(currentLifesAmount);
		uiLevelProgressBar.UpdateProgress(currentPoints, levelMaxPoints);
	}
	
	private int CalculateLevelMaxPoints()
	{
		var currentLevel = SaveLoad.Level;
		return (int)(currentLevel * Mathf.Log(currentLevel) + 6);
	}
	
	public void ReturnToMainMenu()
	{
		SceneManager.LoadScene("MainMenuScene");
	}
	
	private int CalculateLevelCoins()
	{
		var level = SaveLoad.Level;
		return (int)(Mathf.Pow(level, 1/4) * Mathf.Log(100 * level) + 53);
	}
	
	public void ClearObstaclesContainer()
	{
		foreach (Transform child in objectsContainer)
		{
			Destroy(child.gameObject);
		}
	}
	
	private void CheckIsLoseResult()
	{
		if (currentLifesAmount != 0)
		{
			player.PlayDamageCoroutine();
			ClearObstaclesContainer();
		}
		else
		{
			uiGameWinLose.Show(false, 0);
			player.Disable();
			player.DamageTriggerEntered -= OnPlayerGotDamageHandler;
			player.CoinCollected -= OnPlayerAddedCoin;
		}
	}
	
	private void CheckValidLevelValue()
	{
		if (SaveLoad.Level == 0)
		{
			SaveLoad.Level = 1;
			SaveLoad.Save();
		}
	}
}
