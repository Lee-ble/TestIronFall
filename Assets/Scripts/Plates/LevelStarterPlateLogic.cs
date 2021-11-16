using UnityEngine;

public class LevelStarterPlateLogic : MonoBehaviour
{
	private bool _isLevelStarted = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player") && !_isLevelStarted)
		{
			if (EventManager.Instance != null)
			{
				EventManager.Instance.LevelStarted();
			}
			DestroyStarterObject();
		}
	}

	private void DestroyStarterObject()
	{
		this.gameObject.SetActive(false);
	}
}
