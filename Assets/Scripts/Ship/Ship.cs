using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	//refs
	ObjectPoolerScript blastPool;
	Hp hpScript;
	//ProxiTrigger proxiTrigger;	//For detecting other ships

	//prefab
	//public GameObject blast;

	//vars
	public int owner;
	public Vector3 objective;	//target of lifetime
	public Vector3 target;	//momentary target
	public GameObject combatTarget;
	float speed;
	float topSpeed = 5f;	//Settable from outside
	float turnSpeed = 2.5f; //Settable from outside

	public bool inCombat = false;
	bool firing = false;

	void Awake () {
		blastPool = GameObject.FindGameObjectWithTag ("BlastPool").GetComponent<ObjectPoolerScript>();
		hpScript = this.GetComponent <Hp>();
		//proxiTrigger = this.GetComponentInChildren<ProxiTrigger> ();
	}
	
	void OnEnable()
	{
		speed = 0;
		hpScript.hp = hpScript.maxHp;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (inCombat && combatTarget.activeSelf)	//in combat!
		{
			target = combatTarget.transform.position;
			if (!firing)
			{
				firing = true;
				InvokeRepeating("Fire", 0.05f, 1f);		//Fire at target
			}
		}
		else	//Not in combat, or target was destroyed
		{
			inCombat = false;
			firing = false;
			target = objective;
			CancelInvoke("Fire");	//Make sure we're not firing
		}

		//Movement
		speed = Mathf.Lerp (speed, topSpeed, Time.deltaTime * 0.7f);
		this.transform.position += transform.up * speed * Time.deltaTime;
		this.transform.position -= new Vector3 (0,0,transform.position.z);

		Quaternion newRoatation = Quaternion.LookRotation (transform.position - target, Vector3.forward);
		newRoatation.x = 0.0f;	//only rot in z
		newRoatation.y = 0.0f;
		this.transform.rotation = Quaternion.Slerp (transform.rotation, newRoatation, Time.deltaTime * turnSpeed);
		 
		//Alive?
		if (hpScript.hp <= 0)
		{
			CancelInvoke();		//Or else fire incvoke will continue
			gameObject.SetActive(false);
		}
	}

	void Fire()
	{
		Quaternion blastRoatation = Quaternion.LookRotation (transform.position - combatTarget.transform.position, Vector3.forward);
		blastRoatation.x = 0.0f;	//only rot in z
		blastRoatation.y = 0.0f;

		GameObject blastInst = blastPool.GetAvailablePooledObject ();

		blastInst.transform.position = this.transform.position;
		blastInst.transform.rotation = blastRoatation;
		blastInst.GetComponent<Blast> ().combatTarget = combatTarget;

		blastInst.SetActive (true);
	}
}