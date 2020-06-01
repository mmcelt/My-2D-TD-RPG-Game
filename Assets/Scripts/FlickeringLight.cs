using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
	#region Fields

	//[SerializeField] Color _normal = new Color(0.972f, 0.838f, 0.298f, 1f);
	//[SerializeField] Color _blue = new Color(0.165f, 0.635f, 0.882f, 1f);
	public float _baseIntensity = 1f, _baseIncrementTime = 0.2f;
	public float _range = 10f;
	public Color _lightColor;
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
		_theLight.color = _lightColor;
		_theLight.intensity = Random.Range(_baseIntensity * 0.7f, _baseIntensity * 1.3f);
		yield return new WaitForSeconds(Random.Range(_baseIncrementTime * 0.7f, _baseIncrementTime * 1.3f));
		StartCoroutine(LightFlickerRoutine());
	}
	#endregion
}
