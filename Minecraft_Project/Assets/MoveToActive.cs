using UnityEngine;
using System.Collections;

public class MoveToActive : MonoBehaviour {
	public static MoveToActive moveToActive;
	public Transform [] slotPositions;
	public float speed = 4;
	public int index = 0;

	void Awake () 
	{
		moveToActive = this;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			if(index != 8)
			{
				index++;
			}
			else
			{
				index = 0;
			}
		}
		else if(Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			if(index != 0)
			{
				index--;
			}
			else
			{
				index = 8;
			}
		}

		if (Input.GetKeyDown (KeyCode.Alpha1))
			index = 0;
		if (Input.GetKeyDown (KeyCode.Alpha2))
			index = 1;
		if (Input.GetKeyDown (KeyCode.Alpha3))
			index = 2;
		if (Input.GetKeyDown (KeyCode.Alpha4))
			index = 3;
		if (Input.GetKeyDown (KeyCode.Alpha5))
			index = 4;
		if (Input.GetKeyDown (KeyCode.Alpha6))
			index = 5;
		if (Input.GetKeyDown (KeyCode.Alpha7))
			index = 6;
		if (Input.GetKeyDown (KeyCode.Alpha8))
			index = 7;
		if (Input.GetKeyDown (KeyCode.Alpha9))
			index = 8;
		transform.position = Vector3.Lerp (transform.position , slotPositions[index].position , speed );

	}
}
