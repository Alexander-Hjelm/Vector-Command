using UnityEngine;
using System.Collections;

public class ShipSpawn : MonoBehaviour {

	ObjectPoolerScript shipPool;

	void Awake()
	{
		shipPool = GameObject.FindGameObjectWithTag ("ShipPool").GetComponent<ObjectPoolerScript> ();
	}

	public void SpawnUnit(Base baseScript, Vector3 pos, Quaternion rot, Vector3 targetPos)
	{
		if(baseScript.NumberOfUnits > 0)
		{
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
}
