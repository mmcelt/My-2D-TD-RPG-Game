using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BootyGains: MonoBehaviour
{
	#region Fields

	public static BootyGains Instance;

	[SerializeField] GameObject _bootyPanel;
	[SerializeField] Text _itemsText, _goldText;
	public string[] _bootyItems;
	public int _goldFound;

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
	//	if (Input.GetKeyDown(KeyCode.B))
	//	{
	//		OpenBootyScreenRoutine(100, new string[] { "Iron Sword", "Mana Potion" }, 0.5f);
	//	}
	//}
	#endregion

	#region Public Methods

	public IEnumerator OpenBootyScreenRoutine(int gold, string[] items, float panelDelayTime)
	{
		GameManager.Instance._bootyPanelOpen = true;

		_goldFound = gold;
		_bootyItems = items;

		_goldText.text = _goldFound + "g";
		_itemsText.text = "";

		foreach(string item in _bootyItems)
			_itemsText.text += item + "\n";

		yield return new WaitForSeconds(panelDelayTime);

		_bootyPanel.SetActive(true);
	}

	public void CloseBootyPanel()
	{
		for (int i = 0; i < _bootyItems.Length; i++)
		{
			GameManager.Instance.AddItem(_bootyItems[i]);
		}

		GameManager.Instance._currentGold += _goldFound;
		_bootyPanel.SetActive(false);
		GameManager.Instance._bootyPanelOpen = false;

	}
	#endregion

	#region Private Methods


	#endregion
}
