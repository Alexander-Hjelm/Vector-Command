using UnityEngine;
using System.Collections;

public class Blast : MonoBehaviour {

	public GameObject combatTarget;	//Set in ship script on enable

	float speed = 25;

	// Use this for initialization
	void OnEnable () {
		Invoke ("TimedDestroy", 10f);	//failsafe
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.up * speed * Time.deltaTime;
	}

	void TimedDestroy()
	{
		this.gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		GameObject other = coll.gameObject;

		if (coll.gameObject.GetInstanceID() == combatTarget.GetInstanceID() && other.tag == "Ship")	//If we are colliding w/ the ship we were aiming for
		{
			other.GetComponent<Hp>().modHp(-4);		//deal dmg
			gameObject.SetActive(false);				//Inactivate
		}
	}

}
