using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selection : MonoBehaviour 
{
    public void Original()
    {
		SceneManager.LoadScene("School");
	}

    public void NoMaths()
    {
		SceneManager.LoadScene("SchoolNoMaths");
	}
}