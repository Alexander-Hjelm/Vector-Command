using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	//refs
	Hp hpScript;

	//vars
	public Vector3 target;
	float speed;
	float topSpeed = 0.05f;	//Settable from outside
	float turnSpeed = 2.5f; //Settable from outside

	bool inCombat = false;

	void Start () {
		hpScript = this.GetComponent <Hp>();
		speed = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
		//Movement
		speed = Mathf.Lerp (speed, topSpeed, Time.deltaTime * 0.7f);
		this.transform.position += transform.up * speed;
		this.transform.position -= new Vector3 (0,0,transform.position.z);

		Quaternion newRoatation = Quaternion.LookRotation (transform.position - target, Vector3.forward);
		newRoatation.x = 0.0f;	//only rot in z
		newRoatation.y = 0.0f;
		this.transform.rotation = Quaternion.Slerp (transform.rotation, newRoatation, Time.deltaTime * turnSpeed);
		 
		//Alive?
		if (hpScript.hp <= 0)
		{
			Destroy(this.gameObject);
		}

	}
}
