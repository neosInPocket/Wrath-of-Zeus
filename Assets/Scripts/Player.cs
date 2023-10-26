using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Player : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float maxXSpeed;
	[SerializeField] private float bouncePower;
	[SerializeField] private float brakeSpeed = 2;
	private float currentTime;
	public float CurrentTime => currentTime;
	private bool isMoving;
	public bool IsMoving => isMoving;
	public Rigidbody2D RigidBody => rb;
	
	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		Enable();
	}
	
	private void Update()
	{
		if (!isMoving)
		{
			if (currentTime > 0)
			{
				if (currentTime - Time.deltaTime < 0)
				{
					currentTime = 0;
				}
				else
				{
					currentTime -= Time.deltaTime;
				}
				
				rb.velocity = new Vector2(CalculateXSpeed(currentTime), rb.velocity.y);
			}
			
			return;
		}
		
		if (rb.velocity.x > maxXSpeed) return;
		
		currentTime += Time.deltaTime;
		rb.velocity = new Vector2(CalculateXSpeed(currentTime), rb.velocity.y);
	}
	
	private void MovePlayer(Finger finger)
	{
		currentTime = 0;
		isMoving = true;
	}
	
	private void ReleasePlayer(Finger finger)
	{
		isMoving = false;
	}
	
	private float CalculateXSpeed(float time)
	{
		return Mathf.Log(Mathf.Pow(time + 0.607f, 20)) + 10;
	}
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<PlayerBooster>(out PlayerBooster booster))
		{
			rb.velocity = new Vector2(rb.velocity.x, bouncePower);
		}
		
		if (collider.TryGetComponent<Coin>(out Coin coin))
		{
			if (coin.IsCollected) return;
			coin.PlayCollectEffect();
		}
	}
	
	public void Enable()
	{
		Touch.onFingerDown += MovePlayer;
		Touch.onFingerUp += ReleasePlayer;
	}
	
	public void Disable()
	{
		Touch.onFingerDown -= MovePlayer;
		Touch.onFingerUp -= ReleasePlayer;
	}
	
	private void OnDestroy()
	{
		EnhancedTouchSupport.Disable();
		TouchSimulation.Disable();
		Disable();
	}
}