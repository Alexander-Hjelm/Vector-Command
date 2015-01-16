using UnityEngine;
using System.Collections;

public class Blast : MonoBehaviour {

	float speed = 10;

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

}
