using UnityEngine;
using System;	//var
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public class XMLGetter : MonoBehaviour {

	public GameObject basePrefab;

	List<MyData> dataList = new List<MyData> ();
	List<GameObject> baseList = new List<GameObject>();

	// Use this for initialization
	void Start ()
	{
		/*
		List<MyData> dataList = new List<MyData> ();
		dataList.Add (new MyData () { name = "Adolf", shite = 10 });
		dataList.Add (new MyData () { name = "Winston", shite = 20 });
		var ser = new System.Xml.Serialization.XmlSerializer(dataList.GetType());
		var writer = new System.IO.StreamWriter("min.xml");
		ser.Serialize (writer, dataList);
			writer.Close ();
		writer.Dispose ();
		*/

		ReadXML ();

		foreach(MyData md in dataList)
		{
			baseList.Add(GameObject.Instantiate(basePrefab) as GameObject);	//inst base and add instance to base list

			baseList.Last().GetComponent<Base>().worldPos = new Vector3(md.x, md.y, 0);	//assign world pos
			baseList.Last().GetComponent<Owner>().owner = md.owner;	//assign owner
			//Debug.Log (md.neighbours[0].id);
		}

		//assign neighbours of each base (go) from neighbours of each node in dataList
		for (int i = 0; i < baseList.Count; i++)
		{
			if (dataList[i].neighbours != null)
			{
				foreach (Neighbour n in dataList[i].neighbours)
				{
					baseList[i].GetComponent<Base>().neighbours.Add(baseList[n.id]);	//add first as second's neighbour
					baseList[n.id].GetComponent<Base>().neighbours.Add(baseList[i]);		//add second as first's neighbour
					Debug.Log("Assigning neighbour " + n.id + " to base " + i);
				}
			}

			baseList[i].name = ("Base " + i);
		}

		foreach (GameObject bas in baseList)
		{
			//foreach (int n in bas.GetComponent<>
		}
	}

	void ReadXML()
	{
		var ser = new System.Xml.Serialization.XmlSerializer(dataList.GetType());

		using (var fs = new System.IO.FileStream("min.xml", System.IO.FileMode.Open))
		{
			dataList = (List<MyData>) ser.Deserialize(fs); 
		}

		/*
		foreach(MyData node in dataList)
		{
			Debug.Log(node.x + " : " + node.y + " : Owner = " + node.owner);
		}
		*/
	}
}
