using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	[SerializeField] private GameObject CollectEffectPrefab;
	private bool isCollected;
	public bool IsCollected => isCollected;
	
	public void PlayCollectEffect()
	{
		StartCoroutine(CollectEffect());
	}
	
	private IEnumerator CollectEffect()
	{
		var effect = Instantiate(CollectEffectPrefab, transform.position, Quaternion.identity, transform);
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
