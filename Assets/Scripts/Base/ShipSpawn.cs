using UnityEngine;
using System.Collections;

public class ShipSpawn : MonoBehaviour {

	ObjectPoolerScript shipPool;

	//pararms for SpawUnit
	public Base baseScript;
	public Vector3 targetPos;
	public bool shouldBeSpawning = false;

	public float gameFreq = 1f;	//How fast we should spawn (invoke rep)	

	void Awake()
	{
		shipPool = GameObject.FindGameObjectWithTag ("ShipPool").GetComponent<ObjectPoolerScript> ();
	}

	void Update()
	{
		if(shouldBeSpawning)
		{
			InvokeRepeating("SpawnUnit", 0.1f, gameFreq);
			shouldBeSpawning = false;
		}
	}

	public void StopSpawn()
	{
		CancelInvoke ("SpawnUnit");
	}

	public void initiateAttack(Base bas, Vector3 pos)
	{
		baseScript = bas;
		targetPos = pos;
		shouldBeSpawning = true;
	}

	public void SpawnUnit()
	{
		if(baseScript.NumberOfUnits > 0)
		{
			Vector3 pos = CalcRandSpawnPos(targetPos);
			Quaternion rot = Quaternion.LookRotation (transform.position - pos, Vector3.forward);
			rot.x = 0;
			rot.y = 0;


			//spawns single unit w/ same owner as this base
			GameObject unit = shipPool.GetAvailablePooledObject();
			
			unit.transform.position = pos;
			unit.transform.rotation = rot;
			unit.GetComponent<Ship>().owner = baseScript.owner;
			unit.GetComponent<Ship> ().objective = targetPos;

			unit.SetActive(true);
			
			baseScript.NumberOfUnits--;
		}
	}

	Vector3 CalcRandSpawnPos(Vector3 target)	//Using trigonometry, find the point towards target base that is 1.3 units away    +    add a little random
	{
		float angle = Vector3.Angle(-Vector3.left, target - transform.position);
		
		angle = 2 * Mathf.PI * angle / 360;	//to radians
		
		//Debug.DrawRay (transform.position, target - transform.position);
		//Debug.DrawRay (transform.position, -Vector3.left);
		
		//angle could be negative
		Vector3 cross = Vector3.Cross(-Vector3.left, target - transform.position);
		if (cross.z < 0)
			angle = -angle;
		
		angle += Random.Range (-Mathf.PI / 3, Mathf.PI / 3);
		
		Vector3 spawnPos = 1.3f * new Vector3 (Mathf.Cos (angle), Mathf.Sin (angle), 0);
		return transform.position + spawnPos;
	}
}
