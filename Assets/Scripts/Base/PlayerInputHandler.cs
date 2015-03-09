using UnityEngine;
using System.Collections;

public class PlayerInputHandler : MonoBehaviour {

	//refs to this
	Base baseScript;
	ShipSpawn shipSpawn;
	public GameObject colliderObj3D;
	
	//refs to other
	PlayerHandler playerHandler;

	//vars
	public Vector3 clickPos;
	public Vector3 mousePos;
	public GameObject hoverTarget;

	public bool spawning = false;

	LineRenderer lineRenderer;

	void Awake ()
	{
		baseScript = gameObject.GetComponent<Base> ();
		shipSpawn = gameObject.GetComponent<ShipSpawn> ();
		playerHandler = GameObject.FindGameObjectWithTag ("PlayerHandler").GetComponent<PlayerHandler>();
		lineRenderer = this.GetComponentInChildren<LineRenderer> ();
		lineRenderer.enabled = false;
	}

	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Vector3 first = clickPos - new Vector3(0,0,clickPos.z);
			Vector3 second = mousePos - new Vector3(0,0,mousePos.z);

			lineRenderer.SetPosition(0, first);
			lineRenderer.SetPosition(1, second);
		}
	}

	void OnMouseDown()
	{
		if (baseScript.owner == playerHandler.playerInt)	//If we clicked on one o the player's bases
		{
			clickPos = ClickPos2World();
			StartCoroutine("ShipSpawnHandler");

			// Line Renderer Stuph
			lineRenderer.enabled = true;
		}
	}

	void OnMouseUp()
	{
		shipSpawn.StopSpawn();
		spawning = false;
		lineRenderer.enabled = false;
	}
	
	IEnumerator ShipSpawnHandler()
	{
		while (Input.GetMouseButton(0))
		{
			mousePos = ClickPos2World();
			Debug.DrawLine(clickPos, mousePos);

			hoverTarget = GetCollision();
			if (hoverTarget != null 
			    && hoverTarget.tag != "BG"
			    && baseScript.neighbours.Contains( hoverTarget.transform.parent.gameObject ) && hoverTarget != colliderObj3D)
			{
				if (!spawning)
				{
					shipSpawn.initiateAttack(baseScript, hoverTarget.transform.parent.position);
					spawning = true;
				}
			}
			else
			{
				shipSpawn.StopSpawn();
				spawning = false;
			}

			//If, then invoke spawn ship w/ other base as target

			yield return null;
		}
	}

	Vector3 ClickPos2World()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast (ray, out hit);
		Vector3 worldPos = hit.point;
		worldPos.z = 0;
		return worldPos;
	}

	GameObject GetCollision()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast (ray, out hit);
		if (hit.collider)
		{
			return hit.collider.gameObject;
		}
		else return null;
	}
}
