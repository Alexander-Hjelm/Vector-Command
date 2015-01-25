using UnityEngine;
using System.Collections;

public class ShipSpawner : MonoBehaviour {

	//refs to this
	Base baseScript;
	Owner ownerScript;
	public GameObject colliderObj3D;

	//refs to other
	PlayerHandler playerHandler;

	//vars
	public Vector3 clickPos;
	public Vector3 mousePos;
	public GameObject hoverTarget;

	void Awake ()
	{
		baseScript = gameObject.GetComponent<Base> ();
		ownerScript = gameObject.GetComponent<Owner> ();


		playerHandler = GameObject.FindGameObjectWithTag ("PlayerHandler").GetComponent<PlayerHandler>();
	}

	void Update () {
		
	}

	void OnMouseDown()
	{
		if (ownerScript.owner == playerHandler.playerInt)	//If we clicked on one o the player's bases
		{
			clickPos = ClickPos2World();
			StartCoroutine("ShipSpawnHandler");
		}
	}

	IEnumerator ShipSpawnHandler()
	{
		while (Input.GetMouseButton(0))
		{
			print ("drue");
			mousePos = ClickPos2World();
			Debug.DrawLine(clickPos, mousePos);

			hoverTarget = GetCollision();
			if (hoverTarget != null && hoverTarget != colliderObj3D)
			{
				Invoke("SpawnShipInvoked", 0.2f);
			}

			//If, then invoke spawn ship w/ other base as target


			yield return null;
		}
	}

	void SpawnShipInvoked()
	{
		print (hoverTarget.transform.parent.position);
		baseScript.SpawnUnit(this.transform.position + Vector3.up * 2, Quaternion.identity, hoverTarget.transform.parent.position);
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
