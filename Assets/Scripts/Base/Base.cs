using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Base : MonoBehaviour {

	//public Vector3 worldPos;	//Where in the world is this base?
	public int owner;

	public int NumberOfUnits = 0;
	public int MaxNumberOfUntis = 10;

	public float spawnRate = 1f;	//For spawning units
	
	//Scripts on this
	SpriteRenderer spriteRenderer;
	ShipSpawn shipSpawn;
	PlayerInputHandler inputHandler;

	//UI refs
	Text numOfShipsText;

	//Other GOs
	GameObject playerHandler;
	Color[] playerCols;
	//public GameObject[] neighbours; //neighbouring bases
	public List<GameObject> neighbours = new List<GameObject>();

	public GameObject linePrefab;		//line prefab
	public GameObject shipPrefab;

	public GameObject mapMarker;	//this on minimap

	public CentralAI centralAI;	//For emptying requestlist on takeover

	void Awake()
	{
		//refs
		numOfShipsText = this.GetComponentInChildren<Text> ();
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
		shipSpawn = this.GetComponent<ShipSpawn> ();
		inputHandler = this.GetComponent<PlayerInputHandler> ();

		playerHandler = GameObject.FindGameObjectWithTag ("PlayerHandler");
		playerCols = playerHandler.GetComponent<PlayerHandler> ().playerCols;

		mapMarker = transform.FindChild ("MapMarker").gameObject;
	}

	void Start()
	{
		//ChangeOwner (owner);
		InvokeRepeating ("AddUnit", 0f, spawnRate);	//Incr unit count over time, cancel invoke and invoke again to change
		//InvokeRepeating ("RegenHp", 0f, hpRegenRate);	//Hp Regen
		//transform.position = new Vector3 (worldPos.x, worldPos.y, 0);

		//Draw lines to neighbours
		foreach (GameObject obj in neighbours)
		{
			if (obj.tag == "Base")
			{
				GameObject line = GameObject.Instantiate(linePrefab) as GameObject;
				LineScript linescript = line.GetComponent<LineScript>();

				//linescript.startPoint = this.transform.position;
				//linescript.endPoint = obj.transform.position;

				linescript.thisBase = this.gameObject;
				linescript.targetBase = obj;

				//linescript.startCol = playerCols [ownerScript.owner];
				//linescript.endCol = playerCols[obj.GetComponent<Owner>().owner];
			}
		}
	}

	void Update()
	{
		numOfShipsText.text = NumberOfUnits.ToString();

		if (Input.GetKeyDown(KeyCode.A))
		{
			shipSpawn.baseScript = this;
			shipSpawn.targetPos = new Vector3(0,0,0);
			shipSpawn.SpawnUnit();
		}
	}

	public void AddUnit()
	{
		if (NumberOfUnits < MaxNumberOfUntis && owner != 0)
		{
			NumberOfUnits++;
		}
	}

	public void AddUnit(int num)
	{
		if (NumberOfUnits < MaxNumberOfUntis || num <= 0)
		{
			NumberOfUnits += num;
		}
	}

	public void ChangeOwner(int newOwner)
	{
		shipSpawn.StopSpawn ();
		this.owner = newOwner;
		spriteRenderer.color = playerCols [newOwner];	//Change color of sprite
		numOfShipsText.color = playerCols [newOwner];	//Change color of GUI text to match that of owner
		mapMarker.GetComponent<SpriteRenderer>().color = playerCols[newOwner];
		inputHandler.spawning = false;

		inputHandler.Activate (false);

		//AI
		centralAI = GameObject.FindGameObjectWithTag("Central AI").transform.FindChild("AI_" + owner.ToString()).GetComponent<CentralAI>();
		//centralAI.EmptyRequestList (this.gameObject);
	}

	/*
	void RegenHp()
	{
		if (hpScript.hp < hpScript.maxHp)
		{
			hpScript.modHp (1);
		}
	}
	*/
}