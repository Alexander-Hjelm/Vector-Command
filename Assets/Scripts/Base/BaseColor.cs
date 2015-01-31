using UnityEngine;
using System.Collections;

public class BaseColor: MonoBehaviour {

	GameObject playerHandler;
	Color[] playerCols;
	Base baseScript;
	SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Awake () {
		playerHandler = GameObject.FindGameObjectWithTag ("PlayerHandler");
		playerCols = playerHandler.GetComponent<PlayerHandler> ().playerCols;

		spriteRenderer = this.GetComponent<SpriteRenderer> ();
		baseScript = this.GetComponent<Base> ();
	}

	void Start()
	{
		SetColor (baseScript.owner);
	}

	void OnEnable()
	{
		SetColor (baseScript.owner);
	}

	// Update is called once per frame
	public void SetColor (int owner) {
		spriteRenderer.color = playerCols [owner];
	}
}
