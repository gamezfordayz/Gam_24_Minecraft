using UnityEngine;
using System.Collections;

public class DroppedObject : MonoBehaviour {

	public CubeProperties.itemIDs itemID;
	Transform child;
	//bool cantBePickedUp = true;
	// Use this for initialization
	void Start () 
	{
		Destroy (gameObject, 20f);
		child = transform.GetChild (0);
	}

	void Update()
	{
		child.transform.Rotate (new Vector3 (0, 2f, 0));
	}
	

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			if(InventoryManager.inventoryManager.AddItemToInventory(itemID))
			{
				InventoryManager.inventoryManager.PlayPickUpNoise();
				Destroy(gameObject);
			}
		}
	}

}
