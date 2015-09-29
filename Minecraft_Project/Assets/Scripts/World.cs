using UnityEngine;
using System.Collections;

public class World : MonoBehaviour 
{
	public static World currentWorld;
	public float x , z;
	public int chunkLength = 20 , chunkHeight = 140, seed = 0;
	public float offsetValueX ,offsetValueZ = 0;
	// Use this for initialization
	void Awake () 
	{
		currentWorld = this;
		if (seed == 0) {
			seed = Random.Range (0, int.MaxValue);

		}
		Random.seed = seed;
		offsetValueX = Random.Range(1 , 10000);
		offsetValueZ = Random.Range(1 , 10000);

	}

	// 564264800 seed is cool
	// Update is called once per frame
	void Update () 
	{
	}
}
