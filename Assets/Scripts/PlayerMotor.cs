using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {
		
	[SerializeField]private Camera cam;

	private Vector3 velocity = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	private float cameraRotationX = 0f;
	private float currentCameraRotationX = 0f;
	private bool m_cursorIsLocked = true;

	[SerializeField]private float cameraRotationLimit = 85f;

	public bool lockCursor = true;


	private Rigidbody rb; //instance

	void Start()
	{
		rb = GetComponent<Rigidbody>();	//instance

	}

	//Gets a movement vector
	public void Move(Vector3 _velocity)
	{
		velocity = _velocity;
	}
	//Gets a movement vector
	public void Rotate(Vector3 _rotation)
	{
		rotation = _rotation;
	}

	//Gets a rotational vector for the camera
	public void RotateCamera(float _cameraRotationX)
	{
		cameraRotationX = _cameraRotationX;
		UpdateCursorLock();

	}



	//Run every physics iteration
	void FixedUpdate()
	{
		PerformMovement();
		PerformRotation();
	}

	//Perform movement based on velocity variable
	void PerformMovement()
	{
		if(velocity != Vector3.zero)
		{
			rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
		}
			
	}

	//Perform Rotation
	void PerformRotation()
	{
		rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
		if (cam != null)
		{
			//Set our rotation and clamp it
			currentCameraRotationX -= cameraRotationX; //For camera to be inverted change it to +=
			currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

			//Apply our rotation to the transform of our camera
			cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
		}
	}



	//Cursor Lock Code from lines 77-115 -- Taken straight from the FirstPerson Unity Asset MouseLook script
	public void SetCursorLock(bool value)
	{
		lockCursor = value;
		if(!lockCursor)
		{//we force unlock the cursor if the user disable the cursor locking helper
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	public void UpdateCursorLock()
	{
		//if the user set "lockCursor" we check & properly lock the cursos
		if (lockCursor)
			InternalLockUpdate();
	}

	private void InternalLockUpdate()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			m_cursorIsLocked = false;
		}
		else if(Input.GetMouseButtonUp(0))
		{
			m_cursorIsLocked = true;
		}

		if (m_cursorIsLocked)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else if (!m_cursorIsLocked)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}
