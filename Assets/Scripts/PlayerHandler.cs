using UnityEngine;
using System.Collections;

public class PlayerHandler : MonoBehaviour {

	public Color[] playerCols = new Color[7];
	public string[] playerNames = new string[7];

	public int playerInt;

	void Start()
	{
		playerInt = PlayerPrefs.GetInt("PlayerCol");

		playerCols [0] = Color.white;
		playerCols [1] = Color.green;
		playerCols [2] = Color.blue + new Color(0, 0.4f, 0, 0);
		playerCols [3] = Color.yellow;
		playerCols [4] = Color.magenta;
		playerCols [5] = Color.red;
		playerCols [6] = Color.cyan;

		playerNames [0] = "Civilian";
		playerNames [1] = "UBX Talon";
		playerNames [2] = "Pinnacle";
		playerNames [3] = "Maeve";
		playerNames [4] = "Yoshimura 12";
		playerNames [5] = "Mod_Myriad";
		playerNames [6] = "Imperion";
	}
}
