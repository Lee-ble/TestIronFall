using UnityEngine;
using TMPro;

public class CoinsTextDisplay : MonoBehaviour
{
	[SerializeField] private TMP_Text coinTextField;

	private void Start()
	{
		if (EventManager.Instance != null)
		{
			EventManager.Instance.CoinsAmountChanged += DisplayCoinsAmountChange;
		}
	}

	private void DisplayCoinsAmountChange(int coinsAmount)
	{
		coinTextField.text = "Coins: " + coinsAmount;
	}

	private void OnDestroy()
	{
		if (EventManager.Instance != null)
		{
			EventManager.Instance.CoinsAmountChanged -= DisplayCoinsAmountChange;
		}
	}
}
