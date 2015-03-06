﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Base : MonoBehaviour {

	public Vector3 worldPos;	//Where in the world is this base?
	public int owner;

	public int NumberOfUnits = 0;
	public int MaxNumberOfUntis = 10;

	float spawnRate = 1f;	//For spawning units

	float hpRegenRate = 0.2f;
	
	//Scripts on this
	SpriteRenderer spriteRenderer;
	BaseHp hpScript;
	ShipSpawn shipSpawn;

	//UI refs
	Text numOfShipsText;

	//Other GOs
	GameObject playerHandler;
	Color[] playerCols;
	//public GameObject[] neighbours; //neighbouring bases
	public List<GameObject> neighbours = new List<GameObject>();

	public GameObject linePrefab;		//line prefab
	public GameObject shipPrefab;

	void Awake()
	{
		//refs
		numOfShipsText = this.GetComponentInChildren<Text> ();
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
		hpScript = this.GetComponent<BaseHp> ();
		shipSpawn = this.GetComponent<ShipSpawn> ();

		playerHandler = GameObject.FindGameObjectWithTag ("PlayerHandler");
		playerCols = playerHandler.GetComponent<PlayerHandler> ().playerCols;
	}

	void Start()
	{
		ChangeOwner (owner);
		InvokeRepeating ("AddUnit", 0f, spawnRate);	//Incr unit count over time, cancel invoke and invoke again to change
		InvokeRepeating ("RegenHp", 0f, hpRegenRate);	//Hp Regen
		transform.position = new Vector3 (worldPos.x, worldPos.y, 0);

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

	public void ChangeOwner(int newOwner)
	{
		this.owner = newOwner;
		spriteRenderer.color = playerCols [newOwner];	//Change color of sprite
		numOfShipsText.color = playerCols [newOwner];	//Change color of GUI text to match that of owner
	}

	void RegenHp()
	{
		if (hpScript.hp < hpScript.maxHp)
		{
			hpScript.modHp (1);
		}
	}
}