using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOverTrigger : MonoBehaviour
{
	[SerializeField] private float destroyDistance = 10; 
	public Action MoveOver;
	public Action Destroyed;
	private Transform playerTransform;
	private bool isDestroying;
	
	public void RaiseMoveOverEvent()
	{
		MoveOver?.Invoke();
	}
	
	private void Update()
	{
		if (!isDestroying) return;
		
		if (playerTransform.position.x - destroyDistance > transform.position.x)
		{
			var spObject = GetComponentInParent<SpawnableObject>();
			if (spObject.IsStatic) return;
			
			Destroy(spObject.gameObject);
		}
	}
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<Player>(out Player player))
		{
			playerTransform = player.transform;
			isDestroying = true;
			RaiseMoveOverEvent();
		}
	}
}
