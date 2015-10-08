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


	SlotProperties firstSwap;
	public AudioClip pickUpSound;


	Coroutine resetSlot;

	Crafting crafting;
	
	public GameObject craftingButton;
	public GameObject inventoryCraft;

	public bool masterCrafter = false;

	void Awake () 
	{
		inventoryManager = this;
		crafting = GetComponent<Crafting> ();
	}
	
	void Update()
	{
		if (!inventoryCraft.activeInHierarchy)
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
			outputInventorySlots.UpdateSlot((CubeProperties.itemIDs)output , numberOfOutput);
//			outputInventorySlots.itemID = (CubeProperties.itemIDs)output;
//			outputInventorySlots.numberOfItem = (byte)numberOfOutput;
		}
	}
	
	public void SaveXML()
	{
		if (!inventoryCraft.activeInHierarchy)
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
	
	public void AddOneToCrafting(SlotProperties slot)
	{
		slot.UpdateSlot (firstSwap.itemID, 1);
		firstSwap.UpdateNumberOfItem (-1);
	}


	public void  SetFirstSwap( SlotProperties slot)
	{
		firstSwap = slot;
		if(resetSlot == null)
			resetSlot = StartCoroutine (ResetFirstSwap ());
		else {
			StopCoroutine(resetSlot);
			resetSlot= StartCoroutine (ResetFirstSwap());
		}
	}

	public void SwapWithFirstSlot( SlotProperties slot)
	{
		if(firstSwap != null)
		{
			if(firstSwap.itemID != slot.itemID || !firstSwap.isStackable || !slot.isStackable)
			{
				CubeProperties.itemIDs temp = firstSwap.itemID;
				int tempNum = firstSwap.numberOfItem;
				firstSwap.UpdateSlot(slot.itemID , slot.numberOfItem);
				slot.UpdateSlot(temp , tempNum);
			}
			else 
			{
				int numberSlotsLeft = 64 - slot.numberOfItem;
				if(firstSwap.numberOfItem < numberSlotsLeft)
				{
					slot.UpdateNumberOfItem(firstSwap.numberOfItem);
					firstSwap.UpdateSlot(0,0);
				}
				else 
				{
					slot.UpdateNumberOfItem(numberSlotsLeft);
					firstSwap.UpdateNumberOfItem(-numberSlotsLeft);
				}

			}
		}
	}

	IEnumerator ResetFirstSwap()
	{
		yield return new WaitForSeconds (10f);
		firstSwap = null;
	}



	public SlotProperties GetNextAvailableSlot(CubeProperties.itemIDs itemID)
	{

		foreach (SlotProperties temp in activeInventorySlots)
		{
			if(temp.itemID == 0)
				return temp;
		}
		foreach( SlotProperties temp in mainInventorySlots)
		{
			if(temp.itemID == 0)
				return temp;
		}
		return null;
	}

	public SlotProperties CheckIfIsInInvenory(CubeProperties.itemIDs itemID)
	{

		foreach (SlotProperties temp in activeInventorySlots)
		{
			if(temp.itemID == itemID  && !temp.atMaxStacks)
				return temp;
		}
		foreach( SlotProperties temp in mainInventorySlots)
		{
			if(temp.itemID == itemID && !temp.atMaxStacks)
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
				temp.UpdateNumberOfItem(1);
				return true;
			}
		}
		SlotProperties temp1 = GetNextAvailableSlot (itemID);
		if(temp1 != null)
		{
			temp1.UpdateSlot(itemID);
			return true;
		}
		return false;
	}

	public bool AddItemToInventory(CubeProperties.itemIDs itemID , int number , SlotProperties slot)
	{
		if(CubeProperties.cubeProperties.itemDict[itemID].stackable)
		{
			SlotProperties temp = CheckIfIsInInvenory( itemID);
			if( temp != null)
			{
				int remainder = temp.UpdateNumberOfItem(number);
				if(remainder == 0)
				{
					slot.UpdateSlot(0,0);
					return true;
				}
				else
				{
					slot.UpdateSlot(itemID , remainder);
					return AddItemToInventory(itemID ,remainder ,slot );
				}
			}
		}
		SlotProperties temp1 = GetNextAvailableSlot (itemID);
		if(temp1 != null)
		{
			temp1.UpdateSlot(itemID , number);
			return true;
		}
		return false;
	}
	
	public void PlayPickUpNoise()
	{
		AudioSource.PlayClipAtPoint (pickUpSound , Camera.main.transform.position);
	}

}
