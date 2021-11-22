using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CoinScoreManager : MonoBehaviour
{
	private int _currentCoinsAmount;
	private int _currentLevelCoinsAmount = 0;
	public Action<int> OnBalanceChanged;
	public int PlayerCoins
	{
		get { return PlayerPrefs.GetInt("PlayerCoins"); }
		set { PlayerPrefs.SetInt("PlayerCoins", value); }
	}

	#region Singleton
	public static CoinScoreManager Instance = null;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			_currentCoinsAmount = 0;
		}
		else if (Instance == this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}
	#endregion

	private void Start()
	{
		Application.targetFrameRate = 60;	
		SceneManager.sceneLoaded += Restart;
	}

	private void Restart(Scene scene, LoadSceneMode loadSceneMode)
	{
		_currentLevelCoinsAmount = 0;
		_currentCoinsAmount = PlayerCoins;
		if (EventManager.Instance != null)
		{
			EventManager.Instance.CoinPickedUp += AddCoinsToCurrentAmount;
			Time.timeScale = 1f;
		}
		
	}
	
	private void AddCoinsToCurrentAmount(int coinsAmount)
	{
		_currentCoinsAmount += coinsAmount;
		_currentLevelCoinsAmount += coinsAmount;
		if (EventManager.Instance != null)
		{
			//EventManager.Instance.CoinsAmountChanged(this._currentLevelCoinsAmount);
		}
	}

	public void SpendCoins(int coinsAmount)
	{
		if ((_currentCoinsAmount - coinsAmount) >= 0)
		{
			_currentCoinsAmount -= coinsAmount;
			SavePlayerCoins();
		}
	}

	public bool Purchase(ShopItemModel model)
	{
		if (_currentCoinsAmount < model.Price)
			return false;
		else
		{
			model.IsPurchased = 1;
			SpendCoins(model.Price);
			return true;
		}
	}

	public void SavePlayerCoins()
	{
		PlayerCoins = _currentCoinsAmount;
		if (EventManager.Instance == null)
		{
			OnBalanceChanged(_currentCoinsAmount);
		}
	}

	public int GetCurrentLevelCoinsAmount()
	{
		return _currentLevelCoinsAmount;
}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= Restart;
		if (EventManager.Instance != null)
		{
			EventManager.Instance.CoinPickedUp -= AddCoinsToCurrentAmount;
		}
	}
}
