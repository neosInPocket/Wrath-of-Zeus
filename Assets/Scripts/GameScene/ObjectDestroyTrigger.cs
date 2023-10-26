using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyTrigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<SpawnableObject>(out SpawnableObject spawnable))
		{
			if (spawnable.IsStatic) return;
			Destroy(spawnable.gameObject);
		}
	}
}
