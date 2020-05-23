using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{	
	#region Fields

	public enum DIRECTION { UP,DOWN,LEFT,RIGHT }

	bool _canMove = true, _moving = false;
	int _speed = 5, _buttonCooldown;
	DIRECTION _dir = DIRECTION.DOWN;
	Vector3 _pos;

	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	
	void Update() 
	{
		if (_canMove)
		{
			DoMove();
		}

		if (_moving)
		{
			if (transform.position == _pos)
			{
				_moving = false;
				_canMove = true;

				DoMove();
			}
			transform.position = Vector3.MoveTowards(transform.position, _pos, Time.deltaTime * _speed);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	void DoMove()
	{
		_buttonCooldown--;

		if (_buttonCooldown <= 0)
		{
			if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
			{
				if (_dir != DIRECTION.UP)
				{
					_buttonCooldown = 5;
					_dir = DIRECTION.UP;
				}
				else
				{
					_canMove = false;
					_moving = true;
					_pos += Vector3.up;
				}
			}
			else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
			{
				if (_dir != DIRECTION.DOWN)
				{
					_buttonCooldown = 5;
					_dir = DIRECTION.DOWN;
				}
				else
				{
					_canMove = false;
					_moving = true;
					_pos += Vector3.down;
				}
			}
			else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow))
			{
				if (_dir != DIRECTION.LEFT)
				{
					_buttonCooldown = 5;
					_dir = DIRECTION.LEFT;
				}
				else
				{
					_canMove = false;
					_moving = true;
					_pos += Vector3.left;
				}
			}
			else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow))
			{
				if (_dir != DIRECTION.RIGHT)
				{
					_buttonCooldown = 5;
					_dir = DIRECTION.RIGHT;
				}
				else
				{
					_canMove = false;
					_moving = true;
					_pos += Vector3.right;
				}
			}
		}
	}
	#endregion
}
