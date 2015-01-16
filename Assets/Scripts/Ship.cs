using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	Hp hpScript;

	// Use this for initialization
	void Start () {
		hpScript = this.GetComponent <Hp>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (hpScript.hp <= 0)
		{
			Destroy(this.gameObject);
		}

	}
}
