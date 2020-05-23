using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntrance : MonoBehaviour
{
	#region Fields

	public string _transitionName;
	[SerializeField] bool _inDungeon = true;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		//if (!_inDungeon && _transitionName == PlayerController.Instance._areaTransitionName)
		//{
		//	PlayerController.Instance.transform.position = transform.position;
		//}
		if(_inDungeon && _transitionName == PlayerController.Instance._areaTransitionName)
		{
			MyCharacterController.Instance.transform.position = transform.position;
		}

		UIFade.Instance.FadeFromBlack();
		GameManager.Instance._fadingBetweenAreas = false;
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
