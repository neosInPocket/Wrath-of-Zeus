using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPreambleWindow : MonoBehaviour
{
    [SerializeField] private Animator animator;
	public Action OnCountEndAction;
	
	public void Play()
	{
		StartCoroutine(PlayCountDown());
	}
	
	private IEnumerator PlayCountDown()
	{
		animator.SetTrigger("show");
		yield return new WaitForSeconds(3f);
		OnCountEndAction?.Invoke();
	}
}
