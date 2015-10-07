using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BiomeTypes : MonoBehaviour
{
	public static BiomeTypes biomeGenerator;
	public int biomeSize = 200;
	public Dictionary <biomeType , biomeProperties> biomeTypeDict = new Dictionary< biomeType , biomeProperties>();
	public List<createBiome> biomeList = new List<createBiome>();
	public enum biomeType
	{
		desert , plains , mountains , snowyMountains , diamond
	};

	public struct biomeProperties {
		public CubeProperties.itemIDs defaultCube;
		public CubeProperties.itemIDs defaultGrass;
		public CubeProperties.itemIDs defaultTree;
		public int gradiant;
	}

	public struct createBiome
	{
		public Vector3 startCorner;
		public Vector3 endCorner;
		public biomeType type;
		public createBiome( Vector3 startPos, Vector3 endPos , biomeType type)
		{
			this.startCorner = startPos;
			this.endCorner = endPos;
			this.type = type;
		}
	}

	void Awake()
	{
		biomeGenerator = this;

		biomeProperties desertBiome = new biomeProperties();
		desertBiome.defaultCube = CubeProperties.itemIDs.sand;
		desertBiome.defaultGrass = CubeProperties.itemIDs.sand;
		desertBiome.defaultTree = CubeProperties.itemIDs.cactus;
		desertBiome.gradiant = 4;
		biomeTypeDict.Add (biomeType.desert, desertBiome);

		biomeProperties plainsBiome = new biomeProperties();
		plainsBiome.defaultCube = CubeProperties.itemIDs.dirt;
		plainsBiome.defaultGrass = CubeProperties.itemIDs.grass;
		plainsBiome.defaultTree = CubeProperties.itemIDs.wood;
		plainsBiome.gradiant = 10;
		biomeTypeDict.Add (biomeType.plains, plainsBiome);

		biomeProperties mountainBiome = new biomeProperties();
		mountainBiome.defaultCube = CubeProperties.itemIDs.dirt;
		mountainBiome.defaultGrass = CubeProperties.itemIDs.grass;
		mountainBiome.defaultTree = CubeProperties.itemIDs.wood;
		mountainBiome.gradiant = 60;
		biomeTypeDict.Add (biomeType.mountains, mountainBiome);

		biomeProperties snowyMountainBiome = new biomeProperties();
		snowyMountainBiome.defaultCube = CubeProperties.itemIDs.dirt;
		snowyMountainBiome.defaultGrass = CubeProperties.itemIDs.snowGrass;
		snowyMountainBiome.defaultTree = CubeProperties.itemIDs.wood;
		snowyMountainBiome.gradiant = 60;
		biomeTypeDict.Add (biomeType.snowyMountains, snowyMountainBiome);


	}

	public createBiome CheckIfBiomeExists(Vector3 pos)
	{
		foreach (createBiome biome in biomeList)
		{
			if(pos.x >= 0 && pos.z >= 0)
			{
				if(biome.startCorner.x <= pos.x && biome.startCorner.z <= pos.z && biome.endCorner.x >= pos.x && biome.endCorner.z >= pos.z)
				{
					return biome;
				}
			}
			if(pos.x < 0 && pos.z >=0)
			{
				if(biome.startCorner.x >= pos.x && biome.startCorner.z <= pos.z && biome.endCorner.x <= pos.x && biome.endCorner.z >= pos.z)
				{
					return biome;
				}
			}
			if(pos.x < 0 && pos.z < 0)
			{
				if(biome.startCorner.x >= pos.x && biome.startCorner.z >= pos.z && biome.endCorner.x <= pos.x && biome.endCorner.z <= pos.z)
				{
					return biome;
				}
			}
			if(pos.x >= 0 && pos.z < 0)
			{
				if(biome.startCorner.x <= pos.x && biome.startCorner.z >= pos.z && biome.endCorner.x >= pos.x && biome.endCorner.z <= pos.z)
				{
					return biome;
				}
			}
		}
		return CreateNewBiome (pos);
	}

	createBiome CreateNewBiome(Vector3 pos)
	{
		int xSign = (int)Mathf.Sign (pos.x), zSign = (int)Mathf.Sign (pos.z);
		Vector3 start = new Vector3(((int)pos.x/biomeSize) * biomeSize * xSign , 0, ((int)pos.z/biomeSize) * biomeSize * zSign );
		Vector3 end = new Vector3 (start.x + (biomeSize * xSign) , 0f, start.z + (biomeSize * zSign));
		biomeType type = (biomeType)Random.Range (0, 4);
		createBiome temp = new createBiome ( start , end , type);
		//Debug.Log ("Created New For :" + pos + "   start: "+ start + "   end: " + end);

		biomeList.Add (temp);
		//Debug.Log (pos);
		return temp;
	}






}
