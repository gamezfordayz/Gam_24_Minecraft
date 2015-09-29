using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour 
{
	public static World currentWorld;
	public float x , z;
	public int chunkLength = 20 , chunkHeight = 140, groundOffset  = 40 ,seed = 0;
	public float offsetValueX ,offsetValueZ = 0;
	public List<Transform> chunks;
	public GameObject[] TEMP = null;			// TEMPORARY UNTIL WE INSTAITE AND ADD TO LIST
	// Use this for initialization
	void Awake () 
	{
		currentWorld = this;
		chunks = new List<Transform>();
		if (seed == 0) {
			seed = Random.Range (0, int.MaxValue);

		}
		Random.seed = seed;
		offsetValueX = Random.Range(1 , 10000);
		offsetValueZ = Random.Range(1 , 10000);

	}

	// 564264800 seed is cool
	void Update () 
	{
		// TEMP TILL WE INSTANTIATE THE CHUNKS
		TEMP = GameObject.FindGameObjectsWithTag("World");
	}
}
