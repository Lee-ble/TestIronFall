using UnityEngine;
using DG.Tweening;

public class TopSawPlateLogic : MonoBehaviour
{
	[SerializeField] private float sawSpeed;
	private bool _isLevelStarted = false;

	private void Start()
	{
		this.tag = "Saw";
		if (EventManager.Instance != null)
		{
			EventManager.Instance.LevelStarted += OnLevelStart;
		}
	}
	private void OnLevelStart()
	{
		_isLevelStarted = true;
		MoveTowardsBottom();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player") && _isLevelStarted)
		{
			other.gameObject.GetComponentInParent<PlayerLogic>().DestroyPlayer();
		}
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
}
