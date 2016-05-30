using UnityEngine;
using System.Collections;

public class FirstPerson_Movement : MonoBehaviour {

	public float walkSpeed;
	public float runSpeed;
	public float crouchSpeed;
	float movingSpeed;

	public float jumpForce;

	public float mouseSensitivity;
	public float upDownRange = 60;  // We use this to limit how much down or up can be watched in angle. 
	public float gravity;

	float verticalRot = 0;			// We use these to rotate player with mouse.
	float verticalVelocity = 0;

	CharacterController cc;

	public GameObject cameraHolder;
	public Camera playerCamera;

	private float playerHeight;
	public LayerMask CrouchCollision;

	bool locked = false;

	public bool lisenceToMove;

	void Start()
	{
		if(cc == null)
		{
			this.gameObject.AddComponent<CharacterController>();
			cc = this.gameObject.GetComponent<CharacterController>();
		}

		playerHeight = cc.height;	// 
		playerCamera.transform.position = cameraHolder.transform.position;
		cameraHolder.transform.localPosition = cc.center = new Vector3(0, cc.height / 2, 0); // We set camera's position to player.
	}

	void Update()
	{

		// Open / Close mouse lock
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			locked = !locked;
			if ( locked ) Cursor.lockState = CursorLockMode.Confined;
			else Cursor.lockState = CursorLockMode.None;
		}

		if(lisenceToMove)
		{

		float h = playerHeight;

		if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
		{
			movingSpeed = runSpeed;
		}
		else if (Input.GetKey(KeyCode.LeftControl) || Physics.Raycast(transform.position, Vector3.up, playerHeight, CrouchCollision, QueryTriggerInteraction.Ignore)) 
		{
			movingSpeed = crouchSpeed;
			h = h * 0.5f;
		}
		else 
		{
			movingSpeed = walkSpeed;
		}

		// Crouch

		//float lastHeight = cc.height;
		cc.height = Mathf.Lerp(cc.height, h, 5 * Time.deltaTime);

		cameraHolder.transform.localPosition = cc.center = new Vector3(0, cc.height / 2, 0); // We set cameras position to player.

		// Rotation
		if (!locked && !Input.GetKey(KeyCode.Mouse1))
		{
			float mouseRotSide = Input.GetAxis("Mouse X") * mouseSensitivity;
			this.transform.Rotate(0, mouseRotSide, 0);								// rotate player in Y with mouse

			verticalRot -= Input.GetAxis("Mouse Y") * mouseSensitivity;
			verticalRot = Mathf.Clamp(verticalRot, -upDownRange, upDownRange);

			// Player's camera rotation
			cameraHolder.transform.localRotation = Quaternion.Euler(verticalRot, 0, 0);
		}

		// Gravity
		verticalVelocity -= gravity * Time.deltaTime;

		if (cc.isGrounded && Input.GetButtonDown("Jump"))
		{
			verticalVelocity = jumpForce;
		}

		// Movement
		float forwardSpeed = Input.GetAxis("Vertical") * movingSpeed;
		float sideSpeed = Input.GetAxis("Horizontal") * movingSpeed;

		Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
		speed = transform.rotation * speed;
		cc.Move(speed * Time.deltaTime);
		}
	}
}
