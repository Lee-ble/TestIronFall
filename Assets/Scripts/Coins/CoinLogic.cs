using UnityEngine;

public class CoinLogic : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player") || other.tag.Equals("PlayerCenter"))
		{
			if (EventManager.Instance != null)
			{
				EventManager.Instance.CoinPickedUp(coinValue);
			}
			CoinDestroy();
		}
	}

	void CoinDestroy()
	{
		this.gameObject.SetActive(false);
	}
}
