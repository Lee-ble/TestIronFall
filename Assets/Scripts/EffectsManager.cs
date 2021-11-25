using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
	public static EffectsManager Instance = null;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance == this)
		{
			Destroy(this.gameObject);
		}
	}


	public ParticleSystem OnDeathParticles;
	public Vector3 OnDeathParticlesScale;

	public ParticleSystem OnArmorLostParticles;
	public Vector3 OnArmorLostParticlesScale;

	public ParticleSystem OnGroundHitParticles;
	public Vector3 OnGroundHitParticlesScale;

	public void SpawnParticles(Transform pTransform, ParticleSystem particleSystem, Vector3 scale)
	{
		GameObject particleGameObject = Instantiate(particleSystem.gameObject);
		particleGameObject.transform.position = pTransform.position;
		particleGameObject.transform.localScale = scale;
		particleGameObject.GetComponent<ParticleSystem>().Play();
		StartCoroutine(DestroyGameObject(particleGameObject.GetComponent<ParticleSystem>().main.duration, particleGameObject));
	}

	public IEnumerator DestroyGameObject(float duration, GameObject particleGameObject)
	{
		yield return new WaitForSeconds(duration);
		Destroy(particleGameObject);
	}
}
