using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPreambleWindow : MonoBehaviour
{
	public Action OnCountEndAction;
	
	public void RaiseOnCountEndAction()
	{
		OnCountEndAction?.Invoke();
		gameObject.SetActive(false);
	}
}
