using UnityEngine;
using System.Collections;

public class PlayerHandler : MonoBehaviour {

	public Color[] playerCols = new Color[7];
	public string[] playerNames = new string[7];

	public int playerInt;

	void Awake()
	{
		playerInt = 1;

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

	void OnEnable ()
	{
		SetupPlayerName ();
	}

	void SetupPlayerName()
	{
		switch (PlayerPrefs.GetString("PlayerCol"))
		{
			case "Green":
				playerNames[1] = PlayerPrefs.GetString("PlayerName");
				playerInt = 1;
				break;
			case "Blue":
				playerNames[2] = PlayerPrefs.GetString("PlayerName");
				playerInt = 2;
				break;
			case "Yellow":
				playerNames[3] = PlayerPrefs.GetString("PlayerName");
				playerInt = 3;
				break;
			case "Magenta":
				playerNames[4] = PlayerPrefs.GetString("PlayerName");
				playerInt = 4;
				break;
			case "Red":
				playerNames[5] = PlayerPrefs.GetString("PlayerName");
				playerInt = 5;
				break;
			case "Cyan":
				playerNames[6] = PlayerPrefs.GetString("PlayerName");
				playerInt = 6;
				break;
		}

	}
}
