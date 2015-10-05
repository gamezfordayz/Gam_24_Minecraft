using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeProperties : MonoBehaviour
{
	public static CubeProperties cubeProperties;
	public Vector2 uvSize;
	public GameObject[] possibleDrops;
	public Sprite[] possibleSprites;
	public GameObject defaultGameObject;
	public Sprite defaultSprite;
	public Vector2 defaultUvCorner;
	public enum itemIDs
	{
		air, stone, dirt, grass, cobble, sand, snowGrass ,wood, plank, coal, iron
		, gold, diamond, cactus, leaves , craftingTable , furnace
	};
	public enum itemType
	{
		block , weapon, equiptment , pickaxe , axe, crafting 
	};
	public Dictionary<itemIDs , cubeProps> itemDict = new Dictionary<itemIDs, cubeProps> ();

	public struct cubeProps
	{
		public Vector2[] uvIndexes;
		public itemIDs itemID;
		public itemType itemType;
		public bool dropGameObject;
		public GameObject gameObjectToDrop;
		public Sprite inventorySprite;
		public bool stackable;

		// the inventory srite can be the same as the item id index along with what to drop
		//public Sprite inventorySprite;
		//public int indexOfItemToDrop;
		//public int numberToDrop;
	}

	void Awake()
	{
		uvSize = new Vector2( .04166666666666f , 0.1428571f);
		InitializeStructs ();
		cubeProperties = this;
	}

	void InitializeItemStruct(cubeProps temp ,itemIDs id, itemType type,  bool stackable,  bool dropGameObject, GameObject objectToDrop , Sprite sprite, float topX , float topY)
	{
		temp.itemID = id;
		temp.itemType = type;
		temp.stackable = stackable;
		temp.dropGameObject = dropGameObject;
		temp.gameObjectToDrop = objectToDrop;
		temp.inventorySprite = sprite;
		temp.uvIndexes = new Vector2[]{new Vector2 (topX, topY) , new Vector2 (topX, topY) , new Vector2 (topX, topY)};
		itemDict.Add (temp.itemID, temp);
	}

	void InitializeItemStruct(cubeProps temp ,itemIDs id, itemType type,  bool stackable,  bool dropGameObject, GameObject objectToDrop , Sprite sprite, float topX , float topY , float sideX , float sideY , float bottomX , float bottomY)
	{
		temp.itemID = id;
		temp.itemType = type;
		temp.stackable = stackable;
		temp.dropGameObject = dropGameObject;
		temp.gameObjectToDrop = objectToDrop;
		temp.inventorySprite = sprite;
		temp.uvIndexes = new Vector2[]{new Vector2 (topX, topY) , new Vector2 (sideX, sideY) , new Vector2 (bottomX, bottomY)};
		itemDict.Add (temp.itemID, temp);
	}



	void InitializeStructs()
	{
		cubeProps stone = new cubeProps();
		InitializeItemStruct (stone, itemIDs.stone, itemType.block, true, true , possibleDrops[0] , defaultSprite, 1f, 6f);

		cubeProps dirt = new cubeProps();
		InitializeItemStruct (dirt, itemIDs.dirt,itemType.block, true, true , possibleDrops[1] , defaultSprite, 2f, 6f);

		cubeProps grass = new cubeProps();
		InitializeItemStruct (grass, itemIDs.grass, itemType.block, true, true , possibleDrops[1] , defaultSprite, 0f, 6f, 3f, 6f, 2f, 6f);

		cubeProps cobble = new cubeProps();
		InitializeItemStruct (cobble, itemIDs.cobble, itemType.block, true, true , possibleDrops[0] , defaultSprite, 0f, 5f);

		cubeProps sand = new cubeProps();
		InitializeItemStruct (sand, itemIDs.sand,itemType.block, true, true , defaultGameObject , defaultSprite, 2f, 5f);

		cubeProps snowGrass = new cubeProps ();
		InitializeItemStruct (snowGrass , itemIDs.snowGrass ,itemType.block, true, true , defaultGameObject , defaultSprite, 2f,2f,4f,2f,2f,6f);

		cubeProps plank = new cubeProps();
		InitializeItemStruct (plank, itemIDs.plank,itemType.block, true, true , defaultGameObject , defaultSprite, 4f, 6f);

		cubeProps wood = new cubeProps();
		InitializeItemStruct (wood, itemIDs.wood, itemType.block, true, true , defaultGameObject , defaultSprite, 5f, 5f, 4f, 5f, 5f, 5f);

		cubeProps iron = new cubeProps();
		InitializeItemStruct (iron, itemIDs.iron,itemType.block, true, true , defaultGameObject , defaultSprite, 1f, 4f);

		cubeProps gold = new cubeProps();
		InitializeItemStruct (gold , itemIDs.gold,itemType.block, true, true , defaultGameObject , defaultSprite, 0f, 4f);

		cubeProps diamond = new cubeProps();
		InitializeItemStruct (diamond , itemIDs.diamond,itemType.block, true, false , defaultGameObject , defaultSprite, 2f,3f);

		cubeProps cactus = new cubeProps();
		InitializeItemStruct (cactus , itemIDs.cactus ,itemType.block, true, true , defaultGameObject , defaultSprite, 5f, 2f, 6f, 2f, 7f, 2f);


		cubeProps leaves = new cubeProps();
		InitializeItemStruct (leaves , itemIDs.leaves, itemType.block, true, false , defaultGameObject , defaultSprite, 4f, 3f);

		cubeProps coal = new cubeProps();
		InitializeItemStruct (coal , itemIDs.coal, itemType.block, true, false , defaultGameObject , defaultSprite, 2f ,4f);

		cubeProps craftingTable = new cubeProps ();
		InitializeItemStruct (craftingTable , itemIDs.craftingTable, itemType.crafting, false, true , defaultGameObject , defaultSprite, 11f, 4f , 11f , 3f ,11f, 4f );
	}

}
