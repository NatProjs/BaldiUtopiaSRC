using UnityEngine;

public class EntranceScript : MonoBehaviour
{
	public GameControllerScript gc;

	public void Lower()
	{
		base.transform.position = base.transform.position - new Vector3(0f, 10f, 0f);
	}

	public void Raise()
	{
		base.transform.position = base.transform.position + new Vector3(0f, 10f, 0f);
	}
}
