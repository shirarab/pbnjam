using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;



public class EventManager : MonoBehaviour
{
	public static event Action GameOverAnimation;
	
	
	public static void StartEndOfGame()
	{
		GameOverAnimation?.Invoke();
	}
}