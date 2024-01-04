using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTriggerScript : MonoBehaviour
{
	public GameControllerScript gc;

	private void OnTriggerEnter(Collider other)
	{
		if ((gc.notebooks >= 7) & (other.tag == "Player"))
		{
			SceneManager.LoadScene("Results");
		}
	}
}
