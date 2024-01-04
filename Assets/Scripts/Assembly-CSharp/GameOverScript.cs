using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
	private Image image;

	private float delay;

	public Sprite[] images = new Sprite[5];

	public Sprite rare;

	private float chance;

	private AudioSource audioDevice;

	private void Start()
	{
		image = GetComponent<Image>();
		audioDevice = GetComponent<AudioSource>();
		delay = 5f;
		chance = Random.Range(1f, 99f);
		if (chance < 98f)
		{
			image.sprite = images[Random.Range(0, 4)];
		}
		else
		{
			image.sprite = rare;
		}
	}

	private void Update()
	{
		delay -= 1f * Time.deltaTime;
		if (!(delay <= 0f))
		{
			return;
		}
		if (chance < 98f)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			SceneManager.LoadScene("MainMenu");
			return;
		}
		image.transform.localScale = new Vector3(5f, 5f, 1f);
		image.color = Color.red;
		if (!audioDevice.isPlaying)
		{
			audioDevice.Play();
		}
		if (delay <= -5f)
		{
			Application.Quit();
		}
	}
}
