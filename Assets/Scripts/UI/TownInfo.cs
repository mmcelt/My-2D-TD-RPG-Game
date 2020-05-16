using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownInfo : MonoBehaviour
{
	#region Fields

	[SerializeField] string _townName;
	[SerializeField] GameObject _canvas;
	[SerializeField] Text _townText;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_townText.text = "Welcome to " + _townName;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_canvas.SetActive(true);
			Camera.main.orthographicSize = 5;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_canvas.SetActive(false);
			Camera.main.orthographicSize = 10;
		}
	}
	#endregion

	#region Public Methods

	public void OnYesButtonClicked()
	{

	}

	public void OnNoButtonClicked()
	{
		_canvas.SetActive(false);
		Camera.main.orthographicSize = 10;
	}
	#endregion

	#region Private Methods


	#endregion
}
