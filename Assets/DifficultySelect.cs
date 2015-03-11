using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DifficultySelect : MonoBehaviour {

	public int currentDiff = 0;

	string[] diffNames = {"Easy", "Normal", "Hard", "Brutal"};
	Text diffText;

	void Awake () {
		//refs
		diffText = this.GetComponentInChildren<Text> ();
	}

	public void ChangeDiff()
	{
		currentDiff = (currentDiff + 1) % 4;
		diffText.text = diffNames [currentDiff];
	}
}
