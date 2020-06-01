using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HiddenDoor : MonoBehaviour
{
	#region Fields

	public GameObject _theWall, _theDoorway;

	#endregion

	#region MonoBehaviour Methods

	#endregion

	#region Public Methods

	public void RevealDoorway()
	{
		_theWall.SetActive(false);
		_theDoorway.SetActive(true);
	}
	#endregion

	#region Private Methods


	#endregion
}
