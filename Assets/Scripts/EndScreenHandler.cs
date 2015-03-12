using UnityEngine;
using System.Collections;

public class EndScreenHandler : MonoBehaviour {

	PlayerHandler playerHandler;

	public GameObject winScreen;
	public GameObject loseScreen;

	GameObject[] bases;

	// Use this for initialization
	void Start () {
		playerHandler = GameObject.FindGameObjectWithTag ("PlayerHandler").GetComponent<PlayerHandler> ();

		winScreen.SetActive (false);
		loseScreen.SetActive (false);

		bases = GameObject.FindGameObjectsWithTag ("Base");

		InvokeRepeating ("EvalWin", 0.3f, 0.1f);
		InvokeRepeating ("EvalLose", 0.3f, 0.1f);
	}
	
	// Update is called once per frame
	void EvalWin () {
		foreach (GameObject b in bases)	//victory check
		{
			if (b.GetComponent<Base>().owner != 0 && b.GetComponent<Base>().owner != playerHandler.playerInt)
			{
				return;
			}
		}
		winScreen.SetActive(true);
	}

	void EvalLose ()
	{
		foreach (GameObject b in bases)	//defeat check
		{
			if (b.GetComponent<Base>().owner == playerHandler.playerInt)
			{
				return;
			}

		}
		loseScreen.SetActive(true);
	}
}
