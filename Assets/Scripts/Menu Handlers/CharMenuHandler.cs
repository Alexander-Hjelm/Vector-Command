using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharMenuHandler : MonoBehaviour {

	public GameObject charMenu;
	bool menuOpen = false;

	//Input fields for writing out char name and class
	public GameObject nameText;
	public GameObject classText;

	void Start()
	{
		charMenu.SetActive (false);	//Pause menu not visible at start
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))	//Pause on esc
		{
			if (!menuOpen)
			{
				PopulateCharMenu();
			}

			charMenu.SetActive(!menuOpen);		//Disable if enabled, and vice versa
			menuOpen = !menuOpen;
		}
	}

	void PopulateCharMenu()
	{
		print (PlayerPrefs.GetString ("PlayerName") + " " + PlayerPrefs.GetString ("PlayerClass"));
		nameText.GetComponent<Text> ().text = PlayerPrefs.GetString ("PlayerName");
		classText.GetComponent<Text> ().text = PlayerPrefs.GetString ("PlayerClass");

	}
}
