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
		if(Input.GetAxis("Mouse ScrollWheel") > 0)
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
		else if(Input.GetAxis("Mouse ScrollWheel") < 0)
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

		transform.position = Vector3.Lerp (transform.position , slotPositions[index].position , speed );

	}
}
