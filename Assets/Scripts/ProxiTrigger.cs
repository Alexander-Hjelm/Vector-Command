using UnityEngine;
using System.Collections;

public class ProxiTrigger : MonoBehaviour {

	//refs on this
	Ship shipScript;
	Owner ownerScript;

	// Use this for initialization
	void Start () {
		shipScript = this.GetComponentInParent<Ship> ();
		ownerScript = this.GetComponentInParent<Owner> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Ship")
		{
			if (other.gameObject.GetComponent<Owner>().owner != ownerScript.owner)	//If other ship is not on our team
			{
				if(!shipScript.inCombat && !other.gameObject.GetComponent<Ship>().inCombat)	//if this ship and other ship are not in combat			&& !other.gameObject.GetComponent<Ship>().inCombat
				{
					shipScript.inCombat = true;	//enter combat
					shipScript.combatTarget = other.gameObject;
				}
			}
		}
	}
}
