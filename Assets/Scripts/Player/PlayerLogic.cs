using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerLogic : MonoBehaviour
{
	[SerializeField] private ParticleSystem particle;
	[SerializeField] private Transform spawnTransform;

	public void DestroyPlayer()
	{
		particle.gameObject.SetActive(true);
		particle.Play();
		GameObject gameObject = Instantiate(particle.gameObject);
		gameObject.transform.position = spawnTransform.position;
		gameObject.GetComponent<ParticleSystem>().Play();
		this.gameObject.SetActive(false);
		if (PlayerManager.Instance != null)
		{
			PlayerManager.Instance.RemovePlayer(transform);
		}
	}
}
