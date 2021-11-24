using TMPro;
using UnityEngine;

public class MultiplierPlate : MonoBehaviour
{
	[SerializeField] private MultiplierType multiplierType;
	[SerializeField] private int num;
	[SerializeField] private Vector3 maxOffset;
	[SerializeField] private Vector3 constOffset;
	[SerializeField] private TMP_Text label;

	private bool _alreadyTriggered = false;

	private void Start()
	{
		switch (multiplierType)
		{
			case MultiplierType.Plus:
				label.text = "+" + num.ToString();
				break;
			case MultiplierType.Minus:
				label.text = "-" + num.ToString();
				break;
			case MultiplierType.Multiply:
				label.text = "x" + num.ToString();
				break;
			case MultiplierType.Divide:
				label.text = "/" + num.ToString();
				break;
		}
	}

	/*
	private void OnTriggerEnter(Collider other)
	{
		if (!_alreadyTriggered && other.tag.Equals("Player"))
		{
			if (PlayerManager.Instance != null)
			{
				for (int i = 0; i < num; i++)
				{
					GameObject createdPlayer = PlayerManager.Instance.GetPlayerFromThePool();
					createdPlayer.SetActive(true);
					Vector3 randomOffset = new Vector3(Random.Range(-maxOffset.x, maxOffset.x), Random.Range(-maxOffset.y, maxOffset.y),
						Random.Range(-maxOffset.x, maxOffset.x));
					createdPlayer.transform.position = new Vector3(transform.position.x + randomOffset.x,
						transform.position.y + randomOffset.y, transform.position.z + randomOffset.z); 
					createdPlayer.transform.rotation = transform.rotation;
				}
				HidePlate();
			}
		}
	}
	*/

	private void OnCollisionEnter(Collision collision)
	{
		if (!_alreadyTriggered && (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("PlayerCenter")))
		{
			if (PlayerManager.Instance != null)
			{
				switch (multiplierType)
				{
					case MultiplierType.Plus:
						for (int i = 0; i < num; i++)
						{
							CreateNewPlayer();
						}
						break;
					case MultiplierType.Multiply:
						int k = (num - 1) * PlayerManager.Instance.GetPlayersAmount();
						for (int i = 0; i < k; i++)
						{
							CreateNewPlayer();
						}
						break;
					case MultiplierType.Minus:
						for (int i = 0; i < num; i++)
						{
							RemoveRandomPlayer();
						}
						break;
					case MultiplierType.Divide:
						int l = PlayerManager.Instance.GetPlayersAmount() - (int) PlayerManager.Instance.GetPlayersAmount() / num;
						if (l == PlayerManager.Instance.GetPlayersAmount())
						{
							l -= 1;
						}
						for (int i = 0; i < l; i++)
						{
							RemoveRandomPlayer();
						}
						break;
				}
				_alreadyTriggered = true;
				HidePlate();
			}
		}
	}

	private void CreateNewPlayer()
	{
		if (PlayerManager.Instance != null)
		{
			GameObject createdPlayer = PlayerManager.Instance.GetPlayerFromThePool();
			createdPlayer.SetActive(true);
			Vector3 randomOffset = new Vector3(Random.Range(-maxOffset.x, maxOffset.x), Random.Range(-maxOffset.y, maxOffset.y),
				Random.Range(-maxOffset.z, maxOffset.z));
			createdPlayer.transform.position = new Vector3(transform.position.x + randomOffset.x + constOffset.x,
				transform.position.y + randomOffset.y + constOffset.y, transform.position.z + randomOffset.z + constOffset.z);
			createdPlayer.transform.rotation = transform.rotation;
		}
	}

	private void RemoveRandomPlayer()
	{
		if (PlayerManager.Instance != null && PlayerManager.Instance.GetPlayersAmount() > 0)
		{
			PlayerManager.Instance.DestroyPlayer();
		}
	}

	private void HidePlate()
	{
		transform.parent.gameObject.SetActive(false);
	}

	public enum MultiplierType
	{
		Plus = 0,
		Minus = 1,
		Multiply = 2,
		Divide =3
	}

}
