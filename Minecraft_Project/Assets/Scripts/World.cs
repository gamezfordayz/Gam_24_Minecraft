using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour 
{
	public static World currentWorld;
	public int chunkLength = 20 , chunkHeight = 140, groundOffset  = 40 ,seed = 0;
	public float offsetValueX ,offsetValueZ = 0;
	public List<Transform> chunks;
	public GameObject player = null;
	public GameObject chunkFab = null;
	public float instantiateDistance = 50;
	public float waitForSeconds = 1f;
	Queue<Vector3> objectsToSpawn = new Queue<Vector3>();
	
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

	void Start()
	{

		StartCoroutine (CheckForSpawn());
		StartCoroutine (SpawnChunk());
	}

	void Update()
	{

	}

	IEnumerator CheckForSpawn()
	{
		while (true) 
		{
			if(objectsToSpawn.Count == 0)
				GetChunkPos ();
			yield return new WaitForSeconds (waitForSeconds);
		}
	}

	IEnumerator SpawnChunk()
	{
		while (true) {
			if (objectsToSpawn.Count != 0) {
				GameObject temp = (GameObject)Instantiate (chunkFab, objectsToSpawn.Dequeue(), Quaternion.identity);
				chunks.Add(temp.transform);
			}
			yield return new WaitForSeconds (waitForSeconds/10f);
		}
	}

	void GetChunkPos()
	{

		for (int x = Mathf.RoundToInt(player.transform.position.x - instantiateDistance); x <= Mathf.RoundToInt(player.transform.position.x + instantiateDistance); x += chunkLength) 
		{

			for (int z = Mathf.RoundToInt(player.transform.position.z - instantiateDistance); z <= Mathf.RoundToInt(player.transform.position.z + instantiateDistance); z += chunkLength) 
			{
				Vector3 tempPos = new Vector3( (float)x, 0f, (float)z);
				tempPos.x = Mathf.RoundToInt(tempPos.x / (float)chunkLength) * chunkLength;
				tempPos.z = Mathf.RoundToInt(tempPos.z / (float)chunkLength) * chunkLength;

				if(!FindChunk(tempPos))
				{
					Debug.Log (tempPos);
					objectsToSpawn.Enqueue(tempPos);
				}
			}
		}
	}

	bool FindChunk(Vector3 pos)
	{
		bool found = false;
		for (int i = 0; i < chunks.Count; i++) 
		{
			Vector3 chunkPos = chunks[i].position;
			if(chunkPos == pos)
				found = true;
		}
		return found;
	}
}
