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
        air, stone, dirt, grass, cobble, sand, snowGrass, wood, plank, coal, iron
        , gold, diamond, cactus, leaves, craftingTable, furnace, apple, bone, chicken, 
        diamondAxe, diamondBoots, diamondChest, diamondHelm, diamondLegs, diamondPickAxe,
        diamondSword, door, feather, goldBoot, goldChest, goldHelm, goldLegs, goldPickAxe, 
        goldAxe, goldSword, ironAxe, ironLegs, ironChest, ironBoot, ironHelm, ironSword, ironPickAxe,
        leather, porkChop, rawChicken, rawPorkChop, rawSteak, spiderEye, steak, sticks,
        stoneAxe, stonePickAxe, stoneSword, stringThing, woodAxe, woodPickAxe, woodSword,
        zombieSkin , ironOre , goldOre, diamondOre , coalOre , bedrock
    };
    public enum itemType
    {
        block, weapon, equipment, pickaxe, axe, crafting, food
    };
    public Dictionary<itemIDs, cubeProps> itemDict = new Dictionary<itemIDs, cubeProps>();

    public struct cubeProps
    {
        public Vector2[] uvIndexes;
        public itemIDs itemID;
        public itemType itemType;
        public bool dropGameObject;
        public GameObject gameObjectToDrop;
        public Sprite inventorySprite;
        public bool stackable;

    }

    void Awake()
    {
        uvSize = new Vector2(.04166666666666f, 0.1428571f);
        InitializeStructs();
        cubeProperties = this;
    }

    void InitializeItemStruct(cubeProps temp, itemIDs id, itemType type, bool stackable, bool dropGameObject, GameObject objectToDrop, Sprite sprite, float topX, float topY)
    {
        temp.itemID = id;
        temp.itemType = type;
        temp.stackable = stackable;
        temp.dropGameObject = dropGameObject;
        temp.gameObjectToDrop = objectToDrop;
        temp.inventorySprite = sprite;
        temp.uvIndexes = new Vector2[] { new Vector2(topX, topY), new Vector2(topX, topY), new Vector2(topX, topY) };
        itemDict.Add(temp.itemID, temp);
    }

    void InitializeItemStruct(cubeProps temp, itemIDs id, itemType type, bool stackable, bool dropGameObject, GameObject objectToDrop, Sprite sprite, float topX, float topY, float sideX, float sideY, float bottomX, float bottomY)
    {
        temp.itemID = id;
        temp.itemType = type;
        temp.stackable = stackable;
        temp.dropGameObject = dropGameObject;
        temp.gameObjectToDrop = objectToDrop;
        temp.inventorySprite = sprite;
        temp.uvIndexes = new Vector2[] { new Vector2(topX, topY), new Vector2(sideX, sideY), new Vector2(bottomX, bottomY) };
        itemDict.Add(temp.itemID, temp);
    }



    void InitializeStructs()
    {
        cubeProps stone = new cubeProps();
		InitializeItemStruct(stone, itemIDs.stone, itemType.block, true, true, possibleDrops[0], possibleSprites[47], 1f, 6f);

        cubeProps dirt = new cubeProps();
        InitializeItemStruct(dirt, itemIDs.dirt, itemType.block, true, true, possibleDrops[1], possibleSprites[50], 2f, 6f);

        cubeProps grass = new cubeProps();
		InitializeItemStruct(grass, itemIDs.grass, itemType.block, true, true, possibleDrops[1], possibleSprites[50], 0f, 6f, 3f, 6f, 2f, 6f);

        cubeProps cobble = new cubeProps();
		InitializeItemStruct(cobble, itemIDs.cobble, itemType.block, true, true, possibleDrops[0], possibleSprites[47], 0f, 5f);

        cubeProps sand = new cubeProps();
		InitializeItemStruct(sand, itemIDs.sand, itemType.block, true, true, possibleDrops[54], possibleSprites[54], 2f, 5f);

        cubeProps snowGrass = new cubeProps();
		InitializeItemStruct(snowGrass, itemIDs.snowGrass, itemType.block, true, true, possibleDrops[1], possibleSprites[50], 2f, 2f, 4f, 2f, 2f, 6f);

        cubeProps plank = new cubeProps();
		InitializeItemStruct(plank, itemIDs.plank, itemType.block, true, true, possibleDrops[56], possibleSprites[56], 4f, 6f);

        cubeProps wood = new cubeProps();
		InitializeItemStruct(wood, itemIDs.wood, itemType.block, true, true, possibleDrops[55], possibleSprites[55], 5f, 5f, 4f, 5f, 5f, 5f);

        cubeProps iron = new cubeProps();
        InitializeItemStruct(iron, itemIDs.iron, itemType.crafting, true, true, possibleDrops[23], possibleSprites[22], 0f,0f);

        cubeProps gold = new cubeProps();
        InitializeItemStruct(gold, itemIDs.gold, itemType.crafting, true, true, possibleDrops[15], possibleSprites[14], 0f, 0f);

        cubeProps diamond = new cubeProps();
        InitializeItemStruct(diamond, itemIDs.diamond, itemType.crafting, true, false, possibleDrops[5], possibleSprites[4], 0f,0f);

        cubeProps cactus = new cubeProps();
        InitializeItemStruct(cactus, itemIDs.cactus, itemType.block, true, true, possibleDrops[48], possibleSprites[46], 5f, 2f, 6f, 2f, 7f, 2f);

		cubeProps ironOre = new cubeProps();
		InitializeItemStruct (ironOre, itemIDs.ironOre, itemType.block, true, true, possibleDrops [53], possibleSprites [53], 1f, 4f);

		cubeProps goldOre = new cubeProps();
		InitializeItemStruct (goldOre, itemIDs.goldOre, itemType.block, true, true, possibleDrops [52], possibleSprites [52], 0f, 4f);

		cubeProps diamondOre = new cubeProps();
		InitializeItemStruct (diamondOre, itemIDs.diamondOre, itemType.block, true, true, possibleDrops [5], possibleSprites [4], 2f, 3f);

		cubeProps coalOre = new cubeProps();
		InitializeItemStruct (coalOre, itemIDs.coalOre, itemType.block, true, true, possibleDrops [4], possibleSprites [3], 2f, 4f);

		cubeProps furnace = new cubeProps();
		InitializeItemStruct (furnace, itemIDs.furnace, itemType.block, false, true, possibleDrops [51], possibleSprites [51], 14f,3f, 13f,3f, 15f,3f);

        cubeProps leaves = new cubeProps();
        InitializeItemStruct(leaves, itemIDs.leaves, itemType.block, true, true, possibleDrops[47], defaultSprite, 5f, 3f);

        cubeProps coal = new cubeProps();
        InitializeItemStruct(coal, itemIDs.coal, itemType.crafting, true, true, possibleDrops[4], possibleSprites[3], 0f, 0f);

        cubeProps craftingTable = new cubeProps();
        InitializeItemStruct(craftingTable, itemIDs.craftingTable, itemType.block, false, true, possibleDrops[49], possibleSprites[48], 11f, 4f, 11f, 3f, 11f, 4f);

        cubeProps apple = new cubeProps();
        InitializeItemStruct(apple, itemIDs.apple, itemType.food, true, true, possibleDrops[47], possibleSprites[0] ,  0f, 0f);

        cubeProps bone = new cubeProps();
        InitializeItemStruct(bone, itemIDs.bone, itemType.crafting, true, true, possibleDrops[2], possibleSprites[1], 0f, 0f);

        cubeProps chicken = new cubeProps();
        InitializeItemStruct(chicken, itemIDs.chicken, itemType.food, true, true, possibleDrops[3], possibleSprites[2], 0f, 0f);

        cubeProps diamondAxe = new cubeProps();
        InitializeItemStruct(diamondAxe, itemIDs.diamondAxe, itemType.equipment, false, true, possibleDrops[6], possibleSprites[5], 0f, 0f);

        cubeProps diamondLegs = new cubeProps();
        InitializeItemStruct(diamondLegs, itemIDs.diamondLegs, itemType.equipment, false, true, possibleDrops[10], possibleSprites[9], 0f, 0f);

        cubeProps diamondChest = new cubeProps();
        InitializeItemStruct(diamondChest, itemIDs.diamondChest, itemType.equipment, false, true, possibleDrops[8], possibleSprites[7], 0f, 0f);

        cubeProps diamondHelm = new cubeProps();
        InitializeItemStruct(diamondHelm, itemIDs.diamondHelm, itemType.equipment, false, true, possibleDrops[9], possibleSprites[8], 0f, 0f);

        cubeProps diamondBoots = new cubeProps();
        InitializeItemStruct(diamondBoots, itemIDs.diamondBoots, itemType.equipment, false, true, possibleDrops[7], possibleSprites[6], 0f, 0f);

        cubeProps diamondPickAxe = new cubeProps();
        InitializeItemStruct(diamondPickAxe, itemIDs.diamondPickAxe, itemType.equipment, false, true, possibleDrops[11], possibleSprites[10], 0f, 0f);

        cubeProps diamondSword = new cubeProps();
        InitializeItemStruct(diamondSword, itemIDs.diamondSword, itemType.equipment, false, true, possibleDrops[12], possibleSprites[11], 0f, 0f);

        cubeProps door = new cubeProps();
        InitializeItemStruct(door, itemIDs.door, itemType.crafting, true, true, possibleDrops[13], possibleSprites[12], 0f, 0f);

        cubeProps feather = new cubeProps();
        InitializeItemStruct(feather, itemIDs.feather, itemType.crafting, true, true, possibleDrops[14], possibleSprites[13], 0f, 0f);

        cubeProps goldAxe = new cubeProps();
        InitializeItemStruct(goldAxe, itemIDs.goldAxe, itemType.equipment, false, true, possibleDrops[16], possibleSprites[15], 0f, 0f);

        cubeProps goldLegs = new cubeProps();
        InitializeItemStruct(goldLegs, itemIDs.goldLegs, itemType.equipment, false, true, possibleDrops[20], possibleSprites[19], 0f, 0f);

        cubeProps goldChest = new cubeProps();
        InitializeItemStruct(goldChest, itemIDs.goldChest, itemType.equipment, false, true, possibleDrops[18], possibleSprites[17], 0f, 0f);

        cubeProps goldHelm = new cubeProps();
        InitializeItemStruct(goldHelm, itemIDs.goldHelm, itemType.equipment, false, true, possibleDrops[19], possibleSprites[18], 0f, 0f);

        cubeProps goldBoot = new cubeProps();
        InitializeItemStruct(goldBoot, itemIDs.goldBoot, itemType.equipment, false, true, possibleDrops[17], possibleSprites[16], 0f, 0f);

        cubeProps goldPickAxe = new cubeProps();
        InitializeItemStruct(goldPickAxe, itemIDs.goldPickAxe, itemType.equipment, false, true, possibleDrops[21], possibleSprites[20], 0f, 0f);

        cubeProps goldSword = new cubeProps();
        InitializeItemStruct(goldSword, itemIDs.goldSword, itemType.equipment, false, true, possibleDrops[22], possibleSprites[21], 0f, 0f);

        cubeProps ironAxe = new cubeProps();
        InitializeItemStruct(ironAxe, itemIDs.ironAxe, itemType.equipment, false, true, possibleDrops[24], possibleSprites[23], 0f, 0f);

        cubeProps ironBoot = new cubeProps();
        InitializeItemStruct(ironBoot, itemIDs.ironBoot, itemType.equipment, false, true, possibleDrops[25], possibleSprites[24], 0f, 0f);

        cubeProps ironChest = new cubeProps();
        InitializeItemStruct(ironChest, itemIDs.ironChest, itemType.equipment, false, true, possibleDrops[26], possibleSprites[25], 0f, 0f);

        cubeProps ironHelm = new cubeProps();
        InitializeItemStruct(ironHelm, itemIDs.ironHelm, itemType.equipment, false, true, possibleDrops[27], possibleSprites[26], 0f, 0f);

        cubeProps ironLegs = new cubeProps();
        InitializeItemStruct(ironLegs, itemIDs.ironLegs, itemType.equipment, false, true, possibleDrops[28], possibleSprites[27], 0f, 0f);

        cubeProps ironPickAxe = new cubeProps();
        InitializeItemStruct(ironPickAxe, itemIDs.ironPickAxe, itemType.equipment, false, true, possibleDrops[29], possibleSprites[28], 0f, 0f);

        cubeProps ironSword = new cubeProps();
        InitializeItemStruct(ironSword, itemIDs.ironSword, itemType.equipment, false, true, possibleDrops[30], possibleSprites[29], 0f, 0f);

        cubeProps leather = new cubeProps();
        InitializeItemStruct(leather, itemIDs.leather, itemType.crafting, true, true, possibleDrops[31], possibleSprites[30], 0f, 0f);

        cubeProps porkChop = new cubeProps();
        InitializeItemStruct(porkChop, itemIDs.porkChop, itemType.food, true, true, possibleDrops[32], possibleSprites[31], 0f, 0f);

        cubeProps rawChicken = new cubeProps();
        InitializeItemStruct(rawChicken, itemIDs.rawChicken, itemType.food, true, true, possibleDrops[33], possibleSprites[32], 0f, 0f);

     	cubeProps rawPorkChop = new cubeProps();
        InitializeItemStruct(rawPorkChop, itemIDs.rawPorkChop, itemType.food, true, true, possibleDrops[34], possibleSprites[33], 0f, 0f);

        cubeProps steak = new cubeProps();
        InitializeItemStruct(steak, itemIDs.steak, itemType.food, true, true, possibleDrops[37], possibleSprites[36], 0f, 0f);

        cubeProps rawSteak = new cubeProps();
        InitializeItemStruct(rawSteak, itemIDs.rawSteak, itemType.food, true, true, possibleDrops[35], possibleSprites[34], 0f, 0f);

        cubeProps spiderEye = new cubeProps();
        InitializeItemStruct(spiderEye, itemIDs.spiderEye, itemType.crafting, true, true, possibleDrops[36], possibleSprites[36], 0f, 0f);

        cubeProps sticks = new cubeProps();
        InitializeItemStruct(sticks, itemIDs.sticks, itemType.crafting, true, true, possibleDrops[38], possibleSprites[37], 0f, 0f);

        cubeProps stoneAxe = new cubeProps();
        InitializeItemStruct(stoneAxe, itemIDs.stoneAxe, itemType.equipment, false, true, possibleDrops[39], possibleSprites[38], 0f, 0f);

        cubeProps stonePickAxe = new cubeProps();
        InitializeItemStruct(stonePickAxe, itemIDs.stonePickAxe, itemType.equipment, false, true, possibleDrops[40], possibleSprites[39], 0f, 0f);

        cubeProps stoneSword = new cubeProps();
        InitializeItemStruct(stoneSword, itemIDs.stoneSword, itemType.equipment, false, true, possibleDrops[41], possibleSprites[40], 0f, 0f);
        //StringThing due to string error
        cubeProps stringThing = new cubeProps();
        InitializeItemStruct(stringThing, itemIDs.stringThing, itemType.crafting, true, true, possibleDrops[42], possibleSprites[41], 0f, 0f);

        cubeProps woodAxe = new cubeProps();
        InitializeItemStruct(woodAxe, itemIDs.woodAxe, itemType.equipment, false, true, possibleDrops[43], possibleSprites[42], 0f, 0f);

        cubeProps woodPickAxe = new cubeProps();
        InitializeItemStruct(woodPickAxe, itemIDs.woodPickAxe, itemType.equipment, false, true, possibleDrops[44], possibleSprites[43], 0f, 0f);

        cubeProps woodSword = new cubeProps();
        InitializeItemStruct(woodSword, itemIDs.woodSword, itemType.equipment, false, true, possibleDrops[45], possibleSprites[44], 0f, 0f);

        cubeProps zombieSkin = new cubeProps();
        InitializeItemStruct(zombieSkin, itemIDs.zombieSkin, itemType.food, true, true, possibleDrops[46], possibleSprites[45], 0f, 0f);

		cubeProps bedrock = new cubeProps();
		InitializeItemStruct(bedrock, itemIDs.bedrock, itemType.block, false, false, defaultGameObject, defaultSprite, 1f, 5f);

    }


}
