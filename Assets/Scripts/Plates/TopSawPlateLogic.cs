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
