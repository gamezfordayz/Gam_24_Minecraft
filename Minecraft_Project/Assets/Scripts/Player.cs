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

	public GameObject highlightFab;
	public GameObject[] activeToolArms = new GameObject[13];

	MoveToActive moveToActive;
	int indexOfSlot;
	SlotProperties activeSlot;
	public CubeProperties.itemIDs activeItemID;
	CubeProperties.itemIDs tempItemID;
	CubeProperties.itemType activeItemType;

	public AudioClip[] destroyBlockSounds;

	void Start ()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		moveToActive = MoveToActive.moveToActive;
		indexOfSlot = moveToActive.index;
		myCamera = transform.FindChild ("Main Camera");
	}
	
	void Update () 
	{
		if (OpenMenus.inMenu)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			return;
		}
		else 
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		tempItemID = activeItemID;

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
		if (Input.GetKeyDown (KeyCode.Space) && grounded) {
			if (gameObject.GetComponent<Rigidbody> ().velocity.y < 5f)	
				gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.up * jumpForce);
			grounded = false;
		}
		UpdateActiveSlot ();
		if (Input.GetMouseButtonDown (1)) 
		{
			CheckWhatWasHitRightClick();
		}
		if (Input.GetMouseButtonDown (0)) 
		{
			DestroyCubeFromMesh();
		}

		RaycastHit hit;
		if (Physics.Raycast (myCamera.transform.position, myCamera.forward, out hit, blockPlaceRange)) {
			if (hit.collider.gameObject.tag == "World") {
				Vector3 pos = hit.point - hit.normal / 2;
				pos.x = Mathf.Floor (pos.x) + 0.5f;
				pos.y = Mathf.Floor (pos.y) + 0.5f;
				pos.z = Mathf.Floor (pos.z) + 0.5f;


				highlightFab.transform.position = pos;
			}
		}
		else {
			highlightFab.transform.position  = Vector3.zero;
		}
		if (activeItemID != tempItemID)
			ShowActiveTool ();
	}

	void UpdateActiveSlot()
	{
		indexOfSlot = moveToActive.index;
		activeSlot = InventoryManager.inventoryManager.activeInventorySlots [indexOfSlot];
		activeItemID = activeSlot.itemID;
		if (activeItemID != 0)
			activeItemType = CubeProperties.cubeProperties.itemDict [activeItemID].itemType;
	}
	

	void CheckWhatWasHitRightClick()
	{
		RaycastHit hit;
		if(Physics.Raycast(myCamera.transform.position,  myCamera.forward,out hit, blockPlaceRange))
		{
			if(hit.collider.gameObject.tag == "World")
			{
				int x,y,z;
				hit.point -= hit.normal/2;
				x = Mathf.FloorToInt(hit.point.x - hit.transform.position.x);
				y = Mathf.FloorToInt(hit.point.y - hit.transform.position.y);
				z = Mathf.FloorToInt(hit.point.z - hit.transform.position.z);
				CubeProperties.itemIDs cube = (CubeProperties.itemIDs)hit.collider.gameObject.GetComponent<ChunkGenerator>().GetCube(x,y,z);
				if(cube == CubeProperties.itemIDs.craftingTable || cube == CubeProperties.itemIDs.furnace)
				{
					if(cube == CubeProperties.itemIDs.craftingTable)
						OpenMenus.openMenu.OpenCrafting();
					if(cube == CubeProperties.itemIDs.furnace)
						OpenMenus.openMenu.OpenFurnace();
				}
				else 
				{
					if(activeItemID != 0 && activeItemType == CubeProperties.itemType.block)
					{
						hit.point += hit.normal/2;
						AddCubeToMesh(hit);
					}
				}
			}
		}
	}

	void AddCubeToMesh(RaycastHit hit)
	{
		hit.point += hit.normal/2;
		Collider[] hitColliders =  Physics.OverlapSphere(hit.point , 0.5f);
		foreach (Collider temp in hitColliders)
		{
			if(temp.tag == "Player")
				return;
		}
		if(hit.collider.gameObject.tag == "World")
		{
			
			int x,y,z;
			
			x = Mathf.FloorToInt(hit.point.x - hit.transform.position.x);
			y = Mathf.FloorToInt(hit.point.y - hit.transform.position.y);
			z = Mathf.FloorToInt(hit.point.z - hit.transform.position.z);
			hit.collider.gameObject.GetComponent<ChunkGenerator>().CreateCube(x,y,z , activeItemID);
			activeSlot.UpdateNumberOfItem(-1);
		}
	}

	void DestroyCubeFromMesh()
	{
		RaycastHit hit;
		if(Physics.Raycast(myCamera.transform.position,  myCamera.forward,out hit, blockDestroyRange))
		{
			if(hit.collider.gameObject.tag == "World")
			{
				int x,y,z;
				hit.point -= hit.normal/2;
				x = Mathf.FloorToInt(hit.point.x - hit.transform.position.x);
				y = Mathf.FloorToInt(hit.point.y - hit.transform.position.y);
				z = Mathf.FloorToInt(hit.point.z - hit.transform.position.z);
				if((CubeProperties.itemIDs)hit.collider.gameObject.GetComponent<ChunkGenerator>().GetCube(x,y,z) == CubeProperties.itemIDs.bedrock)
					return;
				AudioSource.PlayClipAtPoint(destroyBlockSounds[Random.Range(0,9)],transform.position, .15f);
				hit.collider.gameObject.GetComponent<ChunkGenerator>().DestroyCube(x,y,z);
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
	void ShowActiveTool()
	{
		if (activeItemID == CubeProperties.itemIDs.diamondAxe)
		{
			for (int i = 0; i<activeToolArms.Length; i++)
			{
				activeToolArms[i].SetActive(false);
			}
			activeToolArms[1].SetActive(true);
		}
		else if (activeItemID == CubeProperties.itemIDs.goldAxe)
		{
			for (int i = 0; i<activeToolArms.Length; i++)
			{
				activeToolArms[i].SetActive(false);
			}
			activeToolArms[2].SetActive(true);
		}
		else if (activeItemID == CubeProperties.itemIDs.ironAxe)
		{
			for (int i = 0; i<activeToolArms.Length; i++)
			{
				activeToolArms[i].SetActive(false);
			}
			activeToolArms[3].SetActive(true);
		}
		else if (activeItemID == CubeProperties.itemIDs.woodAxe)
		{
			for (int i = 0; i<activeToolArms.Length; i++)
			{
				activeToolArms[i].SetActive(false);
			}
			activeToolArms[4].SetActive(true);
		}

		else if (activeItemID == CubeProperties.itemIDs.diamondPickAxe)
		{
			for (int i = 0; i<activeToolArms.Length; i++)
			{
				activeToolArms[i].SetActive(false);
			}
			activeToolArms[5].SetActive(true);
		}
		else if (activeItemID == CubeProperties.itemIDs.goldPickAxe)
		{
			for (int i = 0; i<activeToolArms.Length; i++)
			{
				activeToolArms[i].SetActive(false);
			}
			activeToolArms[6].SetActive(true);
		}
		else if (activeItemID == CubeProperties.itemIDs.ironPickAxe)
		{
			for (int i = 0; i<activeToolArms.Length; i++)
			{
				activeToolArms[i].SetActive(false);
			}
			activeToolArms[7].SetActive(true);
		}
		else if (activeItemID == CubeProperties.itemIDs.woodPickAxe)
		{
			for (int i = 0; i<activeToolArms.Length; i++)
			{
				activeToolArms[i].SetActive(false);
			}
			activeToolArms[8].SetActive(true);
		}
		else if (activeItemID == CubeProperties.itemIDs.diamondSword)
		{
			for (int i = 0; i<activeToolArms.Length; i++)
			{
				activeToolArms[i].SetActive(false);
			}
			activeToolArms[9].SetActive(true);
		}
		else if (activeItemID == CubeProperties.itemIDs.goldSword)
		{
			for (int i = 0; i<activeToolArms.Length; i++)
			{
				activeToolArms[i].SetActive(false);
			}
			activeToolArms[10].SetActive(true);
		}
		else if (activeItemID == CubeProperties.itemIDs.ironSword)
		{
			for (int i = 0; i<activeToolArms.Length; i++)
			{
				activeToolArms[i].SetActive(false);
			}
			activeToolArms[11].SetActive(true);
		}
		else if (activeItemID == CubeProperties.itemIDs.woodSword)
		{
			for (int i = 0; i<activeToolArms.Length; i++)
			{
				activeToolArms[i].SetActive(false);
			}
			activeToolArms[12].SetActive(true);
		}
		else
		{
			for (int i = 0; i<activeToolArms.Length; i++)
			{
				activeToolArms[i].SetActive(false);
			}
			activeToolArms[0].SetActive(true);
		}
		
	}
}