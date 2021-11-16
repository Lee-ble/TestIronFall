using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
	void Start()
	{
		if (this.transform.childCount > 0)
		{
			foreach (Transform t in GetComponentsInChildren<Transform>())
			{
				t.tag = "Player";
			}
		}
		this.tag = "Player";
	}

	public void DestroyPlayer()
	{
		this.gameObject.SetActive(false);

		if (PlayerManager.Instance != null)
		{
			PlayerManager.Instance.RemovePlayer(transform);
		}
	}
}
