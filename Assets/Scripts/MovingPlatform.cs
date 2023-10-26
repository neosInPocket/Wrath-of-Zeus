using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : SpawnableObject
{
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float freq;
	private float amplitude;
	private float currentTime;
	
	private void Start()
	{
		amplitude = 3f * freq;
		transform.position = new Vector2(transform.position.x, -amplitude / freq);
	}
	
	private void Update()
	{
		rb.velocity = new Vector2(rb.velocity.x, amplitude * Mathf.Sin(freq * currentTime));
		currentTime += Time.deltaTime;
	}
}
