using TMPro;
using UnityEngine;

public class MultiplierPlate : MonoBehaviour
{
	[SerializeField] private bool isMultiplier;
	[SerializeField] private int num;
	[SerializeField] private Vector3 maxOffset;
	[SerializeField] private Vector3 constOffset;
	[SerializeField] private TMP_Text label;

	private bool _alreadyTriggered = false;

	private void Start()
	{
		if (isMultiplier)
		{
			label.text = "x" + num.ToString();
		}
		else
		{
			label.text = "+" + num.ToString();
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
				if (!isMultiplier)
				{
					for (int i = 0; i < num; i++)
					{
						CreateNewPlayer();
					}
				}	
				else
				{
					int k = (num - 1) * PlayerManager.Instance.GetPlayersAmount();
					for (int i = 0; i < k; i++)
					{
						CreateNewPlayer();
					}
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
	private void HidePlate()
	{
		this.gameObject.SetActive(false);
	}

}
