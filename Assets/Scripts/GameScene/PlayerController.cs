using System;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.VFX;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Transform spawnPosition;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float maxXSpeed;
	[SerializeField] private float bouncePower;
	[SerializeField] private float brakeSpeed = 2;
	[SerializeField] private PlayerBooster safeBooster; 
	[SerializeField] private GameObject trail;
	[SerializeField] private VisualEffect effect; 
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private GameObject deathEffectPrefab; 
	private float currentTime;
	public float CurrentTime => currentTime;
	private bool isMoving;
	public bool IsMoving
	{
		get => isMoving;
		set => isMoving = value;	
	}
	public Rigidbody2D RigidBody => rb;
	public Action DamageTriggerEntered;
	public Action CoinCollected;
	private PlayerBooster lastBooster;
	private float[] jumpUpgrade = { 7, 8, 9, 10 };
	
	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		lastBooster = safeBooster;
		Initialize();
	}
	
	public void Initialize()
	{
		bouncePower = jumpUpgrade[SaveLoad.JumpAmplitudeUpgrade];
		rb.velocity = Vector2.zero;
		rb.angularVelocity = 0;
		transform.position = spawnPosition.position;
		rb.velocity = Vector2.zero;
		rb.angularVelocity = 0;
		spriteRenderer.color = new Color(1, 1, 1, 1);
		trail.gameObject.SetActive(true);
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
			lastBooster = booster;
		}
		
		if (collider.TryGetComponent<Coin>(out Coin coin))
		{
			if (coin.IsCollected) return;
			coin.IsCollected = true;
			coin.PlayCollectEffect();
			CoinCollected?.Invoke();
		}
		
		if (collider.TryGetComponent<DamageTrigger>(out DamageTrigger damageTrigger))
		{
			DamageTriggerEntered?.Invoke();
		}
	}
	
	public void ReturnToSaveBooster()
	{
		rb.velocity = Vector2.zero;
		rb.angularVelocity = 0;
		
		if (lastBooster == null)
		{
			transform.position = safeBooster.SafePosition.position;
			return;
		}
		
		transform.position = lastBooster.SafePosition.position;
	}
	
	public void Enable()
	{
		Touch.onFingerDown += MovePlayer;
		Touch.onFingerUp += ReleasePlayer;
	}
	
	public void Disable()
	{
		isMoving = false;
		Touch.onFingerDown -= MovePlayer;
		Touch.onFingerUp -= ReleasePlayer;
	}
	
	private void OnDestroy()
	{
		EnhancedTouchSupport.Disable();
		TouchSimulation.Disable();
		Disable();
	}
	
	public void PlayDamageCoroutine()
	{
		StartCoroutine(DamageEffect());
	}
	
	public void PlayDeathCoroutine()
	{
		StartCoroutine(DeathEffect());
	}
	
	private IEnumerator DamageEffect()
	{
		for (int i = 0; i < 10; i++)
		{
			effect.gameObject.SetActive(false);
			spriteRenderer.color = new Color(1, 1, 1, 0);
			yield return new WaitForSeconds(.2f);
			effect.gameObject.SetActive(true);
			spriteRenderer.color = new Color(1, 1, 1, 1);
			yield return new WaitForSeconds(.2f);
		}
	}
	
	private IEnumerator DeathEffect()
	{
		spriteRenderer.color = new Color(1, 1, 1, 0);
		var effect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity, transform);
		rb.velocity = Vector2.zero;
		rb.angularVelocity = 0;
		trail.gameObject.SetActive(false);
		yield return new WaitForSeconds(1f);
		Destroy(effect);
	}
}
