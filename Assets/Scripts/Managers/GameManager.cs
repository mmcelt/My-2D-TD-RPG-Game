﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	#region Fields

	public static GameManager Instance;

	[Header("Players")]
	public CharSats[] _playerStats;
	public bool _gameMenuOpen, _dialogActive, _fadingBetweenAreas, _shopOpen, _battleActive, _inDungeon, _bootyPanelOpen, _interactingWithObject;
	[Header("Items")]
	public string[] _itemsHeld;
	public int[] _numberHeldOfItem;
	public Item[] _referenceItems;
	[Header("Gold")]
	public int _currentGold;

	[HideInInspector] public bool _isContinuedGame, _isCursed;
	[HideInInspector] public float _curseInterval;

	float _curseCounter;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Start() 
	{
		
		SortItems();
	}
	
	void Update() 
	{
		if (_inDungeon)
		{
			PlayerController.Instance.gameObject.SetActive(false);
		}
		else
		{
			PlayerController.Instance.gameObject.SetActive(true);
		}

		if(_gameMenuOpen || _dialogActive || _fadingBetweenAreas || _shopOpen || _battleActive || _bootyPanelOpen || _interactingWithObject)
		{
			if (!_inDungeon)
				PlayerController.Instance._canMove = false;
			else
			{
				OldSchoolFPC.Instance._canMove = false;
			}
		}
		else
		{
			if (!_inDungeon)
				PlayerController.Instance._canMove = true;
			else
			{
				if(OldSchoolFPC.Instance)
					OldSchoolFPC.Instance._canMove = true;
			}
		}

		//curse handler
		if (_isCursed && _curseCounter <= 0)
		{
			foreach (CharSats player in _playerStats)
			{
				if (player.gameObject.activeInHierarchy)
				{
					player._currentHP -= 1;
					GameMenu.Instance.UpdateMainStats();
				}
			}
			_curseCounter = _curseInterval;
		}
		if (_isCursed && _curseCounter > 0)
			_curseCounter -= Time.deltaTime;

		//if (Input.GetKeyDown(KeyCode.J))
		//{
		//	AddItem("Iron Armor");
		//	AddItem("Pooper Scooper");

		//	RemoveItem("Health Potion");
		//	RemoveItem("Crapola");
		//}

		//if (Input.GetKeyDown(KeyCode.O))
		//	SaveData();
		//if (Input.GetKeyDown(KeyCode.P))
		//	LoadData();
	}
	#endregion

	#region Public Methods

	public Item GetItemDetails(string itemToFind)
	{
		for(int i=0; i<_referenceItems.Length; i++)
		{
			if (_referenceItems[i]._itemName == itemToFind)
			{
				return _referenceItems[i];
			}
		}

		Debug.LogError("Can't Find: " + itemToFind);
		return null;
	}

	public void SortItems()
	{
		bool itemAfterSpace = true;
		while (itemAfterSpace)
		{
			itemAfterSpace = false;

			for (int i = 0; i < _itemsHeld.Length - 1; i++)
			{
				if (_itemsHeld[i] == "")
				{
					_itemsHeld[i] = _itemsHeld[i + 1];
					_itemsHeld[i + 1] = "";

					_numberHeldOfItem[i] = _numberHeldOfItem[i + 1];
					_numberHeldOfItem[i + 1] = 0;

					if(_itemsHeld[i] != "")
					{
						itemAfterSpace = true;
					}
				}
			}
		}
	}

	public void AddItem(string itemToAdd)
	{
		int newItemPosition = 0;
		bool foundSpace = false;

		for(int i=0; i<_itemsHeld.Length; i++)
		{
			if(_itemsHeld[i]=="" || _itemsHeld[i] == itemToAdd)
			{
				newItemPosition = i;
				foundSpace = true;
				break;
			}
		}

		if (foundSpace)
		{
			bool itemExists = false;
			foreach(Item item in _referenceItems)
			{
				if (item._itemName == itemToAdd)
				{
					itemExists = true;
					break;
				}
			}

			if (itemExists)
			{
				_itemsHeld[newItemPosition] = itemToAdd;
				_numberHeldOfItem[newItemPosition]++;
			}
			else
			{
				Debug.LogError(itemToAdd + " New Item Not Valid!" );
			}
		}

		GameMenu.Instance.ShowItems();
	}

	public void RemoveItem(string itemToRemove)
	{
		bool foundItem = false;
		int itemPosition = 0;

		for(int i=0; i<_itemsHeld.Length; i++)
		{
			if (_itemsHeld[i] == itemToRemove)
			{
				foundItem = true;
				itemPosition = i;
				break;
			}
		}

		if (foundItem)
		{
			_numberHeldOfItem[itemPosition]--;
			if (_numberHeldOfItem[itemPosition] <= 0)
			{
				_numberHeldOfItem[itemPosition] = 0;
				_itemsHeld[itemPosition] = "";
			}
		}
		else
		{
			Debug.LogError(itemToRemove + " Item to Remove NOT FOUND!");
		}

		GameMenu.Instance.ShowItems();
	}

	public void SaveData()
	{
		//current scene
		PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
		//player's position
		PlayerPrefs.SetFloat("Player_Position_x", PlayerController.Instance.transform.position.x);
		PlayerPrefs.SetFloat("Player_Position_y", PlayerController.Instance.transform.position.y);
		PlayerPrefs.SetFloat("Player_Position_z", PlayerController.Instance.transform.position.z);
		//in dungeon...
		if (_inDungeon)
		{
			PlayerPrefs.SetFloat("DungeonPlayer_Position_x", OldSchoolFPC.Instance.transform.position.x);
			PlayerPrefs.SetFloat("DungeonPlayer_Position_y", OldSchoolFPC.Instance.transform.position.y);
			PlayerPrefs.SetFloat("DungeonPlayer_Position_z", OldSchoolFPC.Instance.transform.position.z);
		}
		//character infos...
		for (int i=0; i<_playerStats.Length; i++)
		{
			if (_playerStats[i].gameObject.activeSelf)
			{
				PlayerPrefs.SetInt("Player_" + _playerStats[i]._charName + "_active", 1);
			}
			else
			{
				PlayerPrefs.SetInt("Player_" + _playerStats[i]._charName + "_active", 0);
			}

			PlayerPrefs.SetInt("Player_" + _playerStats[i]._charName + "_level", _playerStats[i]._charLevel);
			PlayerPrefs.SetInt("Player_" + _playerStats[i]._charName + "_currentEXP", _playerStats[i]._currentEXP);
			PlayerPrefs.SetInt("Player_" + _playerStats[i]._charName + "_currentHP", _playerStats[i]._currentHP);
			PlayerPrefs.SetInt("Player_" + _playerStats[i]._charName + "_maxHP", _playerStats[i]._maxHP);
			PlayerPrefs.SetInt("Player_" + _playerStats[i]._charName + "_currentMP", _playerStats[i]._currentMP);
			PlayerPrefs.SetInt("Player_" + _playerStats[i]._charName + "_maxMP", _playerStats[i]._maxMP);
			PlayerPrefs.SetInt("Player_" + _playerStats[i]._charName + "_STR", _playerStats[i]._strength);
			PlayerPrefs.SetInt("Player_" + _playerStats[i]._charName + "_DEF", _playerStats[i]._defense);
			PlayerPrefs.SetInt("Player_" + _playerStats[i]._charName + "_wpnPWR", _playerStats[i]._weaponPwr);
			PlayerPrefs.SetInt("Player_" + _playerStats[i]._charName + "_armPWR", _playerStats[i]._armorPwr);
			PlayerPrefs.SetString("Player_" + _playerStats[i]._charName + "_equippedWpn", _playerStats[i]._equippedWpn);
			PlayerPrefs.SetString("Player_" + _playerStats[i]._charName + "_equippedArm", _playerStats[i]._equippedArm);
		}
		//inventory data
		for(int i=0; i<_itemsHeld.Length; i++)
		{
			PlayerPrefs.SetString("ItemInInventory_" + i, _itemsHeld[i]);
			PlayerPrefs.SetInt("ItemAmount_" + i, _numberHeldOfItem[i]);
		}
		//misc data...
		if (_inDungeon)
			PlayerPrefs.SetInt("InDungeon", 1);
		else
			PlayerPrefs.SetInt("InDungeon", 0);
	}

	public void LoadData()
	{
		_isContinuedGame = true;

		//Misc...
		if (PlayerPrefs.GetInt("InDungeon") == 0)
			_inDungeon = false;
		else
			_inDungeon = true;

		//player position
		if (_inDungeon)
		{
			OldSchoolFPC.Instance.transform.position = new Vector3(PlayerPrefs.GetFloat("DungeonPlayer_Position_x"), PlayerPrefs.GetFloat("DungeonPlayer_Position_y"), PlayerPrefs.GetFloat("DungeonPlayer_Position_z"));

			PlayerController.Instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));
			PlayerController.Instance.gameObject.SetActive(false);
			OldSchoolFPC.Instance.gameObject.SetActive(true);
		}
		else
			PlayerController.Instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));


		//character data
		for (int i=0; i<_playerStats.Length; i++)
		{
			if(PlayerPrefs.GetInt("Player_" + _playerStats[i]._charName + "_active") == 0)
			{
				_playerStats[i].gameObject.SetActive(false);
			}
			else
			{
				_playerStats[i].gameObject.SetActive(true);
			}

			_playerStats[i]._charLevel = PlayerPrefs.GetInt("Player_" + _playerStats[i]._charName + "_level");
			_playerStats[i]._currentEXP = PlayerPrefs.GetInt("Player_" + _playerStats[i]._charName + "_currentEXP");
			_playerStats[i]._currentHP = PlayerPrefs.GetInt("Player_" + _playerStats[i]._charName + "_currentHP");
			_playerStats[i]._maxHP = PlayerPrefs.GetInt("Player_" + _playerStats[i]._charName + "_maxHP");
			_playerStats[i]._currentMP = PlayerPrefs.GetInt("Player_" + _playerStats[i]._charName + "_currentMP");
			_playerStats[i]._maxMP = PlayerPrefs.GetInt("Player_" + _playerStats[i]._charName + "_maxMP");
			_playerStats[i]._strength = PlayerPrefs.GetInt("Player_" + _playerStats[i]._charName + "_STR");
			_playerStats[i]._defense = PlayerPrefs.GetInt("Player_" + _playerStats[i]._charName + "_DEF");
			_playerStats[i]._weaponPwr = PlayerPrefs.GetInt("Player_" + _playerStats[i]._charName + "_wpnPWR");
			_playerStats[i]._armorPwr = PlayerPrefs.GetInt("Player_" + _playerStats[i]._charName + "_armPWR");
			_playerStats[i]._equippedWpn = PlayerPrefs.GetString("Player_" + _playerStats[i]._charName + "_equippedWpn");
			_playerStats[i]._equippedArm = PlayerPrefs.GetString("Player_" + _playerStats[i]._charName + "_equippedArm");
		}
		//inventory data
		for (int i = 0; i < _itemsHeld.Length; i++)
		{
			_itemsHeld[i] = PlayerPrefs.GetString("ItemInInventory_" + i);
			_numberHeldOfItem[i] = PlayerPrefs.GetInt("ItemAmount_" + i);
		}
	}

	//public bool CheckForPlayerOutOfSyncWithGrid()
	//{
	//	bool ok = true;

	//	if (!_inDungeon)
	//	{
	//		for (int x = 2; x <= 46; x += 4)
	//		{
	//			if (OldSchoolFPC.Instance.transform.position.x == x)
	//			{
	//				ok = true;
	//				break;
	//			}
	//		}
	//		for (int z = -2; z <= 42; z += 4)
	//		{
	//			if (OldSchoolFPC.Instance.transform.position.z == z)
	//			{
	//				ok = true;
	//				break;
	//			}
	//		}
	//	}

	//	return ok;
	//}
	#endregion

	#region Private Methods


	#endregion
}
