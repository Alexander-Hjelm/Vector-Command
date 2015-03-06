using UnityEngine;
using System.Collections;

public class Blast : MonoBehaviour {

	public GameObject combatTarget = null;	//Set in ship script on enable

	float speed = 35;

	// Use this for initialization
	void OnEnable () {
		Invoke ("TimedDestroy", 5f);	//failsafe
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.up * speed * Time.deltaTime;
	}

	void TimedDestroy()
	{
		this.gameObject.SetActive(false);
	}
}
