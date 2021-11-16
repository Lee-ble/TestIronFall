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
	
	private int _humansPerCurrentLevel = 0;
	private List<PlayerLogic> _playersPoolList;
	private List<Transform> _instantiatedPlayers;

	void Start()
	{
		_instantiatedPlayers = new List<Transform>();
		_instantiatedPlayers.Add(firstPlayerObject.transform.Find("Armature/Hips").transform);
		_humansPerCurrentLevel = GameObject.FindObjectsOfType<HumanLogic>().Length;
		_playersPoolList = new List<PlayerLogic>();
		for (int i = 0; i < _humansPerCurrentLevel; i++)
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
			_instantiatedPlayers.Add(returnedPlayer.transform.Find("Armature/Hips").transform);
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
	
	public int GetPlayersAmount()
	{
		return _instantiatedPlayers.Count;
	}
	public void RemovePlayer(Transform transform)
	{
		_instantiatedPlayers.Remove(transform.Find("Armature/Hips").transform);
		if (_instantiatedPlayers.Count == 0)
		{
			if (EventManager.Instance != null)
			{
				EventManager.Instance.AllPlayersDestroyed();
			}
		}	
	}
}
