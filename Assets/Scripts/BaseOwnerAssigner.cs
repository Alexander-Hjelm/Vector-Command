using UnityEngine;
using System.Collections;

public class BaseOwnerAssigner : MonoBehaviour {

	PlayerHandler playerHandler;

	public GameObject playerBase;
	public GameObject[] initialBases;

	int[] taken = {0,0,0,0,0,0,0,0,0,0,0,0,0,0};

	int i = 0;

	// Use this for initialization
	void Start () {
		Invoke ("Setup", 0.01f);
	}
	
	// Update is called once per frame
	void Setup () {
		playerHandler = GameObject.FindGameObjectWithTag ("PlayerHandler").GetComponent<PlayerHandler>();
		
		playerBase.GetComponent<Base>().ChangeOwner(playerHandler.playerInt);
		foreach (GameObject b in initialBases)
		{
			do
			{
				i = Random.Range(1, playerHandler.playerCols.Length);
			} while(i == playerHandler.playerInt || taken[i] == 1);
			b.GetComponent<Base>().ChangeOwner(i);
			taken[i] = 1;
		}
	}
}
