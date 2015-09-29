using UnityEngine;
using System.Collections;

public class ChunkProperties : MonoBehaviour {

	[Range (0, 300)]
	public int gradiantValue = 0;
	public bool betweemBiomes = false;
	public bool xAxis = true;
	public bool corner = false;
	public bool left = true;
	public bool lower = false;
	public int boimeType = 0;

	public bool hasSetSides = false;
	public int lowerGradiant = -1, upperGradiant = -1;


	public const int PLAINS = 1 , HILLS = 2, MOUNTAINS = 3, DESERT = 4;

	void Awake()
	{
		hasSetSides = ! betweemBiomes;
	}

	void Update()
	{
		if (hasSetSides != true) 
		{
			FindNeighborChunk();
		}
	}

	void FindNeighborChunk()
	{
		foreach (GameObject temp in World.currentWorld.TEMP)
		{
			if(corner != true)
			{
				if(xAxis)
				{
					if(temp.transform.position == new Vector3 (transform.position.x - World.currentWorld.chunkLength , transform.position.y, transform.position.z))
						lowerGradiant = temp.GetComponent<ChunkProperties>().gradiantValue;
					if(temp.transform.position == new Vector3 (transform.position.x + World.currentWorld.chunkLength , transform.position.y, transform.position.z))
						upperGradiant = temp.GetComponent<ChunkProperties>().gradiantValue;
				}
				else
				{
					if(temp.transform.position == new Vector3 (transform.position.x , transform.position.y, transform.position.z - World.currentWorld.chunkLength ))
						lowerGradiant = temp.GetComponent<ChunkProperties>().gradiantValue;
					if(temp.transform.position == new Vector3 (transform.position.x , transform.position.y, transform.position.z + World.currentWorld.chunkLength ))
						upperGradiant = temp.GetComponent<ChunkProperties>().gradiantValue;
				}
			}
			else
			{
				//left , topright: lower = -x -z ,upper = +x +z
				//right, topleft: lower = +x -z ,upper = -x + z

				int first =  World.currentWorld.chunkLength , second = World.currentWorld.chunkLength ;
				if(left)
				{
					first *= -1;	second = first;
				}
				else
					second *= -1;

				if(temp.transform.position == new Vector3 (transform.position.x + first, transform.position.y, transform.position.z + second  ))
					lowerGradiant = temp.GetComponent<ChunkProperties>().gradiantValue;
				first =  World.currentWorld.chunkLength; second = World.currentWorld.chunkLength ;
				if(left != true)
					first *= -1;
				if(temp.transform.position == new Vector3 (transform.position.x + first , transform.position.y, transform.position.z + second))
					upperGradiant = temp.GetComponent<ChunkProperties>().gradiantValue;				
			}
		}
		if (lowerGradiant != - 1 && upperGradiant != -1)
		{
			hasSetSides = true;
			gameObject.GetComponent<ChunkGenerator>().CreateChunk();
		}
	}
}
