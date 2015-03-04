using UnityEngine;
using System.Collections;

public class PauseMenuHandler : MonoBehaviour {

	public GameObject pauseMenu;

	public bool paused = false; //For other classes to know if game is paused

	void Start()
	{
		pauseMenu.SetActive (false);	//Pause menu not visible at start
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))	//Pause on esc
		{
			paused = true;
			Time.timeScale = 0;
			pauseMenu.SetActive (true);
		}
	}

	public void Continue()
	{
		paused = false;
		pauseMenu.SetActive (false);
		Time.timeScale = 1;
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
