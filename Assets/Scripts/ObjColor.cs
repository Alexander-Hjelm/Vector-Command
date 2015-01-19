using UnityEngine;
using System.Collections;

public class ObjColor: MonoBehaviour {

	GameObject playerHandler;
	Color[] playerCols;

	SpriteRenderer spriteRenderer;
	Owner ownerScript;

	// Use this for initialization
	void Awake () {
		playerHandler = GameObject.FindGameObjectWithTag ("PlayerHandler");
		playerCols = playerHandler.GetComponent<PlayerHandler> ().playerCols;

		spriteRenderer = this.GetComponent<SpriteRenderer> ();
		ownerScript = this.GetComponent<Owner> ();
	}

	void Start()
	{
		SetColor (ownerScript.owner);
	}

	void OnEnable()
	{
		SetColor (ownerScript.owner);
	}

	// Update is called once per frame
	public void SetColor (int owner) {
		spriteRenderer.color = playerCols [owner];
	}
}
