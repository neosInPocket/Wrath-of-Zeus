using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
	[SerializeField] private Transform playerTransform;
	[SerializeField] private SpawnableObject lastObject;
	[SerializeField] private SpawnableObject boosterPrefab;
	[SerializeField] private SpawnableObject movingPlatformPrefab;
	[SerializeField] private float spawnDistance;
	[SerializeField] private float boosterSpawnChance;
	private SpawnableObject currentLastObject;
	private Vector2 screenBounds;
	
	private void Start()
	{
		screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		Initialize();
	}
	
	public void Initialize()
	{
		currentLastObject = lastObject;
		currentLastObject.MoveOverTrigger.MoveOver += OnPlayerMoveOver;
	}
	
	private void OnPlayerMoveOver()
	{
		currentLastObject.MoveOverTrigger.MoveOver -= OnPlayerMoveOver;
		var rndY = Random.Range(-screenBounds.y / 2, 0);
		var position = new Vector2(currentLastObject.transform.position.x + spawnDistance, rndY);
		var spawnableObject = Instantiate(ChooseObjectToSpawn(), position, Quaternion.identity, transform);
		currentLastObject = spawnableObject;
		currentLastObject.MoveOverTrigger.MoveOver += OnPlayerMoveOver;
	}
	
	private SpawnableObject ChooseObjectToSpawn()
	{
		var rnd = Random.Range(0, 1f);
		
		if (boosterSpawnChance >= rnd)
		{
			return boosterPrefab;
		}
		else
		{
			return movingPlatformPrefab;
		}
	}
}
