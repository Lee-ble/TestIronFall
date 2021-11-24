using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerLogic : MonoBehaviour
{
	[SerializeField] private Transform onArmorLostSpawnTransform;
	[SerializeField] private Transform onDeathSpawnTransform;

	[SerializeField] private float iFrame;
	[SerializeField] private float armorLiveTime;
	[SerializeField] private float minExplosionForce;
	[SerializeField] private float maxExplosionForce;
	[SerializeField] private float headYVelocity;
	[SerializeField] private float hatMass;

	private bool _armorTriggered = false;

	private bool _helmetRemoved = false;

	public void DamagePlayer()
	{
		if (EquipmentManager.Instance != null)
		{
			if (EquipmentManager.Instance.CurrentPlayerHeadEquipment.Length != 0 && !_armorTriggered)
			{
				StartCoroutine(IFrameCount());
				if (!_helmetRemoved)
				{
					GameObject headGameObject = gameObject.GetComponent<PlayerEquipmentController>().GetArmor(BodySpawnPart.Head);
					headGameObject.GetComponent<BoxCollider>().isTrigger = true;
					Rigidbody headRigidbody = headGameObject.AddComponent<Rigidbody>();
					headRigidbody.mass = hatMass;
					headRigidbody.isKinematic = false;
					float randomX = Random.Range(0.9f, 1f) * ((Random.Range(0, 2) == 0) ? 1 : -1);
					float randomY = Random.Range(0.9f, 1f) * ((Random.Range(0, 2) == 0) ? 1 : -1);
					float randomExplosionForce = Random.Range(minExplosionForce, maxExplosionForce);
					headRigidbody.velocity.Set(headRigidbody.velocity.x, headRigidbody.velocity.y + headYVelocity, headRigidbody.velocity.z);
					headRigidbody.AddForce(new Vector3(randomX * randomExplosionForce, randomY * randomExplosionForce, 0), ForceMode.Impulse);
					//headRigidbody.AddTorque(new Vector3(randomX * randomExplosionForce, randomY * randomExplosionForce, 0), ForceMode.Impulse);
					if (EffectsManager.Instance != null)
					{
						EffectsManager.Instance.SpawnParticles(onArmorLostSpawnTransform, EffectsManager.Instance.OnArmorLostParticles, 
							EffectsManager.Instance.OnArmorLostParticlesScale);
						StartCoroutine(EffectsManager.Instance.DestroyGameObject(armorLiveTime, headGameObject));
					}
					_helmetRemoved = true;
				}
			}
			else
			{
				DestroyPlayer();
			}
		}
	}

	public void DestroyPlayer()
	{
		if (EffectsManager.Instance != null)
		{
			EffectsManager.Instance.SpawnParticles(onDeathSpawnTransform, EffectsManager.Instance.OnDeathParticles,
				EffectsManager.Instance.OnDeathParticlesScale);
		}
		this.gameObject.SetActive(false);
		if (PlayerManager.Instance != null)
		{
			PlayerManager.Instance.RemovePlayer(transform);
		}
	}
	IEnumerator IFrameCount()
	{
		yield return new WaitForSeconds(iFrame);
		_armorTriggered = true;
	}
}
