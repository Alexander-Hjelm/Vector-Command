using UnityEngine;
using System.Collections;
using System.Collections.Generic;	//Access to lists

public class ObjectPoolerScript : MonoBehaviour {

	//generic pooling script. refer to ShootBulletScript class for details

	public static ObjectPoolerScript current;
	public GameObject pooledObject;				// Create 20 bullets at game start and store to a list
												//Value should reflect max # of bullets allowed at once
	public int pooledAmount = 20;
	public bool willGrow = true; 	//dynamicly sized pool or not?

	List<GameObject> pooledObjects;

	void Awake()
	{
		current = this; 	//setting up static reference
	}

	void Start () {
		pooledObjects = new List<GameObject>();
		for(int i = 0; i < pooledAmount; i++)
		{
			GameObject obj = (GameObject)Instantiate(pooledObject);
			obj.SetActive(false);
			pooledObjects.Add (obj);
		}
	}
	
	public GameObject GetAvailablePooledObject ()
	{
		for(int i = 0; i < pooledObjects.Count; i++)
		{
			if(!pooledObjects[i].activeInHierarchy)
			{
				return pooledObjects[i];
			}
		}

		if(willGrow)	//if no available instaqnce was found
		{
			GameObject obj = (GameObject)Instantiate(pooledObject);
			pooledObjects.Add(obj);
			return obj;
		}

		return null;	// No available Pooled object
	}
}


//POOLING COULD WELL BE USED FOR PEDESTRIANS, ENEMIES, AMMO, BUILDINGS, ETC

//eg WHEN THE FIRST CHARACTER FROM A SINGLE GROUP IS INSTANTIATED, CREATE A POOL FOR THE SAME GROUP.
	//VERY DECENTRALIZED, MUCH DATA-DRIVEN