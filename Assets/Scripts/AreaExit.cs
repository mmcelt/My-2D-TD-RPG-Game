﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
	#region Fields

	[SerializeField] string _areaToLoad;
	public string _areaTransitionName;
	[SerializeField] AreaEntrance _theEntrance;
	[SerializeField] float _waitToLoad = 1f;
	[SerializeField] bool _exiting2D;

	bool _shouldLoadAfterFade;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		_theEntrance._transitionName = _areaTransitionName;
	}

	void Update()
	{
		if (_shouldLoadAfterFade)
		{
			_waitToLoad -= Time.deltaTime;
		}
		if (_waitToLoad <= 0)
		{
			SceneManager.LoadScene(_areaToLoad);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerController.Instance._areaTransitionName = _areaTransitionName;
			GameManager.Instance._fadingBetweenAreas = true;

			if (_exiting2D)
			{
				StartCoroutine(EnteringDungeonRoutine());
			}

			_shouldLoadAfterFade = true;
			UIFade.Instance.FadeToBlack();
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	IEnumerator EnteringDungeonRoutine()
	{
		yield return new WaitForSeconds(_waitToLoad - 0.1f);
		PlayerController.Instance.gameObject.SetActive(false);
		OldSchoolFPC.Instance.gameObject.SetActive(true);
	}
	#endregion
}
