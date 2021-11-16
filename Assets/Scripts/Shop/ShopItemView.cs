using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemView : MonoBehaviour
{
	[SerializeField] private TMP_Text priceLabel;
	[SerializeField] private TMP_Text nameLabel;
	[SerializeField] private Button button;

	public Action OnEquipmentUpdated;

	private ShopItemModel _model;

	public void SetupView(ShopItemModel model)
	{
		nameLabel.text = model.Name;
		
		if (model.IsPurchased != 0)
		{
			priceLabel.text = "Purch";
		}
		else
		{
			if (model.Price != 0)
			{
				priceLabel.text = model.Price.ToString();
			}
			else
			{
				priceLabel.text = "Free";
			}
		}
		_model = model;
		button.onClick.AddListener(TryWear);
	}

	private void TryWear()
	{
		if (_model.IsPurchased == 0)
		{
			TryPurchase();
		}
		else
		{
			if (EquipmentManager.Instance != null)
			{
				EquipmentManager.Instance.AddAndSave(_model);
				OnEquipmentUpdated();
			}
		}
	}

	private void TryPurchase()
	{
		if (CoinScoreManager.Instance != null)
		{
			if (CoinScoreManager.Instance.Purchase(_model))
			{

				if (EquipmentManager.Instance != null)
				{
					EquipmentManager.Instance.AddAndSave(_model);
					priceLabel.text = "Purch";
					OnEquipmentUpdated();
				}

			}
		}
	}
}
