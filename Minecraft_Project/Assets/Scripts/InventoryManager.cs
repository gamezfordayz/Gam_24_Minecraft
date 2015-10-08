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

	void Awake () 
	{
		inventoryManager = this;
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
				Debug.Log("Swaped Was Called");
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
			if(temp.itemID == itemID && !temp.atMaxStacks)
				return temp;
		}
		foreach (SlotProperties temp in activeInventorySlots)
		{
			if(temp.itemID == itemID  && !temp.atMaxStacks)
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
	// BUG HERE ... NUMBER ISNT BEING REDUCED
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
