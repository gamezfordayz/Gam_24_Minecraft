using UnityEngine;
using System.Collections;

public class ChunkProperties : MonoBehaviour {

	[Range (0, 200)]
	public int gradiantValue = 0;
	public int stoneHeightMin = 12;
	public int stoneHeightMax = 15;
	public bool betweemBiomes = false;
	public bool xAxis = true;
	public bool corner = false;
	public bool reversed = true;
	public bool lower = false;
	public int boimeType = 0;

	public bool hasSetSides = false;
	public int lowerGradiant = -1, upperGradiant = -1 , bottomRight = -1, bottomLeft = -1, topRight = -1 , topLeft = -1;


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
		foreach (Transform temp in World.currentWorld.chunks)
		{
			ChunkGenerator chunkGen = transform.GetComponent<ChunkGenerator>();
			if(corner != true)
			{
				if(xAxis)
				{
					lowerGradiant = World.currentWorld.chunks[chunkGen.GetChunkCoords(new Vector2(-1,0))].GetComponent<ChunkProperties>().gradiantValue;
					upperGradiant = World.currentWorld.chunks[chunkGen.GetChunkCoords(new Vector2(1,0))].GetComponent<ChunkProperties>().gradiantValue;
				}
				else
				{
					lowerGradiant = World.currentWorld.chunks[chunkGen.GetChunkCoords(new Vector2(0,-1))].GetComponent<ChunkProperties>().gradiantValue;
					upperGradiant = World.currentWorld.chunks[chunkGen.GetChunkCoords(new Vector2(0,1))].GetComponent<ChunkProperties>().gradiantValue;
				}
			}
			else
			{

				bottomLeft = World.currentWorld.chunks[chunkGen.GetChunkCoords(new Vector2(-1,-1))].GetComponent<ChunkProperties>().gradiantValue;
				bottomRight = World.currentWorld.chunks[chunkGen.GetChunkCoords(new Vector2(1,-1))].GetComponent<ChunkProperties>().gradiantValue;
				topLeft = World.currentWorld.chunks[chunkGen.GetChunkCoords(new Vector2(-1,1))].GetComponent<ChunkProperties>().gradiantValue;
				topRight = World.currentWorld.chunks[chunkGen.GetChunkCoords(new Vector2(1,1))].GetComponent<ChunkProperties>().gradiantValue;
			}
		}
		if ( !corner && lowerGradiant != - 1 && upperGradiant != -1)
		{
			hasSetSides = true;
			gameObject.GetComponent<ChunkGenerator>().CreateChunk();
		}
		else if(corner && bottomLeft != -1 && bottomRight != -1 && topLeft != -1 && topRight != -1)
		{
			hasSetSides = true;
			gameObject.GetComponent<ChunkGenerator>().CreateChunk();
		}
	}
}
