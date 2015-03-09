using UnityEngine;
using System.Collections;

public class LineRemover : MonoBehaviour {

	//Removes duplicate lines between bases


	GameObject[] lines;

	// Use this for initialization
	void OnEnable () {
		Invoke ("RemoveLines", 0.05f);	//Can't be during setup
	}

	void RemoveLines()
	{
		lines = GameObject.FindGameObjectsWithTag ("Line");
		
		foreach (GameObject line1 in lines)
		{
			//print ("foun one!");
			foreach (GameObject line2 in lines)
			{
				if (line1.activeSelf && line2.activeSelf)
				{
					if(line1.GetComponent<LineScript>().thisBase == line2.GetComponent<LineScript>().targetBase && line1.GetComponent<LineScript>().targetBase == line2.GetComponent<LineScript>().thisBase)
					{	
						line2.SetActive(false);
						break;
					}
				}
			}
		}
	}
}
