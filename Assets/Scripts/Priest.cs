using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : MonoBehaviour
{
	#region Fields

	bool _canHeal;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		if (_canHeal && Input.GetButtonDown("Fire1") && PlayerController.Instance._canMove && !Temple.Instance._templePanel.activeInHierarchy)
		{
			Temple.Instance._templePanel.SetActive(true);
			Temple.Instance.FillHealPanel();
			Temple.Instance.UpdateGoldStatus();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_canHeal = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_canHeal = false;
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
