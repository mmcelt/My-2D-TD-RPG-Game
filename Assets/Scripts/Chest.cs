﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
	#region Fields

	[SerializeField] bool _isLocked;
	[SerializeField] string _requiredKey;
	[SerializeField] float _infoPanelShowTime = 1f;
	[Header("Cursed Chest")]
	[SerializeField] bool _isCursed;
	[SerializeField] float _curseInterval = 3f, _curseInfoPanelShowTime = 4f;
	[SerializeField] AudioClip _thisIsCursed;

	bool _open, _alreadyLooted;
	Animator _theAnim;
	BootyGiver _bootyGiver;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_theAnim = GetComponent<Animator>();
		_bootyGiver = GetComponent<BootyGiver>();
	}
	#endregion

	#region Public Methods

	public void OperateChest()
	{
		if (!_isLocked)
		{
			if (!_open)
			{
				OpenChest();
			}
			else
			{
				CloseChest();
			}
		}
		else
		{
			if (_requiredKey != "")
			{
				bool haveKey = false;
				foreach (string item in GameManager.Instance._itemsHeld)
				{
					if (item == _requiredKey)
					{
						haveKey = true;
						OpenChest();
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

	void OpenChest()
	{
		GameManager.Instance._interactingWithObject = true;
		_theAnim.SetTrigger("openChest");
		_open = true;

		if (_isCursed)
		{
			StartCoroutine(ShowCursedChestPanelRoutine());
		}

		if (!_alreadyLooted)
			_bootyGiver.GiveBooty();
	}

	void CloseChest()
	{
		_theAnim.SetTrigger("closeChest");
		_open = false;
		GameManager.Instance._interactingWithObject = false;
	}

	IEnumerator ShowInfoPanelRoutine()
	{
		GameManager.Instance._interactingWithObject = true;
		GameMenu.Instance._infoText.text = "You don't have the required key: " + _requiredKey;
		GameMenu.Instance._infoPanel.SetActive(true);
		yield return new WaitForSeconds(_infoPanelShowTime);
		GameMenu.Instance._infoPanel.SetActive(false);
		GameMenu.Instance._infoText.text = "";
		GameManager.Instance._interactingWithObject = false;
	}

	IEnumerator ShowCursedChestPanelRoutine()
	{
		GameManager.Instance._isCursed = true;
		AudioManager.Instance.Stopmusic();
		GetComponent<AudioSource>().PlayOneShot(_thisIsCursed, 0.5f);
		GameManager.Instance._curseInterval = _curseInterval;
		GameMenu.Instance._infoText.text = "<color=red> This Chest Is Cursed!!</color> \n Your party is now Cursed until you take the antidote!!";
		GameMenu.Instance._infoPanel.SetActive(true);
		yield return new WaitForSeconds(_curseInfoPanelShowTime);
		GameMenu.Instance._infoPanel.SetActive(false);
		GameMenu.Instance._infoText.text = "";
		AudioManager.Instance.PlayMusic(OldSchoolFPC.Instance._musicToPlay);
		GameManager.Instance._interactingWithObject = false;
	}
	#endregion
}
