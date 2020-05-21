using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door: MonoBehaviour
{
	#region Fields

	public bool _doorIsOpen;
	[SerializeField] bool _isLocked;
	[SerializeField] string _requiredKey;

	Animator _theAnim;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_theAnim = GetComponent<Animator>();
	}
	#endregion

	#region Public Methods

	public void OperateDoor()
	{
		if (!_isLocked)
		{
			if (!_doorIsOpen)
			{
				_doorIsOpen = true;
				_theAnim.SetTrigger("openDoor");
			}
			else
			{
				_doorIsOpen = false;
				_theAnim.SetTrigger("closeDoor");
			}
		}
	}
	#endregion

	#region Private Methods


	#endregion
}
