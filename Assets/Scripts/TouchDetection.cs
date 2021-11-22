using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetection : MonoBehaviour
{
	private void Start()
	{
		Input.multiTouchEnabled = false;
	}
	void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			if (Input.GetTouch(0).position.x < Screen.width / 2)
			{
				if (EventManager.Instance != null)
				{
					EventManager.Instance.PlayerSwiped(-1);
				}
			}
			else
			{
				if (EventManager.Instance != null)
				{
					EventManager.Instance.PlayerSwiped(1);
				}
			}
		}
    }
}
