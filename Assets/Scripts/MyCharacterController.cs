﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterController : MonoBehaviour
{
	#region Fields

	public static MyCharacterController Instance;

	[SerializeField] float _speed = 10.0f;

	bool _cursorLocked;

	public bool _canMove = true;

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
		Cursor.lockState = CursorLockMode.Locked;
		_cursorLocked = true;
	}

	void Update()
	{
		if (_canMove)
		{
			float translation = Input.GetAxis("Vertical") * _speed;
			float straffe = Input.GetAxis("Horizontal") * _speed;
			translation *= Time.deltaTime;
			straffe *= Time.deltaTime;

			transform.Translate(straffe, 0, translation);
		}

		if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (_cursorLocked)
				{
					Cursor.lockState = CursorLockMode.None;
					_cursorLocked = false;
				}

				else
				{
					Cursor.lockState = CursorLockMode.Locked;
					_cursorLocked = true;
				}
			}

		//Mouse Operations
		if (Input.GetMouseButtonDown(0) && !_cursorLocked)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
			{
				//Operating Doors
				Door door = hit.transform.GetComponent<Door>();
				if (door)
				{
					door.OperateDoor();
				}
				//Operating Boxes
				Box box = hit.transform.GetComponent<Box>();
				if (box)
				{
					box.OperateBox();
				}
				//Operating Chests
				Chest chest = hit.transform.GetComponent<Chest>();
				if (chest)
				{
					chest.OperateChest();
				}
			}
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}