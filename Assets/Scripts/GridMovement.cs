using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
	#region Fields

	[SerializeField] float _speed = 5f;
	[SerializeField] int _gridSize = 4;
	float _rayLength = 1f;

	Vector3 _up = Vector3.zero, _right = new Vector3(0, 90, 0), _down = new Vector3(0, 180, 0), _left = new Vector3(0, 270, 0), _currentDir = Vector3.zero;

	Vector3 _nextPos, _destination, _direction;

	bool _canMove;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_rayLength = _gridSize * 0.5f;
		_currentDir = _up;
		_nextPos = Vector3.forward * _gridSize;
		_destination = transform.position;
	}
	
	void Update() 
	{
		Move();
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	void Move()
	{
		transform.position = Vector3.MoveTowards(transform.position, _destination, _speed * Time.deltaTime);

		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
		{
			_nextPos = Vector3.forward * _gridSize;
			_currentDir = _up;
			_canMove = true;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
		{
			_nextPos = Vector3.back * _gridSize;
			_currentDir = _down;
			_canMove = true;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
		{
			_nextPos = Vector3.right * _gridSize;
			_currentDir = _right;
			_canMove = true;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
		{
			_nextPos = Vector3.left * _gridSize;
			_currentDir = _left;
			_canMove = true;
		}
		//arrived at destination
		if (Vector3.Distance(_destination, transform.position) <= Mathf.Epsilon)
		{
			transform.localEulerAngles = _currentDir;
			if (_canMove)
			{
				if (ValidDestination())
				{
					_destination = transform.position + _nextPos;
					_direction = _nextPos;
					_canMove = false;
				}
			}
		}

		bool ValidDestination()
		{
			Ray ray = new Ray(transform.position + new Vector3(0, 0.25f, 0), transform.forward);
			RaycastHit hit;

			Debug.DrawRay(ray.origin, ray.direction, Color.red);

			if(Physics.Raycast(ray,out hit, _rayLength))
			{
				if (hit.collider.CompareTag("Wall"))
				{
					return false;
				}
			}
			return true;
		}
	}
	#endregion
}
