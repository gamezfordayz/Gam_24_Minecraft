using UnityEngine;
using System.Collections;

public class OpenMenus : MonoBehaviour {
	public static bool inMenu = false;
	public GameObject inventory;
	public GameObject menuFadeScreen;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.I)) {
				menuFadeScreen.SetActive(!menuFadeScreen.activeInHierarchy);
				inventory.SetActive(!inventory.activeSelf);
		}
		inMenu = menuFadeScreen.activeInHierarchy;

	}
}
