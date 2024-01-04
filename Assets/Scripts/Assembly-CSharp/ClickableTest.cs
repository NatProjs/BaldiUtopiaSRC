using UnityEngine;

public class ClickableTest : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo) && hitInfo.transform.name == "MathNotebook")
			{
				base.gameObject.SetActive(false);
			}
		}
	}
}
