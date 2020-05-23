using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootyGiver : MonoBehaviour
{
	#region Fields

	[SerializeField] string[] _bootyItems;
	[SerializeField] int _baseGold;
	[SerializeField] float _goldRandomFactorMin = 0.5f;
	[SerializeField] float _goldRandomFactorMax = 1.5f, _delayToShowPanel = 1.5f;

	int _bootyGold;

	Chest _chest;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_chest = GetComponent<Chest>();
		_bootyGold = (int)Random.Range(_baseGold * _goldRandomFactorMin, _baseGold * _goldRandomFactorMax);
	}
	#endregion

	#region Public Methods

	public void GiveBooty()
	{
		BootyGains.Instance._bootyItems = _bootyItems;
		BootyGains.Instance._goldFound = _bootyGold;
		StartCoroutine(BootyGains.Instance.OpenBootyScreenRoutine(_bootyGold, _bootyItems, _delayToShowPanel));
	}
	#endregion

	#region Private Methods


	#endregion
}
