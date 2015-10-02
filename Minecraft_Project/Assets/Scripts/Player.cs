using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	
	float mouseSen = 5.0f;
	float verticalRotation = 1.0f;
	public float upDownRange = 60.0f;
	
	public bool grounded = true;
	public float speed = 5.0f; 
	public float jumpForce = 300f;
	public float sprintSpeed = 10.0f;
	
	
	
	public float damage = 5f;
	float timer;
	public Transform myCamera;
	public float blockPlaceRange = 5f;
	public float blockDestroyRange = 3f;

	public LayerMask blockLayer = 1;

	
	
	
	void Start ()
	{
		myCamera = transform.FindChild ("Main Camera");
	}
	
	void Update () 
	{
		float rotLeftRight = Input.GetAxis ("Mouse X") * mouseSen;
		transform.Rotate (0, rotLeftRight, 0);
		
		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSen;
		verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);
		Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);
		
		if (Input.GetKeyDown (KeyCode.LeftShift)) 
		{
			speed = sprintSpeed;
		}
		if (Input.GetKeyUp (KeyCode.LeftShift)) 
		{
			speed = 5.0f; 
		}
		if (Input.GetKey (KeyCode.W)) 
		{
			transform.Translate (Vector3.forward * Time.deltaTime * speed); 
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			transform.Translate (Vector3.back * Time.deltaTime * speed); 
		}
		if (Input.GetKey (KeyCode.A)) 
		{
			transform.Translate (Vector3.left * Time.deltaTime * speed); 
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			transform.Translate (Vector3.right * Time.deltaTime * speed); 
		}
		if (Input.GetKeyDown (KeyCode.Space) && grounded) 
		{
			if(gameObject.GetComponent<Rigidbody> ().velocity.y < 5f)	
				gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.up * jumpForce);
			grounded = false;


		}
//		if (gameObject.GetComponent<Rigidbody> ().velocity.y > 0 && startedJumping) {
//			startedJumping = false;
//			grounded = false;
//		}
		if (Input.GetMouseButtonDown (1)) 
		{
			RaycastHit hit;
			if(Physics.Raycast(myCamera.transform.position,  myCamera.forward,out hit, blockPlaceRange))
			{
				if(hit.collider.gameObject.tag == "World")
				{
					Debug.Log("Build at : " + (hit.point - hit.transform.position));
					//Instantiate( block, hit.collider.gameObject.transform.position + hit.normal, hit.transform.rotation );
				}
			}
			
		}
		if (Input.GetMouseButtonDown (0)) 
		{
			RaycastHit hit;
			if(Physics.Raycast(myCamera.transform.position,  myCamera.forward,out hit, blockDestroyRange))
			{
				if(hit.collider.gameObject.tag == "World")
				{
					int x,y,z;
					hit.point =- hit.normal/2;
					x = Mathf.FloorToInt(hit.point.x - hit.transform.position.x);
					y = Mathf.FloorToInt(hit.point.y - hit.transform.position.y);
					z = Mathf.FloorToInt(hit.point.z - hit.transform.position.z);
					hit.collider.gameObject.GetComponent<ChunkGenerator>().DestroyCube(x,y,z);
				}
			}
			
		}

	}
	void OnCollisionStay (Collision other)
	{
		if(other.gameObject.tag == "World")
		{
			grounded = true;
		}
	}
	
}