using UnityEngine;
using System.Collections;

public class ShipColor : MonoBehaviour {
	
	GameObject playerHandler;
	Color[] playerCols;
	Ship shipScript;
	SpriteRenderer spriteRenderer;
	
	// Use this for initialization
	void Awake () {
		playerHandler = GameObject.FindGameObjectWithTag ("PlayerHandler");
		playerCols = playerHandler.GetComponent<PlayerHandler> ().playerCols;
		
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
		shipScript = this.GetComponent<Ship> ();
	}
	
	void Start()
	{
		SetColor (shipScript.owner);
	}
	
	void OnEnable()
	{
		SetColor (shipScript.owner);
	}
	
	// Update is called once per frame
	public void SetColor (int owner) {
		spriteRenderer.color = playerCols [owner];
	}
}