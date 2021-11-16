using UnityEngine;

public class LevelEnderPlateLogic : MonoBehaviour
{
	private bool _isLevelEnded = false;
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player") && !_isLevelEnded)
		{
			if (EventManager.Instance != null)
			{
				EventManager.Instance.LevelEndedByFinishPlate();
			}
		}
	}
}
