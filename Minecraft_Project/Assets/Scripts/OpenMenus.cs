using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpenMenus : MonoBehaviour 
{
	public static OpenMenus openMenu;
	public static bool inMenu = false;
	public GameObject inventory;
	public GameObject menuFadeScreen;
	public GameObject[] inventoryCrafting;
	public GameObject craftingTable;
	public GameObject furnaceCrafting;
	public Canvas canvas;
	// Use this for initialization

	void Start () 
	{
		openMenu = this;	
		inMenu = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			if(canvas.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				canvas.renderMode = RenderMode.WorldSpace;
				canvas.transform.position =  new Vector3 (-500f ,-500f,-500f);
				CloseCrafting();
				CloseFurnace();
				CloseInventory();
				inMenu = false;
			}
			else
			{
				OpenInventory();
			}
		}

	}

	public void OpenInventory()
	{
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		foreach (GameObject temp in inventoryCrafting)
		{
			temp.SetActive(true);
		}
		inMenu = true;
	}

	public void OpenCrafting()
	{
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		craftingTable.SetActive (true);
		inMenu = true;
	}

	public void OpenFurnace()
	{
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		furnaceCrafting.SetActive (true);
		inMenu = true;
	}

	public void CloseInventory()
	{
		foreach (GameObject temp in inventoryCrafting)
		{
			temp.SetActive(false);
		}
	}
	
	public void CloseCrafting()
	{
		craftingTable.SetActive (false);
	}
	
	public void CloseFurnace()
	{
		furnaceCrafting.SetActive (false);
	}
}
