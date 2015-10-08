using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour {
	public static InventoryManager inventoryManager;
	public SlotProperties[] mainInventorySlots = new SlotProperties[27];
	public SlotProperties[] activeInventorySlots = new SlotProperties[9]; 
	public SlotProperties[] craftingInventorySlots = new SlotProperties[4];
	public SlotProperties outputInventorySlots;
	public SlotProperties[] craftingTableInventorySlots = new SlotProperties[9];
	public SlotProperties[] furnaceInventorySlots = new SlotProperties[9];

	public AudioClip pickUpSound;

	Crafting crafting;

	public GameObject craftingButton;
	public GameObject inventoryCraft;

	public bool inventoryIsFull = false;
	public bool masterCrafter = false;
	//public GameObject mainInventory;
	//public GameObject activeInventory;
	// Use this for initialization
	void Awake () 
	{
		inventoryManager = this;
		crafting = GetComponent<Crafting> ();
	}

	void Update()
	{
		if (!inventoryCraft.active)
			CallCrafting (craftingTableInventorySlots, outputInventorySlots);
		else
			CallCrafting (craftingInventorySlots, outputInventorySlots);

		if (masterCrafter)
			craftingButton.SetActive (true);
		else
			craftingButton.SetActive (false);
	}

	public void CallCrafting(SlotProperties[] craftingInventorySlots, SlotProperties outputInventorySlots)
	{
		//pass in input
		string inputString = null;
		int output = 0;
		int numberOfOutput = 1;

		for(int i=0; i<craftingInventorySlots.Length; i++)
		{
			inputString += ((int)craftingInventorySlots[i].itemID).ToString() + " ";
			if (craftingInventorySlots.Length < 9)
			{
				if (i == 1)
					inputString += "0 ";
				if (i == 3)
					inputString += "0 0 0 0 ";
			}
		}

		if (!masterCrafter)
		{
			output = crafting.GetOutput (inputString);
			numberOfOutput = crafting.GetOutputNumber (inputString);

			outputInventorySlots.itemID = (CubeProperties.itemIDs)output;
			outputInventorySlots.numberOfItem = (byte)numberOfOutput;
		}
	}

	public void SaveXML()
	{
		if (!inventoryCraft.active)
			SaveXMLButton (craftingTableInventorySlots, outputInventorySlots);
		else
			SaveXMLButton (craftingInventorySlots, outputInventorySlots);
	}

	void SaveXMLButton (SlotProperties[] craftingInventorySlots, SlotProperties outputInventorySlots)
	{
		string craftingIDs = null;
		for(int i=0; i<craftingInventorySlots.Length; i++)
		{
			craftingIDs += ((int)craftingInventorySlots[i].itemID).ToString() + " ";

			if (craftingInventorySlots.Length < 9)
			{
				if (i == 1)
					craftingIDs += "0 ";
				if (i == 3)
					craftingIDs += "0 0 0 0 ";
			}
		}
		craftingIDs += "," + ((int)outputInventorySlots.itemID).ToString() + ",";
		craftingIDs += outputInventorySlots.numberOfItem + "," + "\n";

		crafting.SaveXML (craftingIDs);
	}

//	void SortInventorySlots()
//	{
//		for(int i = 0; i < activeInventorySlots.Length; i++)
//		{
//			activeInventorySlots[i] = activeInventory.transform.GetChild(i).GetComponent<SlotProperties>();
//		}
//	}

	public SlotProperties GetNextAvailableSlot(CubeProperties.itemIDs itemID)
	{
		foreach( SlotProperties temp in mainInventorySlots)
		{
			if(temp.itemID == 0)
				return temp;
		}
		foreach (SlotProperties temp in activeInventorySlots)
		{
			if(temp.itemID == 0)
				return temp;
		}
		return null;
	}

	public SlotProperties CheckIfIsInInvenory(CubeProperties.itemIDs itemID)
	{
		foreach( SlotProperties temp in mainInventorySlots)
		{
			if(temp.itemID == itemID)
				return temp;
		}
		foreach (SlotProperties temp in activeInventorySlots)
		{
			if(temp.itemID == itemID)
				return temp;
		}
		return null;
	}

	public bool AddItemToInventory(CubeProperties.itemIDs itemID)
	{
		if(CubeProperties.cubeProperties.itemDict[itemID].stackable)
		{
			SlotProperties temp = CheckIfIsInInvenory( itemID);
			if( temp != null)
			{
				temp.numberOfItem += 1;
				return true;
			}
		}
		SlotProperties temp1 = GetNextAvailableSlot (itemID);
		if(temp1 != null)
		{
			temp1.itemID = itemID;
			return true;
		}
		return false;
	}

	public void PlayPickUpNoise()
	{
		AudioSource.PlayClipAtPoint (pickUpSound , Camera.main.transform.position);
	}

}
