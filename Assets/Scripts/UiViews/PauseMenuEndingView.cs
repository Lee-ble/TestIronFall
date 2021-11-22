using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenuEndingView : MonoBehaviour
{
	[SerializeField] private Button tryAgainButton;
	[SerializeField] private Button goToMainMenuButton;

	[SerializeField] private TMP_Text scoreLabel;
	[SerializeField] private TMP_Text coinsLabel;

	[SerializeField] private Color positiveColor;
	[SerializeField] private Color negativeColor;

	private void Start()
	{
		tryAgainButton.onClick.AddListener(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });
		goToMainMenuButton.onClick.AddListener(() => { SceneManager.LoadScene(0); });
		this.gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		Time.timeScale = 0f;
	}
	private void OnDisable()
	{
		Time.timeScale = 1f;
	}
	public void PositiveEnding()
	{
		scoreLabel.color = positiveColor;
		coinsLabel.color = positiveColor;
		if (CoinScoreManager.Instance != null && PlayerManager.Instance != null)
		{
			scoreLabel.text = "Score: " + (CoinScoreManager.Instance.GetCurrentLevelCoinsAmount() * 100 + PlayerManager.Instance.GetPlayersAmount() * 1000).ToString();
			coinsLabel.text = "Coins: " + CoinScoreManager.Instance.GetCurrentLevelCoinsAmount().ToString();
		}
	}

	public void NegativeEnding()
	{
		scoreLabel.color = negativeColor;
		coinsLabel.color = negativeColor;

		scoreLabel.text = "You lost!";
		coinsLabel.text = "Try Again?";
	}
}
