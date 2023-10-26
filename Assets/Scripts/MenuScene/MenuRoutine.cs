using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuRoutine : MonoBehaviour
{
	private void Start()
	{
		//ClearProgress();
	}
	
	public void LoadGameScene()
	{
		SceneManager.LoadScene("GameScene");
	}
	
	private void ClearProgress()
	{
		SaveLoad.Level = 1;
		SaveLoad.JumpAmplitudeUpgrade = 0;
		SaveLoad.Coins = 100;
		SaveLoad.MaximumLifesUpgrade = 1;
		SaveLoad.FirstEntered = "y";
		SaveLoad.Volume = 1f;
		SaveLoad.Save();
	}
}
