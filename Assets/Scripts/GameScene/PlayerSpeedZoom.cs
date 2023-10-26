using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerSpeedZoom : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera vCamera;
	[SerializeField] private PlayerController player;
	[SerializeField] private float zoomSpeed;
	[SerializeField] private float zoomInSpeed = 0.2f;
	[SerializeField] private float maxZoom = 10;
	
	private void Update()
	{
		if (vCamera.m_Lens.OrthographicSize > maxZoom) return;
		
		var playerTime = player.CurrentTime;
		vCamera.m_Lens.OrthographicSize = Mathf.Log(Mathf.Pow(playerTime + 10, 20)) - 40f;
	}
}
