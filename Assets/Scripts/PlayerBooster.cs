using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBooster : SpawnableObject
{
	[SerializeField] private Transform coinSpawnPosition;
	[SerializeField] private Coin coinPrefab;
	[SerializeField] private float coinSpawnChance = 0.5f;
	
	private void Start()
	{
		var rnd = Random.Range(0, 1f);
		
		if (coinSpawnChance > rnd)
		{
			Instantiate(coinPrefab, coinSpawnPosition.position, Quaternion.identity, transform);
		}
	}
}
