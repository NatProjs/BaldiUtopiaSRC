using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
	private Button button;

	private void Start()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(StartGame);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	private void StartGame()
	{
		SceneManager.LoadScene("School");
	}
}
