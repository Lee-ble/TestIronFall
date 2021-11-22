using UnityEngine;
using DG.Tweening;

public class TopDestroyerLogic : MonoBehaviour
{
	[SerializeField] private float sawSpeed;
	
	private void Start()
	{
		if (EventManager.Instance != null)
		{
			EventManager.Instance.LevelStarted += OnLevelStart;
		}
	}
	private void OnLevelStart()
	{
		MoveTowardsBottom();
	}

	void MoveTowardsBottom()
	{
		transform.DOMoveY(0, sawSpeed).SetSpeedBased(true).SetEase(Ease.Linear);
	} 

	void OnDestroy()
	{
		if (EventManager.Instance != null)
		{
			EventManager.Instance.LevelStarted -= OnLevelStart;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.tag.Equals("Wall") && !other.tag.Equals("Player") && !other.tag.Equals("PlayerCenter"))
		{
			other.gameObject.SetActive(false);
		}
	}
}
