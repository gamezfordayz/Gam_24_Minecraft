using UnityEngine;
using System.Collections;

public class ChunkProperties : MonoBehaviour {

	[Range (0, 200)]
	public int gradiantValue = 0;
	public int stoneHeightMin = 12;
	public int stoneHeightMax = 15;
	public bool betweemBiomes = false;
	public bool hasSetBiomeInfo = false;
	public BiomeTypes.biomeType biome;
	public CubeProperties.itemIDs defaultCube;
	public CubeProperties.itemIDs defaultGrass;
	public CubeProperties.itemIDs defaultTree;
	public bool xAxis = true;
	public bool corner = false;
	public int boimeType = 0;

	public bool hasSetSides = false;
	public int lowerGradiant = -1, upperGradiant = -1 , bottomRight = -1, bottomLeft = -1, topRight = -1 , topLeft = -1;


	public const int PLAINS = 1 , HILLS = 2, MOUNTAINS = 3, DESERT = 4;

	void Awake()
	{
		hasSetSides = ! betweemBiomes;
		StartCoroutine (FindNeighborChunk ());
	}
	

	void Update()
	{
		if(!hasSetBiomeInfo)
		{
			gradiantValue = BiomeTypes.biomeGenerator.biomeTypeDict [(BiomeTypes.biomeType)biome].gradiant;
			defaultCube = BiomeTypes.biomeGenerator.biomeTypeDict [(BiomeTypes.biomeType)biome].defaultCube;
			defaultGrass = BiomeTypes.biomeGenerator.biomeTypeDict [(BiomeTypes.biomeType)biome].defaultGrass;
			defaultTree = BiomeTypes.biomeGenerator.biomeTypeDict [(BiomeTypes.biomeType)biome].defaultTree;
			hasSetBiomeInfo = true;
		}

	}

	IEnumerator FindNeighborChunk()
	{
		yield return new WaitForSeconds (1f);
		while (!hasSetSides) 
		{
			foreach (Transform temp in World.currentWorld.chunks) {
				ChunkGenerator chunkGen = transform.GetComponent<ChunkGenerator> ();
				if (xAxis) {
					if (temp.position == chunkGen.GetChunkCoords (new Vector2 (-1, 0))) {
						lowerGradiant = temp.GetComponent<ChunkProperties> ().gradiantValue;
					}
					if (temp.position == chunkGen.GetChunkCoords (new Vector2 (1, 0))) {
						upperGradiant = temp.GetComponent<ChunkProperties> ().gradiantValue;
					}
				} else {
					if (temp.position == chunkGen.GetChunkCoords (new Vector2 (0, -1))) {
						lowerGradiant = temp.GetComponent<ChunkProperties> ().gradiantValue;
					}
					if (temp.position == chunkGen.GetChunkCoords (new Vector2 (0, 1))) {
						upperGradiant = temp.GetComponent<ChunkProperties> ().gradiantValue;
					}
				}

				if (corner) {
					if (temp.position == chunkGen.GetChunkCoords (new Vector2 (-1, -1))) {
						bottomLeft = temp.GetComponent<ChunkProperties> ().gradiantValue;
					}
					if (temp.position == chunkGen.GetChunkCoords (new Vector2 (1, -1))) {
						bottomRight = temp.GetComponent<ChunkProperties> ().gradiantValue;
					}
					if (temp.position == chunkGen.GetChunkCoords (new Vector2 (-1, 1))) {
						topLeft = temp.GetComponent<ChunkProperties> ().gradiantValue;
					}
					if (temp.position == chunkGen.GetChunkCoords (new Vector2 (1, 1))) {
						topRight = temp.GetComponent<ChunkProperties> ().gradiantValue;
					}

				}
			}
			if (!corner && lowerGradiant != - 1 && upperGradiant != -1) {
				hasSetSides = true;
				StartCoroutine (gameObject.GetComponent<ChunkGenerator> ().CreateChunk ());
			} else if (corner && bottomLeft != -1 && bottomRight != -1 && topLeft != -1 && topRight != -1 && lowerGradiant != -1 && upperGradiant != -1) {
				hasSetSides = true;
				StartCoroutine (gameObject.GetComponent<ChunkGenerator> ().CreateChunk ());
			}
			yield return new WaitForSeconds(.5f);
		}
	}
}
