using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Base : MonoBehaviour {

	public int NumberOfUnits = 0;
	public int MaxNumberOfUntis = 10;

	float spawnRate = 1f;	//For spawning units

	float hpRegenRate = 0.2f;

	//Scripts on this
	Owner ownerScript;
	SpriteRenderer spriteRenderer;
	BaseHp hpScript;

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
		hpScript = this.GetComponent<BaseHp> ();

		playerHandler = GameObject.FindGameObjectWithTag ("PlayerHandler");
		playerCols = playerHandler.GetComponent<PlayerHandler> ().playerCols;
	}

	void Start()
	{
		ChangeOwner (ownerScript.owner);
		InvokeRepeating ("AddUnit", 0f, spawnRate);	//Incr unit count over time, cancel invoke and invoke again to change
		InvokeRepeating ("RegenHp", 0f, hpRegenRate);	//Hp Regen
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

	void RegenHp()
	{
		if (hpScript.hp < hpScript.maxHp)
		{
			hpScript.modHp (1);
		}
	}

	void OnTriggerStay2D(Collider2D other)	//Check for incoming ships
	{
		if (other.gameObject.tag == "Ship"	&& !other.GetComponent<Ship>().inCombat)	//If is a ship not in combat
		{
			if (other.GetComponent<Owner>().owner == ownerScript.owner)	//same owner
			{
				AddUnit();
			}
			else 	//other owner
			{
				hpScript.modHp(-10);
				if (hpScript.hp <= 0)	//dead
				{
					ChangeOwner(other.GetComponent<Owner>().owner);		//change owner
					hpScript.hp = 1;
				}
			}

			other.gameObject.SetActive(false);
		}
	}
}
