﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleRewards : MonoBehaviour
{
	#region Fields

	public static BattleRewards Instance;

	[SerializeField] Text _xpText, _goldText, _itemsText;
	[SerializeField] GameObject _rewardScreen;
	[SerializeField] string[] _rewardItems;
	[SerializeField] int _xpEarned;
	[SerializeField] int _goldFound;
	public bool _markQuestComplete;
	public string _questToMark;


	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
	}

	//void Update() 
	//{
	//	if (Input.GetKeyDown(KeyCode.Y))
	//	{
	//		OpenRewardScreen(54, 100, new string[] { "Iron Sword", "Iron Armor" });
	//	}
	//}
	#endregion

	#region Public Methods

	public void OpenRewardScreen(int xp, int gold, string[] rewards)
	{
		_xpEarned = xp;
		_goldFound = gold;
		_rewardItems = rewards;

		_xpText.text = "Surviving Players Earned " + _xpEarned + " XP!";
		_goldText.text = "Gold Found: " + _goldFound + "g";
		_itemsText.text = "";

		foreach(string item in _rewardItems)
		{
			_itemsText.text += item + "\n";
		}

		_rewardScreen.SetActive(true);
	}

	public void CloseRewardScreen()
	{
		for(int i=0; i<GameManager.Instance._playerStats.Length; i++)
		{
			if (GameManager.Instance._playerStats[i].gameObject.activeSelf && !GameManager.Instance._playerStats[i]._isDead)
			{
				GameManager.Instance._playerStats[i].AddExp(_xpEarned);
			}
		}

		for(int i=0; i<_rewardItems.Length; i++)
		{
			GameManager.Instance.AddItem(_rewardItems[i]);
		}

		GameManager.Instance._battleActive = false;
		_rewardScreen.SetActive(false);

		if (_markQuestComplete)
		{
			QuestManager.Instance.MarkQuestComplete(_questToMark);
		}
	}
	#endregion

	#region Private Methods


	#endregion
}
