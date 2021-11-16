using UnityEngine;

public class HumanLogic : MonoBehaviour
{
	private bool _isInfected = false;

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.gameObject.tag);
		if (collision.gameObject.tag.Equals("Player") && !_isInfected)
		{
			CreatePlayer();
			HideThisHuman();
			_isInfected = true;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Saw"))
		{
			HideThisHuman();
		}
	}

	private void HideThisHuman()
	{
		this.gameObject.SetActive(false);
	}

	private void CreatePlayer()
	{
		if (PlayerManager.Instance != null)
		{
			GameObject newPlayer = PlayerManager.Instance.GetPlayerFromThePool();
			if (newPlayer != null)
			{
				newPlayer.transform.position = transform.position;
				newPlayer.transform.rotation = transform.rotation;
				newPlayer.SetActive(true);
			}
		}
	}
}
