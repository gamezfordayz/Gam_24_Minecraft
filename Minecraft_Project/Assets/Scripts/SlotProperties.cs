﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//public class SlotProperties : MonoBehaviour , IPointerEnterHandler   {IPointerEnterHandlerIsdsdsdsdsd\\\\\\\\\\\\\sdsdsdsdsdsd\sdsds\sdsdsd\s,,,,, s,,sdsdsdsdsds,
public class SlotProperties : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler , IPointerDownHandler , IPointerUpHandler
{
	public CubeProperties.itemIDs itemID = 0;
	public byte numberOfItem = 0;
	public bool isStackable;
	byte maxStack = 64;
	public bool atMaxStacks= false;
	Image image;
	RectTransform rectTransform;
	public Sprite invisSprite;
	public Sprite objectSprite;
	public Color mouseOverColor;
	Color myColor;
	Text textObj;
	//CubeProperties cubeList;


	// Use this for initialization
	void Start () 
	{
		numberOfItem = 1;
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
	
	// Update is called once per frame
	void Update () {
		if(itemID == 0 && image.sprite.name != invisSprite.name)
		{
			image.sprite = invisSprite;
		}
		else if(itemID != 0)
		{
			objectSprite = CubeProperties.cubeProperties.itemDict[(CubeProperties.itemIDs)itemID].inventorySprite;
			image.sprite = objectSprite;
		}
		if (gameObject.tag != "Equiptment") 
		{
			if (numberOfItem > 1)
				textObj.text = numberOfItem.ToString ();
			else
				textObj.text = "";
		}
		if(numberOfItem == maxStack)
		{
			atMaxStacks = true;
		}
		else {
			atMaxStacks = false;
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

	public void OnPointerUp(PointerEventData eventData)
	{
		//on up set the second swap index = this index
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		
	}

	public void UpdateItemSprite(int itemID)
	{
		// set my image sprite based off the item id and a Dictionary and then get that items sprite
	}

}
