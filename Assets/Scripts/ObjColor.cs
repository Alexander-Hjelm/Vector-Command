using UnityEngine;
using System.Collections;

public class ObjColor: MonoBehaviour {

	GameObject playerHandler;
	Color[] playerCols;

	SpriteRenderer spriteRenderer;
	Owner ownerScript;

	// Use this for initialization
	void Start () {
		playerHandler = GameObject.FindGameObjectWithTag ("PlayerHandler");
		playerCols = playerHandler.GetComponent<PlayerHandler> ().playerCols;

		spriteRenderer = this.GetComponent<SpriteRenderer> ();
		ownerScript = this.GetComponent<Owner> ();
		spriteRenderer.color = playerCols [ownerScript.owner];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
