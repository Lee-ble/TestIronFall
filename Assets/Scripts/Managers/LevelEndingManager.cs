using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndingManager : MonoBehaviour
{
	[SerializeField] private PauseMenuEndingView endingView;

	private void Start()
	{
		if (EventManager.Instance != null)
		{
			EventManager.Instance.AllPlayersDestroyed += NegativeEnding;
			EventManager.Instance.LevelEndedByFinishPlate += PositiveEnding;
		}
	}

	private void PositiveEnding()
	{
		endingView.gameObject.SetActive(true);
		endingView.PositiveEnding();
		CoinScoreManager.Instance.SavePlayerCoins();
	}
	
	private void NegativeEnding()
	{
		endingView.gameObject.SetActive(true);
		endingView.NegativeEnding();
	}



	private void OnDestroy()
	{
		if (EventManager.Instance != null)
		{
			EventManager.Instance.AllPlayersDestroyed -= NegativeEnding;
			EventManager.Instance.LevelEndedByFinishPlate -= PositiveEnding;
		}
	}
}
