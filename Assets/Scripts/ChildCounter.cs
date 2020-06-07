using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCounter : MonoBehaviour
{
	#region Fields

	int _children;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_children = transform.childCount;

		Debug.Log("CHILDREN: " + _children);
	}
	
	void Update() 
	{
		
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
