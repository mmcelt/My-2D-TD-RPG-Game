using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
	#region Fields

	[SerializeField] bool _isLocked;
	[SerializeField] string _requiredKey;
	[SerializeField] float _infoPanelShowTime = 1f;

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
		_theAnim.SetTrigger("openChest");
		_open = true;

		if (!_alreadyLooted)
			_bootyGiver.GiveBooty();
	}

	void CloseChest()
	{
		_theAnim.SetTrigger("closeChest");
		_open = false;
	}

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
