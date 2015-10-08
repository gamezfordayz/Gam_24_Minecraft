using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragAndDropIneventory : MonoBehaviour , IBeginDragHandler , IEndDragHandler,  IDragHandler, IDropHandler {

	public void OnBeginDrag( PointerEventData data)
	{
		InventoryManager.inventoryManager.SetFirstSwap (gameObject.GetComponent<SlotProperties> ());
	}
	

	public void OnDrag (PointerEventData eventData)
	{
		if(Input.GetMouseButtonDown(1))
		{
			Debug.Log(eventData.pointerPressRaycast.gameObject.name);
		}
	}

	public void OnEndDrag (PointerEventData eventData)
	{
	}

	public void OnDrop (PointerEventData eventData)
	{
		if (gameObject.GetComponent<SlotProperties> ().itemID == 0 && gameObject.tag == "Crafting")
			InventoryManager.inventoryManager.AddOneToCrafting(gameObject.GetComponent<SlotProperties>());
		else
			InventoryManager.inventoryManager.SwapWithFirstSlot (gameObject.GetComponent<SlotProperties> ());
	}
	
}
