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

	public bool spawning = false;

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
				SpawnShipInvoked();
				CalcRandSpawnPos(hoverTarget.transform.position);
			}




			//If, then invoke spawn ship w/ other base as target


			yield return null;
		}
	}

	void SpawnShipInvoked()
	{
		Vector3 pos = CalcRandSpawnPos(hoverTarget.transform.position);
		Quaternion rot = Quaternion.LookRotation (transform.position - pos, Vector3.forward);
		rot.x = 0;
		rot.y = 0;

		baseScript.SpawnUnit(pos, rot, hoverTarget.transform.parent.position);
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

	Vector3 CalcRandSpawnPos(Vector3 target)	//Using trigonometry, find the point towards target base that is 1.3 units away    +    add a little random
	{
		float angle = Vector3.Angle(-Vector3.left, target - transform.position);

		angle = 2 * Mathf.PI * angle / 360;	//to radians

		Debug.DrawRay (transform.position, target - transform.position);
		Debug.DrawRay (transform.position, -Vector3.left);

		//angle could be negative
		Vector3 cross = Vector3.Cross(-Vector3.left, target - transform.position);
		if (cross.z < 0)
			angle = -angle;

		angle += Random.Range (-Mathf.PI / 3, Mathf.PI / 3);

		Debug.Log (angle);

		Vector3 spawnPos = 1.3f * new Vector3 (Mathf.Cos (angle), Mathf.Sin (angle), 0);
		return transform.position + spawnPos;
	}
}
