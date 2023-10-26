using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	[SerializeField] private GameObject CollectEffectPrefab;
	[SerializeField] private SpriteRenderer spriteRenderer;
	private bool isCollected;
	public bool IsCollected
	{
		get => isCollected;
		set => isCollected = value;
	}
	
	public void PlayCollectEffect()
	{
		StartCoroutine(CollectEffect());
	}
	
	private IEnumerator CollectEffect()
	{
		spriteRenderer.color = new Color(0, 0, 0, 0);
		var effect = Instantiate(CollectEffectPrefab, transform.position, Quaternion.identity, transform);
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
