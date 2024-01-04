using UnityEngine;
using UnityEngine.UI;

public class ExitButtonScript : MonoBehaviour
{
	private Button button;

	private void Start()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(ExitGame);
	}

	private void ExitGame()
	{
		Application.Quit();
	}
}
