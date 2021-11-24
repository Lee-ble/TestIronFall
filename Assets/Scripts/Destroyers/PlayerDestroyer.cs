using UnityEngine;

public class PlayerDestroyer : MonoBehaviour
{
	[SerializeField] private float force;
	[SerializeField]

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals("Player"))
		{
			if (other.gameObject.GetComponentInParent<PlayerLogic>() != null)
			{
				if (gameObject.tag.Equals("TopDestroyer"))
				{
					other.gameObject.GetComponentInParent<PlayerLogic>().DestroyPlayer();
				}
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag.Equals("Player"))
		{
			if (collision.gameObject.GetComponentInParent<PlayerLogic>() != null)
			{
				if (gameObject.tag.Equals("TopDestroyer"))
				{
					collision.gameObject.GetComponentInParent<PlayerLogic>().DestroyPlayer();
				}
				else
				{
					Vector3 direction = collision.GetContact(0).point - transform.position;
					direction = -direction.normalized;
					Rigidbody rigidbody = null;
					collision.gameObject.TryGetComponent<Rigidbody>(out rigidbody);
					direction.z = 0;
					if (rigidbody != null)
					{
						rigidbody.velocity.Set(0, 0, 0);
						rigidbody.AddForce(direction * force * 10);
					}
					collision.gameObject.GetComponentInParent<PlayerLogic>().DamagePlayer();
				}
			}
		}
		else if (collision.gameObject.tag.Equals("Armor"))
		{
			Vector3 direction = collision.GetContact(0).point - transform.position;
			direction = -direction.normalized;
			Rigidbody rigidbody = null;
			collision.gameObject.TryGetComponent<Rigidbody>(out rigidbody);
			direction.z = 0;
			if (rigidbody != null)
			{
				rigidbody.velocity.Set(0, 0, 0);
				rigidbody.AddForce(direction * force);
			}
		}
	}
}
