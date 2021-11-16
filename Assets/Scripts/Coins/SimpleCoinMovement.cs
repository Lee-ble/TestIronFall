using UnityEngine;
using DG.Tweening;

public class SimpleCoinMovement : MonoBehaviour
{
    [SerializeField] private float coinVerticalMoveSpeed ;
	[SerializeField] private float coinVerticalMoveHeight;
	[SerializeField] private float coinHorizontalRotationAngle;
	[SerializeField] private float coinHorizontalRotationSpeed;

	void Start()
	{
		transform.DOMoveY(transform.position.y + coinVerticalMoveHeight, coinVerticalMoveSpeed)
			.SetSpeedBased(true).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
		transform.DORotate(new Vector3 (0, coinHorizontalRotationAngle, 0), coinHorizontalRotationSpeed)
			.SetSpeedBased(true).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
	}
}
