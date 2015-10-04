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
	public float deSpawnDistance = 100;
	public float waitForSeconds = 1f;
	Queue<Vector3> objectsToSpawn = new Queue<Vector3>();
	Queue<GameObject> objectsToSetActive = new Queue<GameObject>();

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
				GetChunkPosToSpawn ();
			DeSpawnChunks();
			yield return new WaitForSeconds (waitForSeconds);
		}
	}

	IEnumerator SpawnChunk()
	{
		while (true) {

			if(objectsToSetActive.Count !=0)
			{
				GameObject temp = objectsToSetActive.Dequeue();
				temp.transform.gameObject.SetActive(true);
				temp.GetComponent<ChunkGenerator>().CreateVisualMesh();
				yield return new WaitForSeconds(waitForSeconds/20f);
			}

			if (objectsToSpawn.Count != 0) 
			{
				GameObject temp = (GameObject)Instantiate (chunkFab, objectsToSpawn.Dequeue(), Quaternion.identity);
				temp.GetComponent<ChunkProperties>().gradiantValue += Mathf.Abs(5 *(Mathf.FloorToInt(temp.transform.position.x /100) + Mathf.FloorToInt(temp.transform.position.z /100)));
				if(temp.transform.position.x % 100 == 0 || temp.transform.position.z % 100 == 0)
				{
					temp.GetComponent<ChunkProperties>().betweemBiomes = true;
					temp.GetComponent<ChunkProperties>().hasSetSides = false;
					if(temp.transform.position.x % 100 == 0 && temp.transform.position.z % 100 == 0)
					{
						temp.GetComponent<ChunkProperties>().corner = true;
					}
					else if(temp.transform.position.x % 100 == 0 )
						temp.GetComponent<ChunkProperties>().xAxis = true;
					else
						temp.GetComponent<ChunkProperties>().xAxis = false;
				}
				chunks.Add(temp.transform);
			}
			yield return new WaitForSeconds (waitForSeconds/10f);
		}
	}

	void GetChunkPosToSpawn()
	{
		for (int x = Mathf.RoundToInt(player.transform.position.x - instantiateDistance); x <= Mathf.RoundToInt(player.transform.position.x + instantiateDistance); x += chunkLength) 
		{
			for (int z = Mathf.RoundToInt(player.transform.position.z - instantiateDistance); z <= Mathf.RoundToInt(player.transform.position.z + instantiateDistance); z += chunkLength) 
			{
				Vector3 tempPos = new Vector3( (float)x, 0f, (float)z);
				tempPos.x = Mathf.FloorToInt(tempPos.x / (float)chunkLength) * chunkLength;
				tempPos.z = Mathf.FloorToInt(tempPos.z / (float)chunkLength) * chunkLength;
				int index = FindChunk(tempPos);
				if(index == -1)
					objectsToSpawn.Enqueue(tempPos);
				else if	(!chunks[index].gameObject.activeInHierarchy)
					objectsToSetActive.Enqueue(chunks[index].gameObject);
			}
		}
	}

	void DeSpawnChunks()
	{
		foreach (Transform temp in chunks) 
		{
			if(Vector2.Distance( new Vector2(player.transform.position.x, player.transform.position.z) , new Vector2(temp.position.x, temp.position.z)) > deSpawnDistance)
				temp.gameObject.SetActive(false);
		}
	}

	public int FindChunk(Vector3 pos)
	{
		for (int i = 0; i < chunks.Count; i++) 
		{
			Vector3 chunkPos = chunks[i].position;
			if(chunkPos == pos)
				return i;
		}
		return -1;
	}
}
