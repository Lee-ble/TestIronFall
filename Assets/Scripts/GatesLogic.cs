using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatesLogic : MonoBehaviour
{
	[SerializeField] private int playersReqForGates;
	[SerializeField] private float openTime;

	private int currentPlayersInGates = 0;

	private void Update()
	{
		if (PlayerManager.Instance != null)
		{
			if (currentPlayersInGates >= playersReqForGates && currentPlayersInGates.Equals(PlayerManager.Instance.GetPlayersAmount()))
			{
				StartCoroutine(OpenGates());
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("PlayerCenter"))
		{
			currentPlayersInGates += 1;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag.Equals("PlayerCenter"))
		{
			currentPlayersInGates -= 1;
		}
	}

	IEnumerator OpenGates()
	{
		yield return new WaitForSeconds(openTime);
		gameObject.SetActive(false);
	}
}
