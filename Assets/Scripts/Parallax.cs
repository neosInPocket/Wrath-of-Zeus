using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
	[SerializeField] private Transform cameraTransform;
	[SerializeField] private float parallaxSpeed;
	private float length;
	private float startPosition;
	
	private void Start()
	{
		startPosition = transform.position.x;
		length = GetComponent<SpriteRenderer>().bounds.size.x;
	}
	
	private void FixedUpdate()
	{
		var temp = cameraTransform.position.x * (1 - parallaxSpeed);
		var distance = cameraTransform.position.x * parallaxSpeed;
		transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);
		
		if (temp > startPosition + length)
		{
			 startPosition += length;
		}
		else
		{
			if (temp < startPosition - length)
			{
				startPosition -= length;
			}
		}
		
	}
}
