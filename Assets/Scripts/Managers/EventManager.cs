using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	#region Singleton
	public static EventManager Instance = null;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance == this)
		{
			Destroy(gameObject);
		}
	}
	#endregion

	public Action<int> CoinsAmountChanged;
	public Action<int> CoinPickedUp;
	public Action LevelStarted;
	public Action AllPlayersDestroyed;
	public Action LevelEndedByFinishPlate;

	public Action<int> PlayerSwiped; 
}
