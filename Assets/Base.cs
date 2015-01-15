using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Base : MonoBehaviour {

	public int NumberOfUnits = 0;
	public int MaxNumberOfUntis = 10;

	float repeatingTime = 1f;

	//Scripts on this
	Owner ownerScript;
	SpriteRenderer spriteRenderer;

	//UI refs
	Text numOfShipsText;

	//Other GOs
	GameObject playerHandler;
	Color[] playerCols;

	void Awake()
	{
		//refs
		numOfShipsText = this.GetComponentInChildren<Text> ();
		ownerScript = this.GetComponent<Owner> ();
		spriteRenderer = this.GetComponent<SpriteRenderer> ();

		playerHandler = GameObject.FindGameObjectWithTag ("PlayerHandler");
		playerCols = playerHandler.GetComponent<PlayerHandler> ().playerCols;
	}

	void Start()
	{
		ChangeOwner (ownerScript.owner);
		InvokeRepeating ("AddUnit", 0f, repeatingTime);	//Incr unit count over time, cancel invoke and invoke again to change
	}

	void Update()
	{
		numOfShipsText.text = NumberOfUnits.ToString();
	}

	void AddUnit()
	{
		if (NumberOfUnits < MaxNumberOfUntis)
		{
			NumberOfUnits++;
		}
	}

	void ChangeOwner(int owner)
	{
		this.ownerScript.owner = owner;
		spriteRenderer.color = playerCols [owner];	//Change color of sprite
		numOfShipsText.color = playerCols [owner];	//Change color of GUI text to match that of owner
	}
}
