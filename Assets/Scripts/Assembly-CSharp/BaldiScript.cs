using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BaldiScript : MonoBehaviour
{
	public bool db;

	public float baseTime;

	public float speed;

	public float timeToMove;

	public float baldiAnger;

	public float angerRate;

	private float moveFrames;

	private float currentPriority;

	public Transform player;

	public Transform wanderTarget;

	public AILocationSelectorScript wanderer;

	private AudioSource baldiAudio;

	public AudioClip slap;

	public AudioClip[] speech = new AudioClip[3];

	public Animator baldiAnimator;

	public float coolDown;

	private Vector3 previous;

	private NavMeshAgent agent;

	private float yourTouchDistanceThreshold = 1.5f;

	private void Start()
	{
		baldiAudio = GetComponent<AudioSource>();
		agent = GetComponent<NavMeshAgent>();
		timeToMove = baseTime;
		Wander();
	}

	private void Update()
	{
		if (timeToMove > 0f)
		{
			timeToMove -= 1f * Time.deltaTime;
		}
		else
		{
			Move();
		}
		if (coolDown > 0f)
		{
			coolDown -= 1f * Time.deltaTime;
		}
	}

	private void FixedUpdate()
	{
		if (moveFrames > 0f)
		{
			moveFrames -= 1f;
			agent.speed = speed;
		}
		else
		{
			agent.speed = 0f;
		}
		Vector3 direction = player.position - base.transform.position;
		RaycastHit hitInfo;
		if (Physics.Raycast(base.transform.position, direction, out hitInfo, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & (hitInfo.transform.tag == "Player"))
		{
			db = true;
			TargetPlayer();
		}
		else
		{
			db = false;
		}

        if (db)
        {
            if (Vector3.Distance(transform.position, player.position) < yourTouchDistanceThreshold)
            {
                SceneManager.LoadScene("GameOver");
            }
        }		
	}

	private void Wander()
	{
		wanderer.GetNewTarget();
		agent.SetDestination(wanderTarget.position);
		coolDown = 1f;
		currentPriority = 0f;
	}

	private void TargetPlayer()
	{
		agent.SetDestination(player.position);
		coolDown = 1f;
		currentPriority = 0f;
	}

	private void Move()
	{
		if ((base.transform.position == previous) & (coolDown < 0f))
		{
			Wander();
		}
		moveFrames = 10f;
		timeToMove = baseTime - baldiAnger;
		previous = base.transform.position;
		baldiAudio.PlayOneShot(slap);
		baldiAnimator.SetTrigger("slap");
	}

	public void GetAngry(float value)
	{
		baldiAnger += angerRate * value;
	}

	public void Hear(Vector3 soundLocation, float priority)
	{
		if (priority >= currentPriority)
		{
			agent.SetDestination(soundLocation);
			currentPriority = priority;
		}
	}
}
