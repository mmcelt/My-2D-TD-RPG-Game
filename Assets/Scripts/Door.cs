﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door: MonoBehaviour
{
	#region Fields

	public bool _doorIsOpen;
	[SerializeField] bool _isLocked;
	[SerializeField] string _requiredKey;
	[SerializeField] float _infoPanelShowTime = 1f;

	Animator _theAnim;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_theAnim = GetComponent<Animator>();
	}
	#endregion

	#region Public Methods

	public void OperateDoor()
	{
		if (!_isLocked)
		{
			if (!_doorIsOpen)
			{
				_doorIsOpen = true;
				_theAnim.SetTrigger("openDoor");
			}
			else
			{
				_doorIsOpen = false;
				_theAnim.SetTrigger("closeDoor");
			}
		}
		else
		{
			if(_requiredKey != "")
			{
				bool haveKey = false;
				foreach(string item in GameManager.Instance._itemsHeld)
				{
					if(item == _requiredKey)
					{
						haveKey = true;
						_doorIsOpen = true;
						_theAnim.SetTrigger("openDoor");
					}
				}
				if (!haveKey)
				{
					//SHOW INFO PANEL
					StartCoroutine(ShowInfoPanelRoutine());
				}
			}
		}
	}
	#endregion

	#region Private Methods

	IEnumerator ShowInfoPanelRoutine()
	{
		GameMenu.Instance._infoText.text = "You don't have the required key: " + _requiredKey;
		GameMenu.Instance._infoPanel.SetActive(true);
		yield return new WaitForSeconds(_infoPanelShowTime);
		GameMenu.Instance._infoPanel.SetActive(false);
		GameMenu.Instance._infoText.text = "";
	}
	#endregion
}
