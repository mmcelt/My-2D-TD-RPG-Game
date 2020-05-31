using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using UnityEditor;

public class OldSchoolFPC : MonoBehaviour
{
	#region Fields

	public static OldSchoolFPC Instance;

	[SerializeField] GameObject _theCamera, _heldLight;
	public float _lightIntensity, _lightLifetime;
	[SerializeField] int _gridStep = 4;
	[SerializeField] float _moveSpeed = 5f;
	[SerializeField] float _rotSpeed = 1f;
	[SerializeField] LayerMask _dungeonWalls;
	[SerializeField] float _rayLength = 2f;
	[SerializeField] float _moveDelayTime = 0.1f;
	public bool _canMove, _haveLight;
	public int _musicToPlay;

	Vector3 _origPos;
	float _origRotY, _newPos;
	bool _rotationInProgress, _movementInProgress, _cameraLookingDown, _lightOn;

	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
			Destroy(gameObject);

		//DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		_canMove = true;
		GameManager.Instance._inDungeon = true;
		_origPos = transform.localPosition;
		Cursor.lockState = CursorLockMode.None;
		AudioManager.Instance.PlayMusic(_musicToPlay);
	}

	void Update()
	{
		if (_canMove && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
		{
			float translation = _gridStep * _moveSpeed;
			transform.Translate(0f, 0f, translation);
		}
		//do an about-face...
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
		{
			_rotationInProgress = true;
			_origPos = transform.position;

			StartCoroutine(RotatePlayer(Vector3.up * 180));
		}
		//turn 90 deg to the right...
		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
		{
			_rotationInProgress = true;
			StartCoroutine(RotatePlayer(Vector3.up * 90));
		}
		//turn 90 deg to the left...
		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
		{
			_rotationInProgress = true;
			StartCoroutine(RotatePlayer(Vector3.up * -90));
		}

		//Mouse Operations
		if (Input.GetMouseButtonDown(0) && !GameManager.Instance._battleActive)
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
				//Revealing Hidden Doorways
				HiddenDoor hiddenDoor = hit.transform.GetComponent<HiddenDoor>();
				if (hiddenDoor)
				{
					hiddenDoor.RevealDoorway();
				}
			}
		}
		//Light
		if (_haveLight && !_lightOn)
		{
			StartCoroutine(ShowLight());
		}
		if (_haveLight && _lightOn)
			_lightLifetime -= Time.deltaTime;
	}

	void FixedUpdate()
	{
		if (Physics.Raycast(transform.position, transform.forward, _rayLength, _dungeonWalls))
		{
			_canMove = false;
		}
		else
		{
			_canMove = true;
		}
	}
	#endregion

	#region Public Methods

	public void AdjustCamera()
	{
		if (!_cameraLookingDown)
		{
			_theCamera.transform.localEulerAngles = new Vector3(20, 0, 0);
			_cameraLookingDown = true;
		}
		else
		{
			_theCamera.transform.localEulerAngles = Vector3.zero;
			_cameraLookingDown = false;
		}
	}
	#endregion

	#region Private Methods

	//bool ValidDestination()
	//{
	//	Ray ray = new Ray(transform.position + new Vector3(0, 0.25f, 0), transform.forward);
	//	RaycastHit hit;

	//	Debug.DrawRay(ray.origin, ray.direction, Color.red);

	//	if (Physics.Raycast(ray, out hit, _rayLength))
	//	{
	//		if (hit.collider.CompareTag("Wall"))
	//		{
	//			return false;
	//		}
	//	}
	//	return true;
	//}

	void MovePlayer()
	{
		Debug.Log("Y ROT: " + _origRotY);

		switch ((int)_origRotY)
		{
			case 0:
				//_newPos = _origPos.z + _gridStep;

				//transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, _newPos), Time.deltaTime * _moveSpeed);

				//if (transform.localPosition.z >= _newPos - 0.04f)
				//{
				//	transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Round(_newPos));
				//}
				StartCoroutine(Move0Routine());
				//_movementInProgress = false;

				break;

			case 90:
				//_newPos = _origPos.x + _gridStep;
				//transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(_newPos, transform.localPosition.y, transform.localPosition.z), Time.deltaTime * _moveSpeed);

				//if (transform.localPosition.x >= _newPos - 0.04f)
				//{
				//	transform.localPosition = new Vector3(Mathf.Round(_newPos), transform.localPosition.y, transform.localPosition.z);
				//	_movementInProgress = false;
				//}
				StartCoroutine(Move90Routine());
				//_movementInProgress = false;
				break;

			case 180:
				//_newPos = _origPos.z - _gridStep;
				//transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, _newPos), Time.deltaTime * _moveSpeed);

				//if (transform.localPosition.z <= _newPos + 0.04f)
				//{
				//	transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Round(_newPos));
				//	_movementInProgress = false;
				//}
				StartCoroutine(Move180Routine());
				//_movementInProgress = false;
				break;

			case -90:
			case 270:
				//_newPos = _origPos.x - _gridStep;
				//transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(_newPos, transform.localPosition.y, transform.localPosition.z), Time.deltaTime * _moveSpeed);

				//if (transform.localPosition.x <= _newPos + 0.04f)
				//{
				//	transform.localPosition = new Vector3(Mathf.Round(_newPos), transform.localPosition.y, transform.localPosition.z);
				//	_movementInProgress = false;
				//}
				StartCoroutine(Move270Routine());
				//_movementInProgress = false;
				break;
		}
		//_movementInProgress = false;
	}

	IEnumerator Move0Routine()
	{
		for (int i = 1; i <= _gridStep; i++)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
			yield return new WaitForSeconds(_moveDelayTime);
		}
		_movementInProgress = false;
		StopAllCoroutines();
	}

	IEnumerator Move90Routine()
	{
		for (int i = 1; i <= _gridStep; i++)
		{
			transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
			yield return new WaitForSeconds(_moveDelayTime);
		}
		_movementInProgress = false;
		StopAllCoroutines();
	}

	IEnumerator Move180Routine()
	{
		for (int i = 1; i <= _gridStep; i++)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
			yield return new WaitForSeconds(_moveDelayTime);
		}
		_movementInProgress = false;
		StopAllCoroutines();
	}

	IEnumerator Move270Routine()
	{
		for (int i = 1; i <= _gridStep; i++)
		{
			transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
			yield return new WaitForSeconds(_moveDelayTime);
		}
		_movementInProgress = false;
		StopAllCoroutines();
	}

	IEnumerator RotatePlayer(Vector3 byAngle)
	{
		Vector3 origRotation = transform.eulerAngles;

		var fromAngle = transform.localRotation;
		var toAngle = Quaternion.Euler(transform.eulerAngles + byAngle);
		for (var t = 0f; t < 1; t += Time.deltaTime / _rotSpeed)
		{
			transform.localRotation = Quaternion.Slerp(fromAngle, toAngle, t);
			yield return null;
		}
		if (transform.localRotation.y != fromAngle.y + byAngle.y)
		{
			float newY = origRotation.y + byAngle.y;
			transform.localRotation = Quaternion.Euler(origRotation.x, Mathf.Round(newY), origRotation.z);
		}
		_rotationInProgress = false;
		StopAllCoroutines();
	}

	IEnumerator ShowLight()
	{
		_lightOn = true;
		_heldLight.SetActive(true);
		_heldLight.GetComponent<FlickeringLight>()._baseIntensity = _lightIntensity;
		yield return new WaitWhile(() => _lightLifetime >= 0f);
		TurnOffLight();
	}

	void TurnOffLight()
	{
		_haveLight = false;
		_lightOn = false;
		_lightLifetime = 0;
		_heldLight.SetActive(false);
	}
	#endregion
}
