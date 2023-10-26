using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthRender : MonoBehaviour
{
	[SerializeField] Image[] lifes;
	
	public void UpdateHealth(int lifesCount)
	{
		foreach (var life in lifes)
		{
			life.color = new Color(0, 0, 0, 1);
		}
		
		for (int i = 0; i < lifesCount; i++)
		{
			lifes[i].color = new Color(1, 1, 1, 1);
		}
	}
}
