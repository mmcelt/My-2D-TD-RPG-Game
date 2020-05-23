using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
	#region Fields

	[SerializeField] bool _isLocked;
	[SerializeField] string _requiredKey;
	[SerializeField] float _infoPanelShowTime = 1f;
	
	bool open;
	Animation _theAnim;
	BootyGiver _bootyGiver;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_theAnim = GetComponent<Animation>();
		_bootyGiver = GetComponent<BootyGiver>();
	}
	#endregion

	#region Public Methods

	public void OperateBox()
	{
		if (!_isLocked)
		{
			if (!open)
			{
				Debug.Log("IN OPERATE BOX");
				_theAnim["ChestAnim"].speed = 1.0f;
				_theAnim.Play();
				open = true;
				_bootyGiver.GiveBooty();
			}
			else
			{
				_theAnim["ChestAnim"].time = _theAnim["ChestAnim"].length;
				_theAnim["ChestAnim"].speed = -1.0f;
				_theAnim.Play();
				open = false;
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
						_theAnim["ChestAnim"].speed = 1.0f;
						open = true;
						_bootyGiver.GiveBooty();
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
