using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBaseController : MonoBehaviour {

	GameObject playerHandler;

	//refs to self
	Base baseScript;
	public CentralAI centralAI;

	//vars
	float leastFriendlies = 0;
	float leastEnemies = 0;

	float fortifyPriority = 0;
	float attackPriority = 0;
	
	GameObject leastFriend;
	GameObject leastEnemy;

	List<GameObject> neighbours;

	void Start()
	{
		playerHandler = GameObject.FindGameObjectWithTag ("PlayerHandler");

		baseScript = gameObject.GetComponent<Base> ();
		neighbours = baseScript.neighbours;

		centralAI = GameObject.FindGameObjectWithTag("Central AI").transform.FindChild("AI_" + baseScript.owner.ToString()).GetComponent<CentralAI>();

		InvokeRepeating ("EvaluateNeighbours", 1f, 1f);
	}

	void EvaluateNeighbours()
	{
		if (baseScript.owner != 0 && baseScript.owner != playerHandler.GetComponent<PlayerHandler>().playerInt)	//If not neutral or player
		{
			print (gameObject.name + "Attacking enemy!");

			leastFriendlies = -1;
			leastEnemies = -1;

			leastFriend = null;
			leastEnemy = null;

			foreach(GameObject n in neighbours)
			{
				Base nBaseScript = n.GetComponent<Base>();

				if(nBaseScript.owner == baseScript.owner)	//friendly
				{
					if (nBaseScript.NumberOfUnits < leastFriendlies || leastFriendlies == -1)
					{
						leastFriendlies = nBaseScript.NumberOfUnits;
						leastFriend = n;
					}
				}
				else
				{
					if (nBaseScript.NumberOfUnits < leastEnemies || leastEnemies == -1)
					{
						leastEnemies = nBaseScript.NumberOfUnits;
						leastEnemy = n;
					}
				}
			}
			//Fortify
			if(leastFriend != null && tryChance(baseScript.NumberOfUnits, leastFriendlies))
			{
				fortifyPriority = 1 - leastFriendlies / baseScript.NumberOfUnits;
				centralAI.inputRequest(new Request(this.gameObject,
				                                   leastFriend.transform.position,
				                                   (int)((baseScript.NumberOfUnits - leastFriend.GetComponent<Base>().NumberOfUnits)*0.3),
				                                   fortifyPriority,
				                                   Request.RequestType.Fortify));
				return;	//Don't attack
			}
			//Attack
			if(tryChance(baseScript.NumberOfUnits, leastEnemies))
			{
				//Set priority      mind div(0)
				attackPriority = 1 - leastEnemies / baseScript.NumberOfUnits;
				

					centralAI.inputRequest(new Request(this.gameObject,
				                                   leastEnemy.transform.position,
				                                   (int)(leastEnemy.GetComponent<Base>().NumberOfUnits + 10),
				                                   attackPriority,
				                                   Request.RequestType.Attack));
			}
		}
	}

	bool tryChance(float us, float them)
	{

		if (Random.Range(0f , 1f) > them/us)
		{
			return true;
		}
		return false;
	}
}
