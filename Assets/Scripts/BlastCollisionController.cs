using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlastCollisionController : MonoBehaviour {

	public Blast blastScript;

	public float repTime = 0.1f;	//InvokeRep rate
	public float threshDist = 0.3f;	//max dist for collision to occur

	public GameObject particlePool;

	bool collided;	//for avoiding spawning multiple particle systems

	void Awake () {
		blastScript = this.GetComponent<Blast> ();

		particlePool = GameObject.FindGameObjectWithTag ("ParticlePool");
	}
	
	void OnEnable()
	{
		InvokeRepeating ("CheckCollision", 0f, repTime);
		collided = false;
	}
	
	void CheckCollision()	//Main
	{
		if(!collided && blastScript.combatTarget != null)
		{
			if((this.transform.position - blastScript.combatTarget.transform.position).magnitude < threshDist)
			{
				collided = true;

				blastScript.combatTarget.GetComponent<Hp>().modHp(-10);		//deal dmg

				if(blastScript.combatTarget.GetComponent<Hp>().hp <= 0)
				{
					//Spawn particle system
					InstParticleSystem();
				}

				gameObject.SetActive(false);				//Inactivate
			}
		}
	}

	void InstParticleSystem()
	{
		GameObject partSys = particlePool.GetComponent<ObjectPoolerScript> ().GetAvailablePooledObject ();
		partSys.transform.position = blastScript.combatTarget.transform.position;

		Quaternion partRot = partSys.transform.rotation = this.transform.rotation;
		partRot.x = 0.0f;	//only rot in z
		partRot.y = 0.0f;
		partSys.transform.rotation = partRot;

		//Set color to target ship
		partSys.GetComponent<ParticleSystem> ().startColor = blastScript.combatTarget.GetComponent<SpriteRenderer>().color;

		partSys.SetActive (true);
	}

}
