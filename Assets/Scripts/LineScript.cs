using UnityEngine;
using System.Collections;

public class LineScript : MonoBehaviour {

	//refs to other scripts
	PlayerHandler playerHandler;

	//Refs to components on self
	LineRenderer lineRenderer;

	public GameObject targetBase;	//Which base shall we draw a line to?

	void Start ()
	{
		lineRenderer = gameObject.GetComponent<LineRenderer> ();
		playerHandler = GameObject.FindGameObjectWithTag ("PlayerHandler").GetComponent<PlayerHandler>();
	}
	
	void Update ()
	{
		Ray ray = new Ray (transform.parent.position, (targetBase.transform.position - transform.parent.position));

		float dist = (transform.parent.position - targetBase.transform.position).magnitude;

		lineRenderer.SetPosition (0, ray.GetPoint(0.97f));	//pos 0 = line start		//Change distance from 0.97 to dynamically adjust for base size
		lineRenderer.SetPosition (1, ray.GetPoint(dist - 0.97f));	//pos 1 = line end

		lineRenderer.SetColors (playerHandler.playerCols [this.GetComponentInParent<Owner> ().owner], playerHandler.playerCols [targetBase.GetComponent<Owner> ().owner]);
	}
}
