using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGridMovement : MonoBehaviour
{
	#region Fields

	[SerializeField] float _speed = 5f;
	[SerializeField] int _gridSize = 4;
	float _rayLength = 1f;

	Vector3 _up = Vector3.zero, _right = new Vector3(0, 90, 0), _down = new Vector3(0, 180, 0), _left = new Vector3(0, 270, 0), _currentDir = Vector3.zero;

	Vector3 _nextPos, _destination, _direction, _lookDirection;
	Quaternion _startRotation;
	Quaternion _targetRotation;
	float _timeCount;

	[SerializeField] bool _canMove, _canRotate;

	float distanceTraveled = 0;

	#endregion

	#region MonoBehaviour Methods

	void Start()
	{
		_rayLength = _gridSize * 0.5f;
		_currentDir = _up;
		_nextPos = Vector3.forward * _gridSize;
		_destination = transform.position;

		Debug.Log(transform.forward);
	}

	void Update()
	{
		if (!_canRotate || !_canMove)
			GetInput();

		if (_canRotate)
			Rotate();

		if (_canMove)
			Movement();
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	void GetInput()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
		{
			if (ValidDestination())
			{
				_nextPos = Vector3.up * _gridSize;
				_destination = _nextPos;
				_canMove = true;
			}
		}
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
		{
			_startRotation = transform.rotation;
			_targetRotation = transform.rotation *= Quaternion.Euler(0, 180f, 0);
			_canRotate = true;
		}

		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
		{
			_startRotation = transform.rotation;
			_targetRotation = transform.rotation *= Quaternion.Euler(0, 90f, 0);
			_canRotate = true;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
		{
			_startRotation = transform.rotation;
			_targetRotation = transform.rotation *= Quaternion.Euler(0, -90f, 0);
			_canRotate = true;
		}
	}

	void Rotate()
	{
		transform.rotation = Quaternion.Slerp(_startRotation, _targetRotation, _timeCount);
		_timeCount += Time.deltaTime;

		if (Quaternion.Angle(transform.rotation, _targetRotation) < 0.1f)
		{
			Debug.Log(Quaternion.Angle(transform.localRotation, _targetRotation));
			_canRotate = false;
			_timeCount = 0.0f;
			transform.rotation = _targetRotation;
		}
	}

	void Movement()
	{
		transform.position = Vector3.MoveTowards(transform.position, _destination, _speed * Time.deltaTime);


		if (distanceTraveled < 4f)
		{
			Vector3 oldPosition = transform.position;
			transform.Translate(Vector3.forward * Time.deltaTime);
			distanceTraveled += Vector3.Distance(oldPosition, transform.position);
			Debug.Log(distanceTraveled);
		}
			//transform.Translate(Vector3.forward * _speed * _gridSize * Time.deltaTime);
		//arrived at destination
		if (Vector3.Distance(_destination, transform.position) <= Mathf.Epsilon)
		{
			//transform.localEulerAngles = _currentDir;
			//if (_canMove)
			//{
			//	if (ValidDestination())
			//	{
			//		_direction = _nextPos;
			//		_canMove = false;
			//	}
			//}
		}
	}

	bool ValidDestination()
	{
		Ray ray = new Ray(transform.position + new Vector3(0, 0.25f, 0), transform.forward);
		RaycastHit hit;

		Debug.DrawRay(ray.origin, ray.direction, Color.red);

		if (Physics.Raycast(ray, out hit, _rayLength))
		{
			if (hit.collider.CompareTag("Wall"))
			{
				return false;
			}
		}
		return true;
	}
	#endregion
}
