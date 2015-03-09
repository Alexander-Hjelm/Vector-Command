using UnityEngine;
using System.Collections;

public class ParticleDestroySelf : MonoBehaviour {

	public float disableTime = 1;

	// Use this for initialization
	void OnEnable () {
		Invoke ("DisableSelf", disableTime);
	}

	void DisableSelf()
	{
		gameObject.SetActive (false);
	}
}
