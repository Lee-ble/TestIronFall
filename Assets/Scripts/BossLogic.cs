using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLogic : MonoBehaviour
{
	[SerializeField] private float maxHP;
	[SerializeField] private float damagePerTouch;

	private float _currentHP;

	private void OnEnable()
	{
		_currentHP = maxHP;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (_currentHP > damagePerTouch)
			{
				_currentHP -= damagePerTouch;
			}
			else
			{
				DestroyBoss();
			}
		}
	}

	private void DestroyBoss()
	{
		this.gameObject.SetActive(false);
	}
}
