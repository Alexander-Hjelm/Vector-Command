using UnityEngine;
using System.Collections;

public class PlayerHandler : MonoBehaviour {

	public Color[] playerCols = new Color[7];

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
	}
}
