using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CentralAI : MonoBehaviour {

	public List<Request> requestList = new List<Request>();
	public float gameFreq = 0.03f;

	Request currentInstruction;
	int currentCycle = 0;

	PlayerHandler playerHandler;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("performCurrentInstruction", 0.5f, gameFreq);
	}

	void performCurrentInstruction()
	{
		//print ("current cycle: " + currentCycle.ToString () + ", max: " + currentInstruction.howMany.ToString ());
		if (currentCycle == 0 && requestList.Count != 0)	// no instruciton is loaded
		{
			currentInstruction = requestList[0];	//set current instruction
			requestList.RemoveAt(0);				//remove the set instruction from our list

			//print (currentInstruction.sendingBase.name + " Changed instruction. Sending " + currentInstruction.howMany + " units.");

			currentInstruction.sendingBase.GetComponent<ShipSpawn>().initiateAttack( currentInstruction.sendingBase.GetComponent<Base>(), currentInstruction.targetPos);	//Spawn ship w/ data of
			currentCycle++;
		}
		else if (currentInstruction != null)
		{
			currentCycle++;
		}

		if(currentInstruction != null && currentCycle > currentInstruction.howMany)	//now we have waited enough
		{
			currentInstruction.sendingBase.GetComponent<ShipSpawn>().StopSpawn();
			currentInstruction = null;
			currentCycle = 0;
		}
	}
		
	//function for taking in new request, evaluating priority and inserting into RequestList
	public void inputRequest(Request request)
	{
		//print ("Sending units: " + request.howMany.ToString());

		requestList.Add (request);
	}


	//fucntionc for sorting requestList according to priority
}
