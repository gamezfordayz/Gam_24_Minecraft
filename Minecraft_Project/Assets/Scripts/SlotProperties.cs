﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//public class SlotProperties : MonoBehaviour , IPointerEnterHandler   {IPointerEnterHandlerIsdsdsdsdsd\\\\\\\\\\\\\sdsdsdsdsdsd\sdsds\sdsdsd\s,,,,, s,,sdsdsdsdsds,
public class SlotProperties : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler , IPointerClickHandler 
{
	public CubeProperties.itemIDs itemID = 0;
	public int numberOfItem = 0;
	public bool isStackable;
	byte maxStack = 64;
	public bool atMaxStacks= false;
	public Image image;
	RectTransform rectTransform;
	public Sprite invisSprite;
	public Sprite objectSprite;
	public Color mouseOverColor;
	Color myColor;
	public Text textObj;


	// Use this for initialization
	void Start () 
	{
		numberOfItem = 0;
		if (gameObject.tag != "Equiptment") 
			textObj = transform.FindChild ("ItemNumber").GetComponent<Text>();
		if (textObj != null)
			textObj.resizeTextMaxSize = 14;
		myColor = gameObject.GetComponent<Image> ().color;
		image = transform.GetChild(0).GetComponent<Image>();
		rectTransform = transform.FindChild("ItemIcon").GetComponent<RectTransform> ();
		rectTransform.anchorMax = new Vector2(.92f , .92f);
		rectTransform.anchorMin = new Vector2(.08f , .08f);
		rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;
		image.sprite = invisSprite;
		transform.GetChild (0).gameObject.SetActive (true);
	}

	void Update()
	{
		if(numberOfItem == 0)
		{
			itemID = 0;
			image.sprite = invisSprite;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		gameObject.GetComponent<Image> ().color = mouseOverColor;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		gameObject.GetComponent<Image> ().color = myColor;
	}

	public void OnPointerClick(PointerEventData data)
	{
		if (itemID != 0) 
		{
			if (gameObject.tag == "Crafting") 
			{
				if (data.button == PointerEventData.InputButton.Right) 
				{
					if (InventoryManager.inventoryManager.AddItemToInventory (itemID, numberOfItem, this))
						UpdateSlot (0, 0);
				}
			}
			else if(gameObject.tag == "Output")
			{
				if(OpenMenus.openMenu.inventoryCrafting[0].activeInHierarchy)
				{
					InventoryManager.inventoryManager.AddItemToInventory (itemID, numberOfItem, this);
					foreach (SlotProperties slot in InventoryManager.inventoryManager.craftingInventorySlots)
					{
						if (slot.itemID != 0)
						{
							slot.UpdateNumberOfItem(-1);
						}
					}
				}
				else if(OpenMenus.openMenu.furnaceCrafting.activeInHierarchy)
				{
					InventoryManager.inventoryManager.AddItemToInventory (itemID, numberOfItem, this);
					foreach (SlotProperties slot in InventoryManager.inventoryManager.furnaceInventorySlots)
					{
						if (slot.itemID != 0)
						{
							slot.UpdateNumberOfItem(-1);
						}
					}
				}
				else
				{
					InventoryManager.inventoryManager.AddItemToInventory (itemID, numberOfItem, this);
					foreach (SlotProperties slot in InventoryManager.inventoryManager.craftingTableInventorySlots)
					{
						if (slot.itemID != 0)
						{
							slot.UpdateNumberOfItem(-1);
						}
					}
				}
			}
		}
	}


	public void UpdateSlot(CubeProperties.itemIDs item )
	{
		if(item == 0)
		{
			image.sprite = invisSprite;
			numberOfItem = 0;
			UpdateText();
		}
		else {
			itemID = item;
			isStackable =  CubeProperties.cubeProperties.itemDict[itemID].stackable;
			objectSprite = CubeProperties.cubeProperties.itemDict[itemID].inventorySprite;
			image.sprite = objectSprite;
			numberOfItem = 1;
			UpdateText ();
			UpdateMaxStacks ();
		}
	}

	public void UpdateSlot(CubeProperties.itemIDs item , int number)
	{
		if (item == 0) {
			image.sprite = invisSprite;
			numberOfItem = 0;
		}
		else 
		{
			itemID = item;
			isStackable = CubeProperties.cubeProperties.itemDict [itemID].stackable;
			objectSprite = CubeProperties.cubeProperties.itemDict [itemID].inventorySprite;
			image.sprite = objectSprite;
			numberOfItem = number;
		}
		UpdateText ();
		UpdateMaxStacks ();
	}

	public int UpdateNumberOfItem(int number)
	{
		if(numberOfItem + number <= maxStack)
		{
			numberOfItem += number;
			UpdateText ();
			UpdateMaxStacks ();
			return 0;
		}
		else 
		{
			int remainder = ((numberOfItem+number) - maxStack);
			numberOfItem = maxStack;
			UpdateText ();
			UpdateMaxStacks ();
			return (remainder);
		}

	}

	void UpdateText()
	{
		if (gameObject.tag != "Equiptment") 
		{
			if (numberOfItem > 1)
				textObj.text = numberOfItem.ToString ();
			else
				textObj.text = "";
		}
	}

	void UpdateMaxStacks()
	{
		if(numberOfItem == maxStack)
		{
			atMaxStacks = true;
		}
		else 
		{
			atMaxStacks = false;
		}
	}

}
