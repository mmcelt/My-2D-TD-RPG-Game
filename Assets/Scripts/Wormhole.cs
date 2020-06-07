using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraFading;

public class Wormhole: MonoBehaviour
{
	#region Fields

	[SerializeField] Transform _destination;
	[SerializeField] float _fadingTime;

	#endregion

	#region MonoBehaviour Methods

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			//CameraFade.Out();

			CameraFade.Out(() =>
			{
				OldSchoolFPC.Instance.transform.position = _destination.position;
				CameraFade.In(_fadingTime);
			}, _fadingTime);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
