using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public static int Level;
	public static int Coins;
	public static int MaximumLifesUpgrade;
	public static int SpeedUpgrade;
	public static float Volume;
	public static string FirstEntered;
	
	public static void Save()
	{
		PlayerPrefs.SetInt("Coins", Coins);
		PlayerPrefs.SetInt("Level", Level);
		PlayerPrefs.SetInt("MaximumLifesUpgrade", MaximumLifesUpgrade);
		PlayerPrefs.SetInt("SpeedUpgrade", SpeedUpgrade);
		PlayerPrefs.SetFloat("Volume", Volume);
		PlayerPrefs.SetString("FirstEntered", FirstEntered);
	}
	
	public static void Load()
	{
		Level = PlayerPrefs.GetInt("Level", 1);
		Coins = PlayerPrefs.GetInt("Coins", 100);
		MaximumLifesUpgrade = PlayerPrefs.GetInt("MaximumLifesUpgrade", 1);
		SpeedUpgrade = PlayerPrefs.GetInt("SpeedUpgrade", 0);
		FirstEntered = PlayerPrefs.GetString("FirstEntered", "y");
		Volume = PlayerPrefs.GetFloat("Volume", 1f);
	}
}
