using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBaseController : MonoBehaviour {

	GameObject playerHandler;

	//refs to self
	Base baseScript;

	//vars
	float leastFriendlies = 0;
	float leastEnemies = 0;

	float fortifyPriority = 0;
	float attackPriority = 0;
	
	GameObject leastFriend;
	GameObject leastEnemy;

	List<GameObject> neighbours;

	//Difficulty-related stuff
	int difficulty = 3;
	//Easy-med-hard-insane
	float[] minUnitRatioToAttack = {1.2f, 1.5f, 1.7f, 1.9f};


	void Start()
	{
		playerHandler = GameObject.FindGameObjectWithTag ("PlayerHandler");

		baseScript = gameObject.GetComponent<Base> ();
		neighbours = baseScript.neighbours;

		InvokeRepeating ("EvaluateNeighbours", 1f, 0.1f);	//adj for difficulty

		//set difficulty:
		difficulty = PlayerPrefs.GetInt("diff");
	}

	void EvaluateNeighbours()
	{
		if (baseScript.owner != 0 && baseScript.owner != playerHandler.GetComponent<PlayerHandler>().playerInt)	//If not neutral or player
		{
			///// LOL U NUUUZZ CHANGE DIZ BLEUGHRHEUGLR!!!!!
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
				int howMany = (int)((baseScript.NumberOfUnits - leastFriend.GetComponent<Base>().NumberOfUnits)*0.3);

				if (howMany == 0);
				{
					return;	//Dont send 0
				}

				//print ("us: " + baseScript.NumberOfUnits + ", them: " + leastFriendlies);
				//print ((int)((baseScript.NumberOfUnits - leastFriend.GetComponent<Base>().NumberOfUnits)*0.3) + " units");
				fortifyPriority = 1 - leastFriendlies / (baseScript.NumberOfUnits + 1);
				baseScript.centralAI.inputRequest(new Request(this.gameObject,
				                                   leastFriend.transform.position,
				                                   howMany,
				                                   fortifyPriority,
				                                   Request.RequestType.Fortify));
				return;	//Don't attack
			}
			//Attack
			if(leastEnemy != null && tryChance(baseScript.NumberOfUnits, leastEnemies))
			{
				int howMany = (int)(leastEnemy.GetComponent<Base>().NumberOfUnits + 10);


				if (howMany == 0)
				{
					print (howMany);
					return;	//Dont send 0
				}


				//Set priority      mind div(0)
				attackPriority = 1 - leastEnemies / baseScript.NumberOfUnits;

				baseScript.centralAI.inputRequest(new Request(this.gameObject,
				                                   leastEnemy.transform.position,
				                                   howMany,
				                                   attackPriority,
				                                   Request.RequestType.Attack));
			}
		}
	}

	bool tryChance(float us, float them)
	{
		us += 1; 	//Prevent div by 0

		float f = Random.Range (0f, minUnitRatioToAttack[difficulty]);	//adj for difficulty

		if (f > them/us + 0.1 /* && them != 0 && us != 0 */)
		{
			//if (leastFriend != null)
				//print (this.name + " attacking: " + leastFriend.name + ". us: " + us + ", them: " + them + ". Random = " + f + " , chance = " (int)(them/us + 0.1));
			//if (us == 0) {print ("div by 0");}
			return true;
		}
		return false;
	}
}
