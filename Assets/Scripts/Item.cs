using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
	#region Fields

	[Header("Item Type")]
	public bool _isItem;
	public bool _isLight;
	public bool _isSpell;
	public bool _isWeapon;
	public bool _isArmor;
	[Header("Item Info")]
	public Sprite _itemSprite;
	public string _itemName, _itemDesc;
	public int _itemValue;
	[Header("Item Details")]
	public int _amountToChange;
	public bool _affectHP, _affectMP, _affectSTR, _affectDEF, _stopCurse;
	public int _weaponStr, _armorStr;
	[Header("Light Info")]
	public int _lightUIIndex;
	public float _intensity;
	public float _lifetime;
	public float _range;
	[Header("Spell Info")]
	public int _manaCost;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	#endregion

	#region Public Methods

	public void Use(int charToUseOn)
	{
		CharSats selectedChar = GameManager.Instance._playerStats[charToUseOn];

		if (_isItem)
		{
			if (_affectHP)
			{
				if(selectedChar._isDead)
				{
					selectedChar._currentHP += 5;
					selectedChar._isDead = false;
				}
				else
				{
					selectedChar._currentHP = Mathf.Min(selectedChar._currentHP + _amountToChange, selectedChar._maxHP);
				}
			}
			if (_affectMP)
			{
				selectedChar._currentMP = Mathf.Min(selectedChar._currentMP + _amountToChange, selectedChar._maxMP);
			}
			if (_affectSTR)
			{
				selectedChar._strength += _amountToChange;
			}
			if (_affectDEF)
			{
				selectedChar._defense += _amountToChange;
			}
			if (_stopCurse)
			{
				GameManager.Instance._isCursed = false;
				foreach(CharSats player in GameManager.Instance._playerStats)
				{
					if(player.gameObject.activeSelf)
						player._currentHP = player._maxHP;
				}
			}
		}
		if (_isWeapon)
		{
			if (selectedChar._equippedWpn != "")
			{
				//put the equipped weapon back in inventory
				GameManager.Instance.AddItem(selectedChar._equippedWpn);

				selectedChar._equippedWpn = _itemName;
				selectedChar._weaponPwr = _weaponStr;
			}
			else
			{
				selectedChar._equippedWpn = _itemName;
				selectedChar._weaponPwr = _weaponStr;
			}
		}
		if (_isArmor)
		{
			if(selectedChar._equippedArm != "")
			{
				//put the equipped armor back in inventory
				GameManager.Instance.AddItem(selectedChar._equippedArm);

				selectedChar._equippedArm = _itemName;
				selectedChar._armorPwr = _armorStr;
			}
			else
			{
				selectedChar._equippedArm = _itemName;
				selectedChar._armorPwr = _armorStr;
			}
		}
		if (_isLight)
		{
			if (GameManager.Instance._inDungeon)
			{
				OldSchoolFPC.Instance.TurnOnLight(_intensity, _range, _lifetime);
				DungeonHUD.Instance.ShowLightIcon(_lightUIIndex);
			}
		}
		if (_isSpell)
		{
			if (_itemName == "Magic Eye")
				GetComponent<MagicEye>().FindAllHiddenDoors();

			GameManager.Instance._playerStats[charToUseOn]._currentMP -= _manaCost;
		}

		//remove the item from the inventory...
		GameManager.Instance.RemoveItem(_itemName);

		GameMenu.Instance._useButton.interactable = false;
		GameMenu.Instance._dropButton.interactable = false;
		foreach (Button btn in GameMenu.Instance._selectCharacterButtons)
			btn.enabled = true;
	}

	public void UseForBattle(int battler)
	{
		BattleChar selectedBattler = BattleManager.Instance._activeBattlers[battler];
		if (_isItem)
		{
			if (_affectHP)
			{
				if (selectedBattler._hasDied)
					selectedBattler._currentHP += 5;
				else
					selectedBattler._currentHP += _amountToChange;

				if (selectedBattler._currentHP > selectedBattler._maxHP)
					selectedBattler._currentHP = selectedBattler._maxHP;
			}

			if (_affectMP)
			{
				selectedBattler._currentMP += _amountToChange;
				if (selectedBattler._currentMP > selectedBattler._maxMP)
					selectedBattler._currentMP = selectedBattler._maxMP;
			}

			if (_affectSTR)
			{
				selectedBattler._STR += _amountToChange;  //this should be a small amount
			}
		}

		if (_isWeapon)
		{
			if (selectedBattler._equippedWpn != "") //character already has a weapon equipped
			{
				//return the existing equipped weapon to the inventory
				GameManager.Instance.AddItem(selectedBattler._equippedWpn);
			}
			selectedBattler._equippedWpn = _itemName;
			selectedBattler._wpnPwr = _weaponStr;
		}

		if (_isArmor)
		{
			if (selectedBattler._equippedArm != "") //character already has armor equipped
			{
				//return the existing equipped armor to the inventory
				GameManager.Instance.AddItem(selectedBattler._equippedArm);
			}
			selectedBattler._equippedArm = _itemName;
			selectedBattler._armPwr = _armorStr;
		}
		GameManager.Instance.RemoveItem(_itemName);   //remove the item from the inventory
	}

	#endregion

	#region Private Methods


	#endregion
}
