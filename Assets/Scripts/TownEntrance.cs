using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TownEntrance : MonoBehaviour
{
	#region Fields

	[SerializeField] string _areaToLoad;
	public string _areaTransitionName, _townName;
	[SerializeField] AreaEntrance _theEntrance;
	[SerializeField] float _waitToLoad = 1f;
	[SerializeField] GameObject _theCanvas;
	[SerializeField] Text _townNameText;

	bool _shouldLoadAfterFade, _enteredArea;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		_theEntrance._transitionName = _areaTransitionName;
	}

	void Start()
	{

	}

	void Update()
	{
		if (_shouldLoadAfterFade && _enteredArea)
		{
			_waitToLoad -= Time.deltaTime;
		}
		if (_waitToLoad <= 0)
		{
			SceneManager.LoadScene(_areaToLoad);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_townNameText.text = "Welcome to " + _townName;
			_enteredArea = true;
			Camera.main.orthographicSize = 5;
			PlayerController.Instance._areaTransitionName = _areaTransitionName;
			GameManager.Instance._fadingBetweenAreas = true;
			_theCanvas.SetActive(true);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_enteredArea = false;
			Camera.main.orthographicSize = 10;
			GameManager.Instance._fadingBetweenAreas = false;
			_theCanvas.SetActive(false);
		}
	}
	#endregion

	#region Public Methods

	public void OnYesButtonClicked()
	{
		_shouldLoadAfterFade = true;
		UIFade.Instance.FadeToBlack();
	}

	public void OnNoButtonClicked()
	{
		_enteredArea = false;
		Camera.main.orthographicSize = 10;
		_theCanvas.SetActive(false);
		GameManager.Instance._fadingBetweenAreas = false;
		PlayerController.Instance.transform.position = _theEntrance.transform.position;
	}
	#endregion

	#region Private Methods


	#endregion
}
