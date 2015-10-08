using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpenMenus : MonoBehaviour {
	public static bool inMenu = false;
	public GameObject inventory;
	public GameObject menuFadeScreen;
	public Canvas canvas;
	// Use this for initialization
	void Start () {
		
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
				inMenu = false;
			}
			else
			{
				canvas.renderMode = RenderMode.ScreenSpaceOverlay;
				inMenu = true;
			}
		}

	}
}
