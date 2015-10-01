using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeProperties : MonoBehaviour
{
	public static CubeProperties cubeType;
	public Vector2 uvSize = new Vector2(.166667f , .25f);
	public enum cubeIndexes
	{
		air, stone, dirt, grass, cobble, sand, wood, plank, mossStone, iron
		, gold, diamond, cactus, stoneBrick, leaves, obsidian , coal
	};
	public Dictionary<cubeIndexes , cubeProps> cubeDict = new Dictionary<cubeIndexes, cubeProps> ();

	public struct cubeProps
	{
		public Vector2[] uvIndexes;
		public cubeIndexes cubeType;
		//public int indexOfItemToDrop;
		//public int numberToDrop;
	}

	void Awake()
	{
		InitializeStructs ();
		cubeType = this;
	}

	void InitializeBlockType(cubeProps temp ,cubeIndexes type , float topX, float topY, float sideX, float sideY, float bottomX, float bottomY)
	{
		temp.cubeType = type;
		temp.uvIndexes = new Vector2[]{new Vector2(topX, topY) , new Vector2(sideX, sideY) , new Vector2(bottomX, bottomY)} ;
		cubeDict.Add (temp.cubeType, temp);
	}

	void InitializeStructs()
	{
		cubeProps stone = new cubeProps();
		InitializeBlockType (stone, cubeIndexes.stone, 5f, 1f, 5f, 1f, 5f ,1f);
		cubeProps dirt = new cubeProps();
		InitializeBlockType (dirt, cubeIndexes.dirt, 1f, 3f, 1f, 3f, 1f, 3f);
		cubeProps grass = new cubeProps();
		InitializeBlockType (grass, cubeIndexes.grass, 0f, 3f, 0f, 2f, 1f, 3f);
		cubeProps cobble = new cubeProps();
		InitializeBlockType (cobble, cubeIndexes.cobble, 1f, 2f, 1f, 2f, 1f, 2f);
		cubeProps sand = new cubeProps();
		InitializeBlockType (sand, cubeIndexes.sand, 2f, 2f, 2f, 2f, 2f, 2f);
		cubeProps plank = new cubeProps();
		InitializeBlockType (plank, cubeIndexes.plank, 4f, 3f, 4f, 3f, 4f, 3f);
		cubeProps wood = new cubeProps();
		InitializeBlockType (wood, cubeIndexes.wood, 3f, 3f, 2f, 3f, 3f, 3f);
//		blockProps mossStone = new blockProps();
//		InitializeBlockType ();
//		blockProps iron = new blockProps();
//		InitializeBlockType ();
//		blockProps gold = new blockProps();
//		InitializeBlockType ();
//		blockProps diamond = new blockProps();
//		InitializeBlockType ();
//		blockProps cactus = new blockProps();
//		InitializeBlockType ();
//		blockProps stoneBrick = new blockProps();
//		InitializeBlockType ();
//		blockProps leaves = new blockProps();
//		InitializeBlockType ();
//		blockProps obsidian = new blockProps();
//		InitializeBlockType ();
//		blockProps coal = new blockProps();
//		InitializeBlockType ();

	}

}
