using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour {
	Vector3 mousePos;
	Vector3 lookAt;
	public float mouseDistance = 0f;
	//float verticalRotation = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (OpenMenus.inMenu) {
			mousePos = Input.mousePosition;
			mousePos.z = mouseDistance;
			lookAt = Camera.main.ScreenToWorldPoint (mousePos);
			lookAt.x = -lookAt.x;
			transform.LookAt (lookAt);
		}
//		else 
//		{
//
//			verticalRotation -= Input.GetAxis ("Mouse Y") * 5f;
//			verticalRotation = Mathf.Clamp (verticalRotation, -90, 90);
//			transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);
//		}
	}
}
