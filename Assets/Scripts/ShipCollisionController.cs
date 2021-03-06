﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipCollisionController : MonoBehaviour {

	Ship shipScript;

	public GameObject shipPool;
	List<GameObject> shipList;

	public float repTime = 0.1f;	//InvokeRep rate
	public float threshDist = 1.5f;	//max dist for collision to occur

	void Awake () {
		shipScript = this.GetComponent<Ship> ();

		shipPool = GameObject.FindGameObjectWithTag ("ShipPool");
	}

	void OnEnable()
	{
		InvokeRepeating ("CheckCollision", 0f, repTime);
	}

	GameObject TryForCloseShip (float thresh) {

		shipList = shipPool.GetComponent<ObjectPoolerScript> ().pooledObjects;

		if (shipList != null && this.enabled)
		{
			shipList = shipPool.GetComponent<ObjectPoolerScript> ().pooledObjects;
			foreach(GameObject ship in shipList)
			{
				//print ("Bummer");
				if(ship.activeSelf
				   && ship.gameObject.GetComponent<Ship>().owner != shipScript.owner
				   &&(ship.transform.position - transform.position).magnitude < thresh)
				{
					if (!(ship.GetInstanceID() == this.gameObject.GetInstanceID()))	//Might be this
					{
						//print ("found Ship in range");
						return ship;
					}
				}
		
			}
		}
		return null;
	}

	void CheckCollision()	//Main
	{
		GameObject other = TryForCloseShip(threshDist);
		if (other != null)
		{
			if (other.gameObject.GetComponent<Ship>().owner != shipScript.owner)	//If other ship is not on our team
			{
				if(!shipScript.inCombat)	//if this ship and other ship are not in combat			&& !other.gameObject.GetComponent<Ship>().inCombat
				{//!other.gameObject.GetComponent<Ship>().hunted
					shipScript.inCombat = true;	//enter combat
					shipScript.combatTarget = other.gameObject;
				}
			}
		}
	}

}
