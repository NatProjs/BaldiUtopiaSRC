using UnityEngine;

public class PickupScript : MonoBehaviour
{
	public GameControllerScript gc;

	public Transform player;

	private void Start()
	{
	}

	private void Update()
	{
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo))
		{
			if ((hitInfo.transform.name == "Pickup_EnergyFlavoredZestyBar") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(1);
			}
			else if ((hitInfo.transform.name == "Pickup_YellowDoorLock") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(2);
			}
			else if ((hitInfo.transform.name == "Pickup_Key") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(3);
			}
			else if ((hitInfo.transform.name == "Pickup_BSODA") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(4);
			}
			else if ((hitInfo.transform.name == "Pickup_Quarter") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(5);
			}
			else if ((hitInfo.transform.name == "Pickup_Tape") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(6);
			}
		}
	}
}
