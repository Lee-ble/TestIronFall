using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFighterLogic : MonoBehaviour
{
	[SerializeField] private Vector3 scalePerPlayer;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player") || other.tag.Equals("PlayerCenter"))
		{
			other.gameObject.GetComponentInParent<PlayerLogic>().DestroyPlayer();
			transform.localScale += scalePerPlayer;
		}
	}
}
