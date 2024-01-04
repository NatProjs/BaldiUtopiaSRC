using UnityEngine;

public class AILocationSelectorScript : MonoBehaviour
{
	public Transform[] newLocation = new Transform[29];

	private int id;

	public void GetNewTarget()
	{
		id = Mathf.RoundToInt(Random.Range(0f, 28f));
		base.transform.position = newLocation[id].position;
	}

	public void GetNewTargetHallway()
	{
		id = Mathf.RoundToInt(Random.Range(0f, 15f));
		base.transform.position = newLocation[id].position;
	}
}
