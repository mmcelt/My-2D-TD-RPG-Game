using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEye : MonoBehaviour
{	
	#region Fields

	
	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		FindAllHiddenDoors();
	}
	#endregion

	#region Public Methods

	public void FindAllHiddenDoors()
	{
		HiddenDoor[] hiddenDoors = FindObjectsOfType<HiddenDoor>();
		foreach (HiddenDoor doorway in hiddenDoors)
		{
			doorway._theDoorway.SetActive(true);
		}
	}
	#endregion

	#region Private Methods

	#endregion
}
