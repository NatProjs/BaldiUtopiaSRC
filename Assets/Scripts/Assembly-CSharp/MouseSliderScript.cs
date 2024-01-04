using UnityEngine;
using UnityEngine.UI;

public class MouseSliderScript : MonoBehaviour
{
	public Slider slider;

	private void Start()
	{
		slider = GetComponent<Slider>();
	}

	private void Update()
	{
		PlayerPrefs.SetFloat("MouseSensitivity", slider.value);
	}
}
