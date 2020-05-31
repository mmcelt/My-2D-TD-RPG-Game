using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEssentialsLoader : MonoBehaviour
{
	#region Fields

	[SerializeField] GameObject _uiCanvas, _battleManager, _gameManager, _audioManager, _dungeonPlayer;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		//if (MyCharacterController.Instance == null)
		//{
		//	MyCharacterController.Instance = Instantiate(_dungeonPlayer).GetComponent<MyCharacterController>();
		//}
		if (OldSchoolFPC.Instance == null)
		{
			OldSchoolFPC.Instance = Instantiate(_dungeonPlayer).GetComponent<OldSchoolFPC>();
		}
		if (UIFade.Instance == null)
		{
			UIFade.Instance = Instantiate(_uiCanvas).GetComponent<UIFade>();
		}
		if (BattleManager.Instance == null)
		{
			BattleManager.Instance = Instantiate(_battleManager).GetComponent<BattleManager>();
		}
		if (GameManager.Instance == null)
		{
			GameManager.Instance = Instantiate(_gameManager).GetComponent<GameManager>();
		}
		//if (AudioManager.Instance == null)
		//{
		//	AudioManager.Instance = Instantiate(_audioManager).GetComponent<AudioManager>();
		//}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
