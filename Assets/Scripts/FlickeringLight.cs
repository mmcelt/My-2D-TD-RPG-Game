using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
	#region Fields

	public float _baseIntensity = 1f, _baseIncrementTime = 0.2f;

	Light _theLight;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_theLight = GetComponent<Light>();
		TurnOn();
		StartCoroutine(LightFlickerRoutine());
	}
	#endregion

	#region Public Methods

	public void TurnOn()
	{
		_theLight.enabled = true;
	}

	public void TurnOff()
	{
		_theLight.enabled = false;
	}

	#endregion

	#region Private Methods

	IEnumerator LightFlickerRoutine()
	{
		_theLight.intensity = Random.Range(_baseIntensity * 0.7f, _baseIntensity * 1.3f);
		yield return new WaitForSeconds(Random.Range(_baseIncrementTime * 0.7f, _baseIncrementTime * 1.3f));
		StartCoroutine(LightFlickerRoutine());
	}
	#endregion
}
