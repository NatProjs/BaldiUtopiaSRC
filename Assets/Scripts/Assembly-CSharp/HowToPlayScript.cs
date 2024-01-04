using UnityEngine;
using UnityEngine.UI;

public class HowToPlayScript : MonoBehaviour
{
	private Button button;

	public GameObject screen;

	private void Start()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(OpenScreen);
	}

	private void OpenScreen()
	{
		screen.SetActive(true);
	}
}
