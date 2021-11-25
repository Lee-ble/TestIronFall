using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenArmorLogic : MonoBehaviour, IBreakable
{
	[SerializeField] private GameObject normalArmor;
	[SerializeField] private GameObject brokenArmor;
	[SerializeField] private List<Rigidbody> partsRigibodies;

	[SerializeField] private float armorLiveTime;
	[SerializeField] private float minExplosionForce;
	[SerializeField] private float maxExplosionForce;

	public void Break()
	{
		normalArmor.SetActive(false);
		brokenArmor.SetActive(true);

		foreach (Rigidbody rigidbody in partsRigibodies)
		{
			float randomX = Random.Range(0.9f, 1f) * ((Random.Range(0, 2) == 0) ? 1 : -1);
			float randomY = Random.Range(0.9f, 1f) * ((Random.Range(0, 2) == 0) ? 1 : -1);
			float randomExplosionForce = Random.Range(minExplosionForce, maxExplosionForce);
			rigidbody.velocity.Set(rigidbody.velocity.x, rigidbody.velocity.y, rigidbody.velocity.z);
			rigidbody.AddForce(new Vector3(randomX * randomExplosionForce, randomY * randomExplosionForce, 0), ForceMode.Impulse);
			StartCoroutine(DestroyGameObject(armorLiveTime, rigidbody.gameObject));
		}


	}

	IEnumerator DestroyGameObject(float time, GameObject objectToDestroy)
	{
		yield return new WaitForSeconds(time);
		Destroy(objectToDestroy);
	}
}
