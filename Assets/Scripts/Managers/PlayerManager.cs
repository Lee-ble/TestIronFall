using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	#region Singleton
	public static PlayerManager Instance = null;

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

	[SerializeField] private GameObject playerObjectPrefab;
	[SerializeField] private GameObject firstPlayerObject;
	[SerializeField] private string bodyLocation;
	[SerializeField] private int _maxPlayersPerCurrentLevel;

	private List<PlayerLogic> _playersPoolList;
	private List<Transform> _instantiatedPlayers;

	void Start()
	{
		_instantiatedPlayers = new List<Transform>();
		_instantiatedPlayers.Add(firstPlayerObject.transform.Find(bodyLocation).transform);
		_playersPoolList = new List<PlayerLogic>();
		for (int i = 0; i < _maxPlayersPerCurrentLevel; i++)
		{
			PlayerLogic createdPlayer = Instantiate(playerObjectPrefab).GetComponent<PlayerLogic>();
			createdPlayer.gameObject.SetActive(false);
			_playersPoolList.Add(createdPlayer);
		}
	}

	public GameObject GetPlayerFromThePool()
	{
		if (_playersPoolList != null && _playersPoolList.Count != 0)
		{
			GameObject returnedPlayer = _playersPoolList[0].gameObject;
			_playersPoolList.RemoveAt(0);
			_instantiatedPlayers.Add(returnedPlayer.transform.Find(bodyLocation).transform);
			return returnedPlayer;
		}
		else
		{
			return null;
		}
	}

	public float GetPlayersAverageY()
	{
		float sum = 0f;
		foreach (Transform transform in _instantiatedPlayers)
		{
			sum += transform.position.y;
		}
		sum /= _instantiatedPlayers.Count;
		return sum;
	}
	
	public float GetLowestPlayerY()
	{
		float lowestY = 3000f;

		foreach (Transform transform in _instantiatedPlayers)
		{
			if (lowestY > transform.position.y)
			{
				lowestY = transform.position.y;
			}
		}
		return lowestY;
	}

	public int GetPlayersAmount()
	{
		return _instantiatedPlayers.Count;
	}
	public void RemovePlayer(Transform transform)
	{
		_instantiatedPlayers.Remove(transform.Find(bodyLocation).transform);
		if (_instantiatedPlayers.Count == 0)
		{
			if (EventManager.Instance != null)
			{
				EventManager.Instance.AllPlayersDestroyed();
			}
		}	
	}
}
