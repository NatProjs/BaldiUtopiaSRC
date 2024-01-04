using UnityEngine;
using UnityEngine.AI;

public class PlaytimeScript : MonoBehaviour
{
	public bool db;

	public int audVal;

	public Transform player;

	public PlayerScript ps;

	public Transform wanderTarget;

	public AILocationSelectorScript wanderer;

	public float coolDown;

	public float playCool;

	public bool playerSpotted;

	public bool jumpRopeStarted;

	private NavMeshAgent agent;

	public AudioClip[] aud_Numbers = new AudioClip[10];

	public AudioClip[] aud_Random = new AudioClip[2];

	public AudioClip aud_Instrcutions;

	public AudioClip aud_Oops;

	public AudioClip aud_LetsPlay;

	public AudioClip aud_Congrats;

	public AudioSource audioDevice;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		audioDevice = GetComponent<AudioSource>();
		Wander();
	}

	private void Update()
	{
		if (coolDown > 0f)
		{
			coolDown -= 1f * Time.deltaTime;
		}
		if (playCool > 0f)
		{
			playCool -= Time.deltaTime;
		}
	}

	private void FixedUpdate()
	{
		if (!ps.jumpRope)
		{
			Vector3 direction = player.position - base.transform.position;
			RaycastHit hitInfo;
			if (Physics.Raycast(base.transform.position, direction, out hitInfo, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & (hitInfo.transform.tag == "Player") & ((base.transform.position - player.position).magnitude <= 80f) & (playCool <= 0f))
			{
				db = true;
				TargetPlayer();
			}
			else
			{
				db = false;
				if ((agent.velocity.magnitude <= 1f) & (coolDown <= 0f))
				{
					Wander();
				}
			}
			jumpRopeStarted = false;
		}
		else
		{
			if (!jumpRopeStarted)
			{
				agent.Warp(base.transform.position - base.transform.forward * 10f);
			}
			jumpRopeStarted = true;
			agent.speed = 0f;
			playCool = 15f;
		}
	}

	private void Wander()
	{
		wanderer.GetNewTarget();
		agent.SetDestination(wanderTarget.position);
		agent.speed = 15f;
		playerSpotted = false;
		audVal = Mathf.RoundToInt(Random.Range(0f, 1f));
		audioDevice.PlayOneShot(aud_Random[audVal]);
		coolDown = 1f;
	}

	private void TargetPlayer()
	{
		agent.SetDestination(player.position);
		agent.speed = 25f;
		coolDown = 1f;
		if (!playerSpotted)
		{
			playerSpotted = true;
			audioDevice.PlayOneShot(aud_LetsPlay);
		}
	}
}
