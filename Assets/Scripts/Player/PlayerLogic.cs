using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerLogic : MonoBehaviour
{
	[SerializeField] private PlayerEquipmentController playerEquipmentController;
	[SerializeField] private Transform onArmorLostSpawnTransform;
	[SerializeField] private Transform onDeathSpawnTransform;

	[SerializeField] private float iFrame;

	private bool _headTriggered = false;
	private bool _bodyTriggered = false;

	private bool _helmetRemoved = false;
	private bool _bodyRemoved = false;

	public void DamagePlayer()
	{
		if (EquipmentManager.Instance != null)
		{
			if (EquipmentManager.Instance.CurrentPlayerHeadEquipment.Length != 0 && !_headTriggered)
			{
				StartCoroutine(IFrameCount());
				if (!_helmetRemoved)
				{

					GameObject headGameObject = playerEquipmentController.GetArmor(BodySpawnPart.Head);
					if (headGameObject == null)
					{
						DestroyPlayer();
						return;
					}
					headGameObject.GetComponent<IBreakable>().Break();

					if (EffectsManager.Instance != null)
					{
						EffectsManager.Instance.SpawnParticles(onArmorLostSpawnTransform, EffectsManager.Instance.OnArmorLostParticles,
							EffectsManager.Instance.OnArmorLostParticlesScale);
					}
					_helmetRemoved = true;
				}
			}
			else if (EquipmentManager.Instance.CurrentPlayerBodyEquipment.Length != 0 && !_bodyTriggered)
			{
				StartCoroutine(IFrameBodyCount());
				if (!_bodyRemoved)
				{
					playerEquipmentController.GetArmor(BodySpawnPart.Body).GetComponent<IBreakable>().Break();
					playerEquipmentController.GetArmor(BodySpawnPart.LeftArm).GetComponent<IBreakable>().Break();
					playerEquipmentController.GetArmor(BodySpawnPart.RightArm).GetComponent<IBreakable>().Break();
					playerEquipmentController.GetArmor(BodySpawnPart.LeftLeg).GetComponent<IBreakable>().Break();
					playerEquipmentController.GetArmor(BodySpawnPart.RightLeg).GetComponent<IBreakable>().Break();
					_bodyRemoved = true;
				}
			}
			else
			{
				DestroyPlayer();
			}
		}
	}

	public void DestroyPlayer()
	{
		if (EffectsManager.Instance != null)
		{
			EffectsManager.Instance.SpawnParticles(onDeathSpawnTransform, EffectsManager.Instance.OnDeathParticles,
				EffectsManager.Instance.OnDeathParticlesScale);
		}
		this.gameObject.SetActive(false);
		if (PlayerManager.Instance != null)
		{
			PlayerManager.Instance.RemovePlayer(transform);
		}
	}
	IEnumerator IFrameCount()
	{
		yield return new WaitForSeconds(iFrame);
		_headTriggered = true;
	}

	IEnumerator IFrameBodyCount()
	{
		yield return new WaitForSeconds(iFrame);
		_bodyTriggered = true;
	}
}
