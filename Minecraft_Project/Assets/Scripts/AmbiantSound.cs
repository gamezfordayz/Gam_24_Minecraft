using UnityEngine;
using System.Collections;

public class AmbiantSound : MonoBehaviour {

	public AudioClip desertSound;
	public AudioClip caveSound;
	public AudioClip normalSound;
	public float normalVolume = .5f;

	public AudioSource audioScource;

	void Start () 
	{
		audioScource.clip = normalSound;
		audioScource.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < 30f ) 
		{
			audioScource.clip = caveSound;
			audioScource.volume = 1f;
			if(!audioScource.isPlaying)
				audioScource.Play();
		}
		else
		{
			audioScource.clip = normalSound;
			audioScource.volume = normalVolume;
		}
	}
}
