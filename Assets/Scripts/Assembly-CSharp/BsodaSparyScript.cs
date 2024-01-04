using UnityEngine;
using UnityEngine.AI;

public class BsodaSparyScript : MonoBehaviour
{
	public float speed;

	private float lifeSpan;

	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.velocity = base.transform.forward * speed;
		lifeSpan = 30f;
	}

	private void Update()
	{
		rb.velocity = base.transform.forward * speed;
		lifeSpan -= Time.deltaTime;
		if (lifeSpan < 0f)
		{
			Object.Destroy(base.gameObject, 0f);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "NPC")
		{
			NavMeshAgent component = other.GetComponent<NavMeshAgent>();
			component.velocity = rb.velocity + component.velocity * 0.1f;
		}
	}
}
