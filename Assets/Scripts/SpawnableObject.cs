using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
	[SerializeField] private MoveOverTrigger moveOverTrigger;
	[SerializeField] private bool isStatic;
	public MoveOverTrigger MoveOverTrigger => moveOverTrigger;
	public bool IsStatic => isStatic;
}
