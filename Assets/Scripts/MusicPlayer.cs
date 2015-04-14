using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
	public AudioClip[] soundtrack;
	
	// Use this for initialization
	void Start ()
	{
		GetComponent<AudioSource>().clip = soundtrack[Random.Range(0, soundtrack.Length)];
		GetComponent<AudioSource>().Play();
		InvokeRepeating("PlayMusic", 0f, 5f);
	}
	
	// Update is called once per frame
	void PlayMusic ()
	{
		if (!GetComponent<AudioSource>().isPlaying)
		{
			GetComponent<AudioSource>().clip = soundtrack[Random.Range(0, soundtrack.Length)];
			GetComponent<AudioSource>().Play();
		}
	}
}