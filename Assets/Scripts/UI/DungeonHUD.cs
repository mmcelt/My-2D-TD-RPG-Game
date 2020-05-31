using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonHUD : MonoBehaviour
{
	#region Fields

	public static DungeonHUD Instance;

	[Header("Compass")]
	[SerializeField] Image _compass;
	[SerializeField] Sprite[] _needlePoints;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);

		//DontDestroyOnLoad(gameObject);
	}

	void Start() 
	{
		
	}
	
	void Update() 
	{
		
	}
	#endregion

	#region Public Methods

	public void SetCompassNeedle(int direction)
	{
		switch (direction)
		{
			case 0:  //N
				_compass.sprite = _needlePoints[0];
				break;

			case 1:  //E
				_compass.sprite = _needlePoints[1];
				break;

			case 2:  //S
				_compass.sprite = _needlePoints[2];
				break;

			case 3:  //W
				_compass.sprite = _needlePoints[3];
				break;
		}
	}
	#endregion

	#region Private Methods


	#endregion
}
