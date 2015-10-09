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
		audioScource.clip = desertSound;
		audioScource.Play ();
	}

}
