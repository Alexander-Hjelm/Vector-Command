using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseCollisionController : MonoBehaviour {
	
	public Base baseScript;
	public BaseHp hpScript;
	
	public GameObject shipPool;
	List<GameObject> shipList = new List<GameObject>();
	
	public float repTime = 0.1f;	//InvokeRep rate
	public float threshDist = 1.4f;	//max dist for collision to occur

	List<GameObject> closeShips = new List<GameObject>();		//for storing ships in proximity

	void Awake () {
		baseScript = this.GetComponent<Base> ();
		hpScript = this.GetComponent<BaseHp> ();
		
		shipPool = GameObject.FindGameObjectWithTag ("ShipPool");
	}
	
	void OnEnable()
	{
		InvokeRepeating ("CheckCollision", 0f, repTime);
	}
	
	void TryForCloseShip (float thresh) {

		shipList = shipPool.GetComponent<ObjectPoolerScript> ().pooledObjects;
		
		if (shipList != null)
		{
			shipList = shipPool.GetComponent<ObjectPoolerScript> ().pooledObjects;
			foreach(GameObject ship in shipList)
			{
				//print ("Bummer");
				if(ship.activeSelf && (ship.transform.position - transform.position).magnitude < thresh)
				{
					if (!(ship.GetInstanceID() == this.gameObject.GetInstanceID()))	//Might be this
					{
						closeShips.Add(ship);
					}
				}
				
			}
		}
		return;
	}
	
	void CheckCollision()	//Main
	{
		TryForCloseShip(threshDist * transform.localScale.x);

		foreach (GameObject ship in closeShips)
		{
			if (ship != null)
			{
				if (!ship.GetComponent<Ship>().inCombat)	//If is a ship not in combat
				{
					if (ship.GetComponent<Ship>().owner == baseScript.owner)	//same owner
					{
						baseScript.AddUnit();
					}
					else 	//other owner
					{
						baseScript.AddUnit(-2);
						if (baseScript.NumberOfUnits <= 0)	//dead
						{
							baseScript.ChangeOwner(ship.GetComponent<Ship>().owner);		//change owner
							baseScript.NumberOfUnits = 1;
						}
					}
					
					ship.gameObject.SetActive(false);
				}
			}	
		}

		closeShips.RemoveRange (0, closeShips.Count);
	}
	
}
