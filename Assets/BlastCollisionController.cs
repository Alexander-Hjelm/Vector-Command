using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlastCollisionController : MonoBehaviour {

	public Blast blastScript;

	public float repTime = 0.1f;	//InvokeRep rate
	public float threshDist = 0.2f;	//max dist for collision to occur

	void Awake () {
		blastScript = this.GetComponent<Blast> ();
	}
	
	void OnEnable()
	{
		InvokeRepeating ("CheckCollision", 0f, repTime);
	}
	
	void CheckCollision()	//Main
	{
		if(blastScript.combatTarget != null)
		{
			if((this.transform.position - blastScript.combatTarget.transform.position).magnitude < threshDist)
			{
				blastScript.combatTarget.GetComponent<Hp>().modHp(-4);		//deal dmg
				gameObject.SetActive(false);				//Inactivate
			}
		}
	}

}
