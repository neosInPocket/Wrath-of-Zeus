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
	[SerializeField] private PlayerController playerController;
	[SerializeField] private GameTutorialWindow tutorial;
	[SerializeField] private UIPreambleWindow preambleWindow;
	[SerializeField] private UIResultWIndow uiGameWinLose;
	[SerializeField] private Transform objectsContainer;
	[SerializeField] private ObjectSpawner objectSpawner;
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
		playerController.DamageTriggerEntered += OnPlayerGotDamageHandler;
		playerController.CoinCollected += OnPlayerAddedCoin;
		ClearObstaclesContainer();
		playerController.Initialize();
		objectSpawner.Initialize();
		
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
		preambleWindow.OnCountEndAction += OnCountDownEnd;
		preambleWindow.gameObject.SetActive(true);
	}
	
	private void OnCountDownEnd()
	{
		preambleWindow.OnCountEndAction -= OnCountDownEnd;
		playerController.Enable();
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
			
			playerController.Disable();
			
			playerController.DamageTriggerEntered -= OnPlayerGotDamageHandler;
			playerController.CoinCollected -= OnPlayerAddedCoin;
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
	
	public void GoToMainMenu()
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
			playerController.PlayDamageCoroutine();
			playerController.ReturnToSaveBooster();
		}
		else
		{
			playerController.PlayDeathCoroutine();
			uiGameWinLose.Show(false, 0);
			playerController.Disable();
			playerController.DamageTriggerEntered -= OnPlayerGotDamageHandler;
			playerController.CoinCollected -= OnPlayerAddedCoin;
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
