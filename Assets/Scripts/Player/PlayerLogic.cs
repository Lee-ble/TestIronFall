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
		GameObject particleGameObject = Instantiate(particle.gameObject);
		particleGameObject.transform.position = spawnTransform.position;
		particleGameObject.GetComponent<ParticleSystem>().Play();
		StartCoroutine(destroyParticle(particleGameObject.GetComponent<ParticleSystem>().main.duration, particleGameObject));
		this.gameObject.SetActive(false);
		if (PlayerManager.Instance != null)
		{
			PlayerManager.Instance.RemovePlayer(transform);
		}
	}

	IEnumerator destroyParticle(float duration, GameObject particleGameObject)
	{
		yield return new WaitForSeconds(duration);
		Destroy(particleGameObject);
	}
}
