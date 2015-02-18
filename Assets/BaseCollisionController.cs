using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseCollisionController : MonoBehaviour {
	
	public Base baseScript;
	public BaseHp hpScript;
	
	public GameObject shipPool;
	List<GameObject> shipList;
	
	public float repTime = 0.1f;	//InvokeRep rate
	public float threshDist = 1.4f;	//max dist for collision to occur
	
	void Awake () {
		baseScript = this.GetComponent<Base> ();
		hpScript = this.GetComponent<BaseHp> ();
		
		shipPool = GameObject.FindGameObjectWithTag ("ShipPool");
	}
	
	void OnEnable()
	{
		InvokeRepeating ("CheckCollision", 0f, repTime);
	}
	
	GameObject TryForCloseShip (float thresh) {
		
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
						return ship;
						print ("found Ship in range");
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
			if (!other.GetComponent<Ship>().inCombat)	//If is a ship not in combat
			{
				if (other.GetComponent<Ship>().owner == baseScript.owner)	//same owner
				{
					baseScript.AddUnit();
				}
				else 	//other owner
				{
					hpScript.modHp(-10);
					if (hpScript.hp <= 0)	//dead
					{
						baseScript.ChangeOwner(other.GetComponent<Ship>().owner);		//change owner
						hpScript.hp = 1;
					}
				}
				
				other.gameObject.SetActive(false);
			}


		}
	}
	
}
