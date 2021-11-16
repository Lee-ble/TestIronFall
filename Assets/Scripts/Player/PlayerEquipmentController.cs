using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentController : MonoBehaviour
{
	[SerializeField] private Transform headParent;
	[SerializeField] private Transform bodyParent;
	[SerializeField] private Transform armsParent;
	[SerializeField] private Transform legsParent;

	private List<GameObject> _instantiatedEquipment  = new List<GameObject>();

	void Start()
	{
		SpawnEquipment();
	}

	public void SpawnEquipment()
	{
		if (_instantiatedEquipment != null && _instantiatedEquipment.Count > 0)
		{
			foreach (var instantiatedEquipment in _instantiatedEquipment)
			{
				Destroy(instantiatedEquipment);
			}
			_instantiatedEquipment.Clear();
		}

		foreach (var equipment in EquipmentManager.Instance.EquipmentList)	
		{
			switch (equipment.BodyPart)
			{
				case BodyPart.Arms:
					_instantiatedEquipment.Add(Instantiate(equipment.EqPrefab, armsParent.position + equipment.Offset, equipment.EqQuaternion, armsParent));
					break;
				case BodyPart.Body:
					_instantiatedEquipment.Add(Instantiate(equipment.EqPrefab, bodyParent.position + equipment.Offset, equipment.EqQuaternion, bodyParent));
					break;
				case BodyPart.Head:
					_instantiatedEquipment.Add(Instantiate(equipment.EqPrefab, headParent.position + equipment.Offset, equipment.EqQuaternion, headParent));
					break;
				case BodyPart.Legs:
					_instantiatedEquipment.Add(Instantiate(equipment.EqPrefab, legsParent.position + equipment.Offset, equipment.EqQuaternion, legsParent));
					break;
			}
		}
	}
}
