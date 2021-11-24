using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentController : MonoBehaviour
{
	[SerializeField] private Transform headParent;
	[SerializeField] private Transform bodyParent;
	[SerializeField] private Transform leftArmParent;
	[SerializeField] private Transform rightArmParent;
	[SerializeField] private Transform leftLegParent;
	[SerializeField] private Transform rightLegParent;

	private Dictionary<BodySpawnPart, GameObject> _instantiatedEquipment  = new Dictionary<BodySpawnPart, GameObject>();

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
				Destroy(instantiatedEquipment.Value);
			}
			_instantiatedEquipment.Clear();
		}

		foreach (var equipment in EquipmentManager.Instance.EquipmentList)	
		{
			switch (equipment.BodyPart)
			{
				case BodyPart.Arms:
					GameObject leftArmObject = Instantiate(equipment.EqPrefab,
						new Vector3(leftArmParent.position.x - equipment.Offset.x, leftArmParent.position.y + equipment.Offset.y,
						leftArmParent.position.z + equipment.Offset.z), equipment.EqQuaternion, leftArmParent);
					leftArmObject.transform.localScale = equipment.Scale;
					_instantiatedEquipment.Add(BodySpawnPart.LeftArm, leftArmObject);
					GameObject rightArmObject = Instantiate(equipment.EqPrefab,
						new Vector3(rightArmParent.position.x + equipment.Offset.x, rightArmParent.position.y + equipment.Offset.y,
						rightArmParent.position.z + equipment.Offset.z), equipment.EqQuaternion, rightArmParent);
					rightArmObject.transform.localScale = equipment.Scale;
					_instantiatedEquipment.Add(BodySpawnPart.RightArm, rightArmObject);
					break;
				case BodyPart.Body:
					GameObject bodyObject = Instantiate(equipment.EqPrefab, bodyParent.position + equipment.Offset, 
						equipment.EqQuaternion, bodyParent);
					bodyObject.transform.localScale = equipment.Scale;
					_instantiatedEquipment.Add(BodySpawnPart.Body, bodyObject);
					break;
				case BodyPart.Head:
					GameObject headObject = Instantiate(equipment.EqPrefab, headParent.position + equipment.Offset,
						equipment.EqQuaternion, headParent);
					headObject.transform.localScale = equipment.Scale;
					_instantiatedEquipment.Add(BodySpawnPart.Head, headObject);
					break;
				case BodyPart.Legs:
					GameObject leftLegObject = Instantiate(equipment.EqPrefab,
						new Vector3(leftLegParent.position.x - equipment.Offset.x, leftLegParent.position.y + equipment.Offset.y,
						leftLegParent.position.z + equipment.Offset.z), equipment.EqQuaternion, leftLegParent);
					leftLegObject.transform.localScale = equipment.Scale;
					_instantiatedEquipment.Add(BodySpawnPart.LeftLeg, leftLegObject);
					GameObject rightLegObject = Instantiate(equipment.EqPrefab, 
						new Vector3(rightLegParent.position.x + equipment.Offset.x, rightLegParent.position.y + equipment.Offset.y, 
						rightLegParent.position.z + equipment.Offset.z), equipment.EqQuaternion, rightLegParent);
					rightLegObject.transform.localScale = equipment.Scale;
					_instantiatedEquipment.Add(BodySpawnPart.RightLeg, rightLegObject);
					break;
			}
		}
	}

	public GameObject GetArmor(BodySpawnPart bodyPart)
	{
		return _instantiatedEquipment[bodyPart];
	}
}

public enum BodySpawnPart
{
	Head = 0,
	Body = 1,
	LeftArm = 2,
	RightArm = 3,
	LeftLeg = 4,
	RightLeg = 5
}