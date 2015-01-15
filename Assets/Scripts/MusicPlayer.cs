using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
	public AudioClip[] soundtrack;
	
	// Use this for initialization
	void Start ()
	{
		audio.clip = soundtrack[Random.Range(0, soundtrack.Length)];
		audio.Play();
		InvokeRepeating("PlayMusic", 0f, 5f);
	}
	
	// Update is called once per frame
	void PlayMusic ()
	{
		if (!audio.isPlaying)
		{
			audio.clip = soundtrack[Random.Range(0, soundtrack.Length)];
			audio.Play();
		}
	}
}