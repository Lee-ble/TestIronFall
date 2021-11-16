using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
	public List<ShopItemModel> ArmsModels;
	public List<ShopItemModel> HeadModels;
	public List<ShopItemModel> LegsModels;
	public List<ShopItemModel> BodyModels;

	#region Singleton
	public static EquipmentManager Instance = null;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance == this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}
	#endregion
	public string CurrentPlayerLegsEquipment
	{
		get { return PlayerPrefs.GetString("CurrentPlayerLegsEquipment"); }
		set { PlayerPrefs.SetString("CurrentPlayerLegsEquipment", value); }
	}
	public string CurrentPlayerHeadEquipment
	{
		get { return PlayerPrefs.GetString("CurrentPlayerHeadEquipment"); }
		set { PlayerPrefs.SetString("CurrentPlayerHeadEquipment", value); }
	}
	public string CurrentPlayerBodyEquipment
	{
		get { return PlayerPrefs.GetString("CurrentPlayerBodyEquipment"); }
		set { PlayerPrefs.SetString("CurrentPlayerBodyEquipment", value); }
	}
	public string CurrentPlayerArmsEquipment
	{
		get { return PlayerPrefs.GetString("CurrentPlayerArmsEquipment"); }
		set { PlayerPrefs.SetString("CurrentPlayerArmsEquipment", value); }
	}

	public List<EquipmentSettings> EquipmentList = new List<EquipmentSettings>();

	private void Start()
	{
		if (!CurrentPlayerHeadEquipment.Equals(string.Empty))
		{
			AddAndSave(HeadModels.First(x => x.Name.Equals(CurrentPlayerHeadEquipment)));

		}
		if (!CurrentPlayerLegsEquipment.Equals(string.Empty))
		{
			AddAndSave(LegsModels.First(x => x.Name.Equals(CurrentPlayerLegsEquipment)));
		}
		if (!CurrentPlayerArmsEquipment.Equals(string.Empty))
		{
			AddAndSave(ArmsModels.First(x => x.Name.Equals(CurrentPlayerArmsEquipment)));
		}
		if (!CurrentPlayerBodyEquipment.Equals(string.Empty))
		{
			AddAndSave(BodyModels.First(x => x.Name.Equals(CurrentPlayerBodyEquipment)));
		}
	}

	public void AddAndSave(ShopItemModel itemModel)
	{
		EquipmentList.RemoveAll(x => x.BodyPart.Equals(itemModel.EquipmentSettings.BodyPart));
		EquipmentList.Add(itemModel.EquipmentSettings);
		switch (itemModel.EquipmentSettings.BodyPart)
		{
			case BodyPart.Arms:
				CurrentPlayerArmsEquipment = itemModel.Name;
				break;
			case BodyPart.Legs:
				CurrentPlayerLegsEquipment = itemModel.Name;
				break;
			case BodyPart.Body:
				CurrentPlayerBodyEquipment = itemModel.Name;
				break;
			case BodyPart.Head:
				CurrentPlayerHeadEquipment = itemModel.Name;
				break;
		}
	}
}

[System.Serializable]
public class EquipmentSettings
{
	public GameObject EqPrefab;
	public Quaternion EqQuaternion;
	public Vector3 Offset;
	public BodyPart BodyPart;

}

public enum BodyPart
{
	Head = 0,
	Body = 1,
	Legs = 2,
	Arms = 3
}
