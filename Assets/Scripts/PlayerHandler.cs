using UnityEngine;
using System.Collections;

public class PlayerHandler : MonoBehaviour {

	public Color[] playerCols = new Color[4];

	void Awake()
	{
		playerCols [0] = Color.white;
		playerCols [1] = Color.green;
		playerCols [2] = Color.blue + new Color(0, 0.4f, 0, 0);
		playerCols [3] = Color.yellow;
	}
}
