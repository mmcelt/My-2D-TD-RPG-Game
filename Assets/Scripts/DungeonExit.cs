using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonExit : MonoBehaviour
{
	#region Fields

	[SerializeField] string _areaToLoad;
	public string _areaTransitionName;
	[SerializeField] DungeonEntrance _theEntrance;
	[SerializeField] float _waitToLoad = 1f;
	[SerializeField] bool _exiting3D;

	bool _shouldLoadAfterFade;

	#endregion

	#region MonoBehaviour Methods

	void Awake() 
	{
		_theEntrance._transitionName = _areaTransitionName;
	}

	void Update()
	{
		if (_shouldLoadAfterFade)
		{
			_waitToLoad -= Time.deltaTime;
		}
		if (_waitToLoad <= 0)
		{
			SceneManager.LoadScene(_areaToLoad);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerController.Instance._areaTransitionName = _areaTransitionName;
			GameManager.Instance._fadingBetweenAreas = true;

			if (_exiting3D)
			{
				GameManager.Instance._inDungeon = false;
				PlayerController.Instance.gameObject.SetActive(true);
			}

			_shouldLoadAfterFade = true;
			UIFade.Instance.FadeToBlack();
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
