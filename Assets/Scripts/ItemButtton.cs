using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtton : MonoBehaviour
{
	#region Fields

	public Image _buttonImage;
	public Text _amountText;
	public int _buttonValue;

	#endregion

	#region MonoBehaviour Methods

	#endregion

	#region Public Methods

	public void OnItemButtonClicked()
	{
		if (GameMenu.Instance._theMenu.activeInHierarchy)
		{
			if (GameManager.Instance._itemsHeld[_buttonValue] != "")
			{
				GameMenu.Instance.SelectItem(GameManager.Instance.GetItemDetails(GameManager.Instance._itemsHeld[_buttonValue]));

				if (GameManager.Instance.GetItemDetails(GameManager.Instance._itemsHeld[_buttonValue])._isSpell)
				{
					if (CheckForValidUser() && CheckForSufficientManaForSpells())
					{
						GameMenu.Instance._useButton.interactable = true;
						GameMenu.Instance._dropButton.interactable = true;
					}
					else
					{
						GameMenu.Instance._useButton.interactable = false;
						GameMenu.Instance._dropButton.interactable = false;
						StartCoroutine(GameMenu.Instance.ShowInfoPanel("Not enough personal Mana!", 1.5f));
					}
				}
				else if (CheckForValidUser())
				{
					GameMenu.Instance._useButton.interactable = true;
					GameMenu.Instance._dropButton.interactable = true;
				}

			}
			else
			{
				GameMenu.Instance._useButton.interactable = false;
				GameMenu.Instance._dropButton.interactable = false;
			}
		}
		if (Shop.Instance._shopMenu.activeSelf)
		{
			if (Shop.Instance._buyMenu.activeSelf)
			{
				if (Shop.Instance._itemsForSale[_buttonValue] != "")
				{
					Shop.Instance.SelectBuyItem(GameManager.Instance.GetItemDetails(Shop.Instance._itemsForSale[_buttonValue]));

					Shop.Instance._buyButton.interactable = true;
				}
				else
				{
					Shop.Instance._buyButton.interactable = false;
					Shop.Instance._buyItemName.text = "";
					Shop.Instance._buyItemDesc.text = "";
					Shop.Instance._buyItemValue.text = "Value:";
				}
			}

			if (Shop.Instance._sellMenu.activeSelf)
			{
				if (GameManager.Instance._itemsHeld[_buttonValue] != "")
				{
					Shop.Instance.SelectSellItem(GameManager.Instance.GetItemDetails(GameManager.Instance._itemsHeld[_buttonValue]));

					Shop.Instance._sellButton.interactable = true;
				}
				else
				{
					Shop.Instance._sellButton.interactable = false;
					Shop.Instance._sellItemName.text = "";
					Shop.Instance._sellItemDesc.text = "";
					Shop.Instance._sellItemValue.text = "Value:";
				}
			}
		}

		if (GameManager.Instance._battleActive)
		{
			if(GameManager.Instance._itemsHeld[_buttonValue] != "")
			{
				BattleManager.Instance.SelectItem(GameManager.Instance.GetItemDetails(GameManager.Instance._itemsHeld[_buttonValue]));
				BattleManager.Instance._useButton.interactable = true;
			}
			else
			{
				BattleManager.Instance._useButton.interactable = false;
			}
		}
	}
	#endregion

	#region Private Methods

	bool CheckForSufficientManaForSpells()
	{
		bool requiredMana = false;

		//check if a player has enough mana to cast the spell...
		for (int i = 0; i < GameManager.Instance._playerStats.Length; i++)
		{
			if (GameManager.Instance._playerStats[i]._currentMP >= GameManager.Instance.GetItemDetails(GameManager.Instance._itemsHeld[_buttonValue])._manaCost)
			{
				GameMenu.Instance._selectCharacterButtons[i].enabled = true;
				requiredMana = true;
			}
			else
			{
				GameMenu.Instance._selectCharacterButtons[i].enabled = false;
			}
		}

		return requiredMana;
	}

	bool CheckForValidUser()
	{
		bool validUser = false;

		//check if a player can use the item...
		for (int i = 0; i < GameManager.Instance._playerStats.Length; i++)
		{
			//if (GameManager.Instance.GetItemDetails(GameManager.Instance._itemsHeld[_buttonValue])._forAverage)
			//{
			//	GameMenu.Instance._selectCharacterButtons[i].enabled = true;
			//	validUser = true;
			//}
			if(GameManager.Instance.GetItemDetails(GameManager.Instance._itemsHeld[_buttonValue])._forWarrior)
			{
				if(GameManager.Instance._playerStats[i]._playerClass == CharSats.CLASS.WARRIOR)
				{
					GameMenu.Instance._selectCharacterButtons[i].enabled = true;
					validUser = true;
				}
				else
				{
					GameMenu.Instance._selectCharacterButtons[i].enabled = false;
				}
			}
			else if (GameManager.Instance.GetItemDetails(GameManager.Instance._itemsHeld[_buttonValue])._forMage)
			{
				if (GameManager.Instance._playerStats[i]._playerClass == CharSats.CLASS.MAGE)
				{
					GameMenu.Instance._selectCharacterButtons[i].enabled = true;
					validUser = true;
				}
				else
				{
					GameMenu.Instance._selectCharacterButtons[i].enabled = false;
				}
			}
		}

		return validUser;
	}
		#endregion
}
