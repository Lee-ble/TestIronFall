using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
	private Vector2 _fingerDownPos;
	private Vector2 _fingerUpPos;

	[SerializeField] private bool detectSwipeAfterRelease = false;

	[SerializeField] private float SWIPE_THRESHOLD = 100f;

	void Update()
	{
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				_fingerUpPos = touch.position;
				_fingerDownPos = touch.position;
			}

			if (touch.phase == TouchPhase.Moved)
			{
				if (!detectSwipeAfterRelease)
				{
					_fingerDownPos = touch.position;
					DetectSwipe();
				}
			}

			if (touch.phase == TouchPhase.Ended)
			{
				_fingerDownPos = touch.position;
				DetectSwipe();
			}
		}
	}

	void DetectSwipe()
	{
		if (HorizontalMoveValue() > SWIPE_THRESHOLD && HorizontalMoveValue() > VerticalMoveValue())
		{
			if (_fingerDownPos.x - _fingerUpPos.x > 0)
			{
				OnSwipeRight();
			}
			else if (_fingerDownPos.x - _fingerUpPos.x < 0)
			{
				OnSwipeLeft();
			}
			_fingerUpPos = _fingerDownPos;
		}
		else
		{
		}
	}

	float VerticalMoveValue()
	{
		return Mathf.Abs(_fingerDownPos.y - _fingerUpPos.y);
	}

	float HorizontalMoveValue()
	{
		return Mathf.Abs(_fingerDownPos.x - _fingerUpPos.x);
	}

	void OnSwipeLeft()
	{
		if (EventManager.Instance != null)
		{
			EventManager.Instance.PlayerSwiped(-1);
		}
	}

	void OnSwipeRight()
	{
		if (EventManager.Instance != null)
		{
			EventManager.Instance.PlayerSwiped(1);
		}
	}
}