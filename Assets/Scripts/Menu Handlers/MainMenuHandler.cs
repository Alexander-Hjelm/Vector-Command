using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;	//For dicts
using System.Linq;	//For getting toggle(FirstOrDefault)

public class MainMenuHandler : MonoBehaviour {

	public GameObject initMenu;
	public GameObject charMenu;
	public GameObject lvlMenu;
	public GameObject optMenu;

	public GameObject nameText;
	public GameObject classToggleGroup;

	public GameObject descFieldText;

	public GameObject diffButton;

	public void GotoCharCreation()
	{
		initMenu.SetActive (false);
		charMenu.SetActive (true);
	}

	public void GotoOptions()
	{
		initMenu.SetActive (false);
		optMenu.SetActive (true);
	}

	public void GotoMainMenu()
	{
		charMenu.SetActive (false);
		optMenu.SetActive (false);
		initMenu.SetActive (true);
	}

	public void Quit()
	{
		Application.Quit ();
	}

	public void StartGame()
	{
		PlayerPrefs.SetString ("PlayerName", nameText.GetComponent<Text> ().text);

		int col = int.Parse(classToggleGroup.GetComponent<ToggleGroup> ().ActiveToggles ().FirstOrDefault ().name.Split (' ') [1]);

		PlayerPrefs.SetInt ("PlayerCol", col);
		print(PlayerPrefs.GetInt("PlayerCol"));

		charMenu.SetActive (false);
		lvlMenu.SetActive (true);
		PlayerPrefs.SetInt("diff", diffButton.GetComponent<DifficultySelect>().currentDiff);
	}
}
