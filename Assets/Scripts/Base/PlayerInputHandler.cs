using UnityEngine;
using System.Collections;

public class PlayerInputHandler : MonoBehaviour {

	//refs to this
	Base baseScript;
	ShipSpawn shipSpawn;
	GameObject activeRing;
	public GameObject colliderObj3D;

	//refs to other
	PlayerHandler playerHandler;

	//vars
	public Vector3 clickPos;
	public Vector3 mousePos;
	public GameObject hoverTarget;
	bool active = false;

	public bool spawning = false;

	LineRenderer lineRenderer;

	void Awake ()
	{
		baseScript = gameObject.GetComponent<Base> ();
		shipSpawn = gameObject.GetComponent<ShipSpawn> ();
		playerHandler = GameObject.FindGameObjectWithTag ("PlayerHandler").GetComponent<PlayerHandler>();
		lineRenderer = this.GetComponentInChildren<LineRenderer> ();
		lineRenderer.enabled = false;
		activeRing = this.transform.FindChild ("ActiveRing").gameObject;
		activeRing.SetActive (false);
	}

	void Update()
	{
		if(baseScript.owner == playerHandler.playerInt)
		{
			if (Input.GetMouseButton(0))
			{
				Vector3 first = clickPos - new Vector3(0,0,clickPos.z);
				Vector3 second = mousePos - new Vector3(0,0,mousePos.z);

				lineRenderer.SetPosition(0, first);
				lineRenderer.SetPosition(1, second);
			}
			else
			{
				Activate(false);
			}

			if(active && baseScript.owner != playerHandler.playerInt)
			{
				Activate(false);
			}
		}
	}

	void OnMouseOver()
	{
		if(Input.GetMouseButton(0) && baseScript.owner == playerHandler.playerInt)
		{
			Activate(true);
		}
	}
	
	IEnumerator ShipSpawnHandler()
	{
		while (Input.GetMouseButton(0))
		{
			mousePos = ClickPos2World();
			Debug.DrawLine(clickPos, mousePos);

			hoverTarget = GetCollision();
			if (hoverTarget != null && hoverTarget.tag != "BG")
			{
				if (!spawning)
				{
					if(baseScript.neighbours.Contains( hoverTarget.transform.parent.gameObject ) && hoverTarget != colliderObj3D)
					{
						shipSpawn.StopSpawn();
						shipSpawn.initiateAttack(baseScript, hoverTarget.transform.parent.position);
						spawning = true;
					}
					else if(hoverTarget.transform.parent.position != this.transform.position)	//Could be this
					{
						GameObject b = FindClosestBase(hoverTarget.transform.parent);
						if (b != null)
							{
							shipSpawn.StopSpawn();
							shipSpawn.initiateAttack(baseScript, FindClosestBase(hoverTarget.transform.parent).transform.position);
							spawning = true;
						}
					}
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

	public void Activate(bool b)
	{
		activeRing.SetActive (b);
		active = b;
		// Line Renderer Stuph
		lineRenderer.enabled = b;

		if(b)
		{
			clickPos = ClickPos2World();
			StartCoroutine("ShipSpawnHandler");
		}
		else
		{
			shipSpawn.StopSpawn();
			spawning = false;
			StopCoroutine("ShipSpawnHandler");
		}

		return;
	}

	GameObject FindClosestBase(Transform inputBase)
	{
		GameObject closest = null;
		float closestDist = 999999999f;
		foreach(GameObject b in baseScript.neighbours)
		{
			float mag = (b.transform.position - inputBase.position).magnitude;

			if(mag < closestDist && b.GetComponent<Base>().owner == playerHandler.playerInt)
			{
				closest = b;
				closestDist = mag;
			}
		}
		return closest;
	}
}
