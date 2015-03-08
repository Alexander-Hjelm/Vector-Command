using UnityEngine;
using System.Collections;

public class ShipColor : MonoBehaviour {
	
	GameObject playerHandler;
	Color[] playerCols;
	Ship shipScript;
	SpriteRenderer spriteRenderer;
	public GameObject mapMarker;	//this on minimap

	// Use this for initialization
	void Awake () {
		playerHandler = GameObject.FindGameObjectWithTag ("PlayerHandler");
		playerCols = playerHandler.GetComponent<PlayerHandler> ().playerCols;
		mapMarker = transform.FindChild ("MapMarker").gameObject;
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
		mapMarker.GetComponent<SpriteRenderer>().color = playerCols[owner];
	}
}