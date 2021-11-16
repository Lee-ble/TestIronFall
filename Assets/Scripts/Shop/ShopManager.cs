using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
	[SerializeField] private PlayerEquipmentController playerEquipment;

	[SerializeField] private Button armsButton;
	[SerializeField] private Button headButton;
	[SerializeField] private Button legsButton;
	[SerializeField] private Button bodyButton;
	[SerializeField] private Button backButton;

	[SerializeField] private GameObject itemPrefab;
	[SerializeField] private Transform contentTransform;
	[SerializeField] private GameObject menuPanel;
	[SerializeField] private TMP_Text coinsLabel;

	private List<ShopItemView> shopItemOffers;

	private void Start()
	{
		shopItemOffers = new List<ShopItemView>();
		armsButton.onClick.AddListener(() => PopulateScroll(BodyPart.Arms));
		headButton.onClick.AddListener(() => PopulateScroll(BodyPart.Head));
		bodyButton.onClick.AddListener(() => PopulateScroll(BodyPart.Body));
		legsButton.onClick.AddListener(() => PopulateScroll(BodyPart.Legs));
		backButton.onClick.AddListener(() => { menuPanel.SetActive(true); gameObject.SetActive(false); playerEquipment.gameObject.SetActive(false); });

		if (CoinScoreManager.Instance != null)
		{
			CoinScoreManager.Instance.OnBalanceChanged += UpdateCoinsDisplay;
			coinsLabel.text = CoinScoreManager.Instance.PlayerCoins.ToString();
		}
		UpdateEquipment();
	}

	private void UpdateCoinsDisplay(int coinsAmount)
	{
		coinsLabel.text = coinsAmount.ToString();
	}

	private void PopulateScroll(BodyPart bodyPart)
	{
		List<ShopItemModel> models = new List<ShopItemModel>();
		switch (bodyPart)
		{
			case BodyPart.Arms:
				models = EquipmentManager.Instance.ArmsModels;
				break;
			case BodyPart.Body:
				models = EquipmentManager.Instance.BodyModels;
				break;
			case BodyPart.Head:
				models = EquipmentManager.Instance.HeadModels;
				break;
			case BodyPart.Legs:
				models = EquipmentManager.Instance.LegsModels;
				break;
		}
		if (shopItemOffers.Count != 0)
		{
			foreach (var offer in shopItemOffers)
			{
				offer.OnEquipmentUpdated -= UpdateEquipment;
				Destroy(offer.gameObject);
			}
		}
		shopItemOffers.Clear();
		foreach (var model in models)
		{
			ShopItemView view = Instantiate(itemPrefab, contentTransform).GetComponent<ShopItemView>();
			view.SetupView(model);
			view.OnEquipmentUpdated += UpdateEquipment;
			shopItemOffers.Add(view);
		}
	}

	private void UpdateEquipment()
	{
		playerEquipment.SpawnEquipment();
	}
}

[System.Serializable]
public class ShopItemModel
{
	public EquipmentSettings EquipmentSettings;
	public int Price;
	public string Name;
	public int IsPurchased { get { return PlayerPrefs.GetInt($"{Name}_IsPurchased"); }
		set { PlayerPrefs.SetInt($"{Name}_IsPurchased", value); } }
}