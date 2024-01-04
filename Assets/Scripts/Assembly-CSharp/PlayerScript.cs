using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
	public GameControllerScript gc;

	public BaldiScript baldi;

	public DoorScript door;

	public PlaytimeScript playtime;

	public bool gameOver;

	public bool jumpRope;

	public bool sweeping;

	public float sweepingFailsave;

	public Vector3 frozenPosition;

	public float mouseSensitivity;

	public float walkSpeed;

	public float runSpeed;

	public float slowSpeed;

	public float maxStamina;

	public float staminaRate;

	public float guilt;

	public float initGuilt;

	private float moveX;

	private float moveZ;

	private float playerSpeed;

	public float stamina;

	public Rigidbody rb;

	public NavMeshAgent gottaSweep;

	public Slider staminaBar;

	public float db;

	public string guiltType;

	public GameObject jumpRopeScreen;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		stamina = maxStamina;
		mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
	}

	private void Update()
	{
		StaminaCheck();
		GuiltCheck();
		if (moveZ != 0f || moveX != 0f)
		{
			gc.LockMouse();
		}
		if (jumpRope & ((base.transform.position - frozenPosition).magnitude >= 1f))
		{
			DeactivateJumpRope();
		}
		if (sweepingFailsave > 0f)
		{
			sweepingFailsave -= Time.deltaTime;
		}
		else
		{
			sweeping = false;
		}
	}

	private void FixedUpdate()
	{
		PlayerMove();
		if (!jumpRope & !sweeping)
		{
			rb.velocity = new Vector3(moveX, 0f, moveZ);
		}
		else if (sweeping)
		{
			rb.velocity = gottaSweep.velocity + new Vector3(moveX, 0f, moveZ) * 0.2f;
		}
		else
		{
			rb.velocity = new Vector3(0f, 0f, 0f);
		}
	}

	private void PlayerMove()
	{
		moveX = 0f;
		moveZ = 0f;
		if (gc.mouseLocked)
		{
			base.transform.Rotate(0f, Input.GetAxis("Mouse X") * mouseSensitivity, 0f);
		}
		float y = base.transform.eulerAngles.y;
		db = Input.GetAxisRaw("Forward");
		if (stamina > 0f)
		{
			if (Input.GetAxisRaw("Run") > 0f)
			{
				playerSpeed = runSpeed;
				if (rb.velocity.magnitude > 0f)
				{
					ResetGuilt("running", 0.2f);
				}
			}
			else
			{
				playerSpeed = walkSpeed;
			}
		}
		else
		{
			playerSpeed = slowSpeed;
		}
		if (Input.GetAxis("Forward") > 0f)
		{
			moveZ += Mathf.Cos(y * ((float)Math.PI / 180f)) * playerSpeed;
			moveX += Mathf.Sin(y * ((float)Math.PI / 180f)) * playerSpeed;
		}
		else if (Input.GetAxis("Forward") < 0f)
		{
			moveZ += 0f - Mathf.Cos(y * ((float)Math.PI / 180f)) * playerSpeed;
			moveX += 0f - Mathf.Sin(y * ((float)Math.PI / 180f)) * playerSpeed;
		}
		if (Input.GetAxis("Strafe") > 0f)
		{
			moveZ += Mathf.Cos((y + 90f) * ((float)Math.PI / 180f)) * playerSpeed;
			moveX += Mathf.Sin((y + 90f) * ((float)Math.PI / 180f)) * playerSpeed;
		}
		else if (Input.GetAxis("Strafe") < 0f)
		{
			moveZ += 0f - Mathf.Cos((y + 90f) * ((float)Math.PI / 180f)) * playerSpeed;
			moveX += 0f - Mathf.Sin((y + 90f) * ((float)Math.PI / 180f)) * playerSpeed;
		}
	}

	private void StaminaCheck()
	{
		if (moveZ != 0f || moveX != 0f)
		{
			if ((Input.GetAxisRaw("Run") > 0f) & (stamina > 0f))
			{
				stamina -= staminaRate * Time.deltaTime;
			}
			if ((stamina < 0f) & (stamina > -25f))
			{
				stamina = -25f;
			}
		}
		else if (stamina < maxStamina)
		{
			stamina += staminaRate * Time.deltaTime;
		}
		staminaBar.value = stamina / maxStamina * 100f;
	}

	private void OnTriggerEnter(Collider other)
	{
		if ((other.transform.name == "Baldi") & !gc.debugMode)
		{
			gameOver = true;
		}
		else if ((other.transform.name == "Playtime") & !jumpRope & (playtime.playCool <= 0f))
		{
			ActivateJumpRope();
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.transform.name == "Gotta Sweep")
		{
			sweeping = true;
			sweepingFailsave = 1f;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.transform.name == "Office Trigger")
		{
			ResetGuilt("escape", door.lockTime);
		}
		else if (other.transform.name == "Gotta Sweep")
		{
			sweeping = false;
		}
	}

	public void ResetGuilt(string type, float amount)
	{
		if (amount >= guilt)
		{
			guilt = amount;
			guiltType = type;
		}
	}

	private void GuiltCheck()
	{
		if (guilt > 0f)
		{
			guilt -= Time.deltaTime;
		}
	}

	public void ActivateJumpRope()
	{
		baldi.Hear(base.transform.position, 2f);
		jumpRopeScreen.SetActive(true);
		jumpRope = true;
		frozenPosition = base.transform.position;
	}

	public void DeactivateJumpRope()
	{
		jumpRopeScreen.SetActive(false);
		jumpRope = false;
	}
}
