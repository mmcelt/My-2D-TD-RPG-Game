using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole: MonoBehaviour
{
	#region Fields

	[SerializeField] Transform _destination;

	#endregion

	#region MonoBehaviour Methods

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			OldSchoolFPC.Instance.transform.position = _destination.position;
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
