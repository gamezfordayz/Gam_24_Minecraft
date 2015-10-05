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

	public bool inventoryIsFull = false;
	//public GameObject mainInventory;
	//public GameObject activeInventory;
	// Use this for initialization
	void Awake () 
	{
		inventoryManager = this;
	}
	

//	void SortInventorySlots()
//	{
//		for(int i = 0; i < activeInventorySlots.Length; i++)
//		{
//			activeInventorySlots[i] = activeInventory.transform.GetChild(i).GetComponent<SlotProperties>();
//		}
//	}

	public SlotProperties GetNextAvailableSlot(int itemID)
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

	public SlotProperties CheckIfIsInInvenory(int itemID)
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

	public bool AddItemToInventory(int itemID)
	{
		if(CubeProperties.cubeProperties.itemDict[(CubeProperties.itemIDs)itemID].stackable)
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
