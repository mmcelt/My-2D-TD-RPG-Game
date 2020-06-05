using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class Temple : MonoBehaviour
{
	#region Fields

	public static Temple Instance;

	public GameObject _templePanel, _selectPlayerPanel;
	[SerializeField] Text[] _nameTexts, _hpTexts, _maxTexts, _costTexts;
	[SerializeField] Text _goldText, _costText;
	[SerializeField] Button _healOneButton, _healAllButton, _healButton;
	[SerializeField] GameObject[] _playerButtons;

	int _goldRequiredToHealAll;
	int[] _individualCosts = new int[3];
	int _selectedPlayer;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);

		//DontDestroyOnLoad(gameObject);
	}

	void Start() 
	{
		
	}
	#endregion

	#region Public Methods

	public void HealOnePlayer()
	{
		_selectPlayerPanel.SetActive(true);

		for (int i = 0; i < _playerButtons.Length; i++)
		{
			if (_individualCosts[i] == 0 || _individualCosts[i] > GameManager.Instance._currentGold)
				_playerButtons[i].SetActive(false);
		}
	}

	public void HealPlayer(int playerIndex)
	{
		_selectedPlayer = playerIndex;
		_costText.text = _individualCosts[_selectedPlayer] + "g";
		_healButton.interactable = true;
	}

	public void HealSelectedPlayer()
	{
		GameManager.Instance._currentGold -= _individualCosts[_selectedPlayer];
		GameManager.Instance._playerStats[_selectedPlayer]._currentHP = GameManager.Instance._playerStats[_selectedPlayer]._maxHP;
		UpdateGoldStatus();
		FillHealPanel();
		_healButton.interactable = false;
		_costText.text = "0g";
		_selectPlayerPanel.SetActive(false);
	}

	public void HealAllPlayers()
	{
		for(int i=0; i<GameManager.Instance._playerStats.Length; i++)
		{
			if(GameManager.Instance._playerStats[i]._currentHP== GameManager.Instance._playerStats[i]._maxHP)
			{
				continue;
			}

			GameManager.Instance._playerStats[i]._currentHP = GameManager.Instance._playerStats[i]._maxHP;
			GameManager.Instance._currentGold -= _individualCosts[i];
			UpdateGoldStatus();
		}

		FillHealPanel();
	}

	public void UpdateGoldStatus()
	{
		foreach(CharSats player in GameManager.Instance._playerStats)
		{
			if (player._currentHP == player._maxHP)
				continue;

			if (player._isDead)
			{
				_goldRequiredToHealAll += player._maxHP * player._charLevel;
			}
			_goldRequiredToHealAll += player._maxHP - player._currentHP;
		}

		if (_goldRequiredToHealAll > 0 && _goldRequiredToHealAll <= GameManager.Instance._currentGold)
		{
			_healAllButton.interactable = true;
		}
		else
		{
			_healAllButton.interactable = false;
		}
	}
	#endregion

	#region Private Methods

	public void FillHealPanel()
	{
		for(int i=0; i<GameManager.Instance._playerStats.Length; i++)
		{
			CharSats player = GameManager.Instance._playerStats[i];

			_nameTexts[i].text = player._charName;
			_hpTexts[i].text = player._currentHP.ToString();
			_maxTexts[i].text = player._maxHP.ToString();
			_costTexts[i].text = DetermineGoldRequiredToHealPlayer(player, i) + "g";
			_individualCosts[i] = DetermineGoldRequiredToHealPlayer(player, i);
			_playerButtons[i].GetComponentInChildren<Text>().text = player._charName;
		}
		_goldText.text = GameManager.Instance._currentGold + "g";
	}

	int DetermineGoldRequiredToHealPlayer(CharSats player ,int index)
	{
		int cost = 0;
		if (player._isDead)
		{
			cost += player._maxHP * player._charLevel;
		}
		cost += player._maxHP - player._currentHP;

		return cost;
	}
	#endregion
}
