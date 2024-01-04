using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCode : MonoBehaviour 
{
    public void Transition()
    {
		SceneManager.LoadScene("Selection");
	}
}