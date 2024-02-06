using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;



public class EventManager : MonoBehaviour
{
	public static event Action<int> GameOverAnimation;
	
	
	public static void StartEndOfGame(int winner)
	{
		Debug.Log("Game Over animation started!");
		GameOverAnimation?.Invoke(winner);
	}
}