using UnityEngine;
using System.Collections;

public class Request 
{
	public enum RequestType{Attack, Defend, Fortify};

	public GameObject sendingBase;
	public Vector3 targetPos;
	public int howMany;
	public float priority;
	public RequestType requestType;

	public Request(GameObject sb, Vector3 tp, int hm, float p, RequestType rt) //constructor
	{
		this.sendingBase = sb;
		this.targetPos = tp;
		this.howMany = hm;
		this.priority = p;
		this.requestType = rt;
	}
}
