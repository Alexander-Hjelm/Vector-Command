using UnityEngine;
using System.Collections;

public class LineScript : MonoBehaviour {

	//refs to other scripts
	PlayerHandler playerHandler;

	//Refs to components on self
	LineRenderer lineRenderer;

	//public Vector3 startPoint;	//Which point shall we draw a line from?
	//public Vector3 endPoint;

	public GameObject thisBase;
	public GameObject targetBase;

	//public Color startCol;
	//public Color endCol;

	void Start ()
	{
		lineRenderer = gameObject.GetComponent<LineRenderer> ();
		playerHandler = GameObject.FindGameObjectWithTag ("PlayerHandler").GetComponent<PlayerHandler>();
	}
	
	void Update ()
	{
		Ray ray = new Ray (thisBase.transform.position, (targetBase.transform.position - thisBase.transform.position));

		float dist = (targetBase.transform.position - thisBase.transform.position).magnitude;

		lineRenderer.SetPosition (0, ray.GetPoint(0.97f));	//pos 0 = line start		//Change distance from 0.97 to dynamically adjust for base size
		lineRenderer.SetPosition (1, ray.GetPoint(dist - 0.97f));	//pos 1 = line end

		lineRenderer.SetColors (playerHandler.playerCols [thisBase.GetComponent<Owner> ().owner], playerHandler.playerCols [targetBase.GetComponent<Owner> ().owner]);
	}
}
