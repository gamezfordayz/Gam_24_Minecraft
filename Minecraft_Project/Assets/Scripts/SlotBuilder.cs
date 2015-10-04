using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlotBuilder : MonoBehaviour {

	int rows;
	int columns;
	float widthOffset;
	float heightOffset;
	public float widthBuffer;
	public float heightBuffer;
	public GameObject slots;
	float startingX;
	float startingY;
	public float leftX , rightX , lowerY , upperY; 
	public Vector2 min , max;
	RectTransform rectTransform;
	public enum typeOfSlot {
		Inventory, Crafting, Equiptment , Active, Chest, Output
	};
	public typeOfSlot slotType;
	string selectedTag;

	// Use this for initialization
	void Start () {
		if(slotType == typeOfSlot.Active)
			selectedTag = "Active";
		if(slotType == typeOfSlot.Crafting)
			selectedTag = "Crafting";
		if(slotType == typeOfSlot.Inventory)
			selectedTag = "Inventory";
		if(slotType == typeOfSlot.Equiptment)
			selectedTag = "Equiptment";
		if(slotType == typeOfSlot.Output)
			selectedTag = "Output";
		rectTransform = GetComponent<RectTransform> ();
		GetStartingPosition ();
		GetInventoryRowsAndOffset();
		GetInventoryColumnsAndOffset ();
		BuildInventory ();

		//this.gameObject.SetActive (false);
	}

	void GetStartingPosition(){
		startingX = slots.GetComponent<RectTransform> ().rect.width/2;
		startingY = -(slots.GetComponent<RectTransform> ().rect.height/2);
	}							
								//Buffer v
	// Rows = RectWidth + (Distance Between Each Slot) / SlotWidth + The Buffer
	void GetInventoryRowsAndOffset(){
		float width = rectTransform.rect.width;
		float slotWidth = slots.GetComponent<RectTransform> ().rect.width;
		rows = (int)((width + widthBuffer) / slotWidth + widthBuffer);
		widthOffset = (width - (slotWidth * rows) - ((rows - 1) * widthBuffer))/2;
	}


	void GetInventoryColumnsAndOffset(){
		float height = rectTransform.rect.height;												
		float slotHeight = slots.GetComponent<RectTransform> ().rect.height;
		columns = (int)((height + widthBuffer) / slotHeight + heightBuffer);
		heightOffset = (height - (slotHeight * columns) - ((columns - 1) * heightBuffer))/2;
	}

	void BuildInventory(){
		// Get Actual starting vaulues
		float calculatedX = startingX + widthOffset;
		float calculatedY = startingY - heightOffset;
		//initialize
		for (int i =0; i < columns; i++) {
			for(int j = 0; j < rows; j++){
				GameObject slot = (GameObject) Instantiate(slots);	
				slot.tag = selectedTag;
				slot.transform.SetParent(this.gameObject.transform, false);			//parent to inventory obj
				slot.GetComponent<RectTransform>().anchoredPosition = new Vector3(calculatedX,calculatedY,0);
				SetAnchors(i,j);
				slot.GetComponent<RectTransform>().anchorMax = new Vector2(rightX , upperY);
				slot.GetComponent<RectTransform>().anchorMin = new Vector2(leftX , lowerY);
				slot.GetComponent<RectTransform>().offsetMin = Vector2.zero;
				slot.GetComponent<RectTransform>().offsetMax = Vector2.zero;
				//slot.tag
				slot.name = "Slot " + (i+1).ToString() + "." + (j +1).ToString();	//change name 
				calculatedX += (startingX *2) + widthBuffer;	//Distance between each slot horizontally
				if(j == rows - 1){													
					calculatedX = startingX + widthOffset;							// Reset
					calculatedY += (startingY * 2) - heightBuffer;					// Move Y down
					
				}
			}		
		}
	}
	// left x coor width offset + j* slot width + buffer	 /  width
	// right x coor width offset + slot width * j+1 + j * buffer 
	void SetAnchors( int i , int j )
	{
		leftX = (float)(widthOffset + ((float)j * (slots.GetComponent<RectTransform> ().rect.width + widthBuffer)))/ rectTransform.rect.width;
		rightX = (float)(widthOffset + ((float)((j +1) * slots.GetComponent<RectTransform> ().rect.width) + (j * widthBuffer))) / rectTransform.rect.width;
		upperY = (float)1 - ((float)(heightOffset + (float)i * (slots.GetComponent<RectTransform> ().rect.height + heightBuffer))/ rectTransform.rect.height);
		lowerY = (float)1 - ((float)(heightOffset + ((float)((i +1) * slots.GetComponent<RectTransform> ().rect.height) + (i * heightBuffer))) / rectTransform.rect.height);
	}
}
