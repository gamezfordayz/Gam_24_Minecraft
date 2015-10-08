using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MimicActiveItems : MonoBehaviour {

	public int index;
	public Image image;
	public Text text;
	public SlotProperties slot;
	public CubeProperties.itemIDs itemID;

	void Start()
	{ 
		slot = InventoryManager.inventoryManager.activeInventorySlots [index];
		image = transform.FindChild ("ItemIcon").GetComponent<Image>();
		text = transform.FindChild ("ItemNumber").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		itemID = slot.itemID;
		image.sprite = slot.image.sprite;
		text.text = slot.textObj.text;
	}
}
