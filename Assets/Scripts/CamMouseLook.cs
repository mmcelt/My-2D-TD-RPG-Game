using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMouseLook : MonoBehaviour
{
	#region Fields

	[SerializeField] float _sensitivity = 5.0f, _smoothing = 2.0f, _minY = -90f, _maxY = 90f;
	Vector2 _mouseLook, _smoothV;

	GameObject _character;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_character = transform.parent.gameObject;
	}
	
	void Update() 
	{
		if (!MyCharacterController.Instance._canMove) return;

		var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		mouseDelta = Vector2.Scale(mouseDelta, new Vector2(_sensitivity * _smoothing, _sensitivity * _smoothing));
		_smoothV.x = Mathf.Lerp(_smoothV.x, mouseDelta.x, 1f / _smoothing);
		_smoothV.y = Mathf.Lerp(_smoothV.y, mouseDelta.y, 1f / _smoothing);
		_mouseLook += _smoothV;
		_mouseLook.y = Mathf.Clamp(_mouseLook.y, _minY, _maxY);

		transform.localRotation = Quaternion.AngleAxis(-_mouseLook.y, Vector3.right);
		_character.transform.localRotation = Quaternion.AngleAxis(_mouseLook.x, _character.transform.up);
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
