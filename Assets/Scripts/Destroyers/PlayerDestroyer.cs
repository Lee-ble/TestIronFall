using UnityEngine;

public class PlayerDestroyer : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			if (other.gameObject.GetComponentInParent<PlayerLogic>() != null)
			{
				other.gameObject.GetComponentInParent<PlayerLogic>().DestroyPlayer();

			}
		}
	}
}
