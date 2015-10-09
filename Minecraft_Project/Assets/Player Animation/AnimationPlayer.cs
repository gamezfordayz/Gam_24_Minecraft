using UnityEngine;
using System.Collections;

public class AnimationPlayer : MonoBehaviour 
{

	Animator anim;
//	int jumpHash = Animator.StringToHash("Jump");
//	int runStateHash = Animator.StringToHash("Base Layer.Run");


	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float move = Input.GetAxis ("Vertical");
		anim.SetFloat ("Speed", move);
		if (gameObject.name == "minecraft_arm" || gameObject.name == "minecraft_DiaAxeArm"
		    || gameObject.name == "minecraft_GoldAxeArm" || gameObject.name == "minecraft_IronAxeArm" 
		    || gameObject.name == "minecraft_WoodAxeArm" || gameObject.name == "minecraft_DiaPickArm"
		    || gameObject.name == "minecraft_GoldPickArm" || gameObject.name == "minecraft_IronPickArm"
		    || gameObject.name == "minecraft_WoodPickArm" || gameObject.name == "minecraft_DiaSwordArm"
		    || gameObject.name == "minecraft_GoldSwordArm" || gameObject.name == "minecraft_IronSwordArm"
		    || gameObject.name == "minecraft_WoodSwordArm")
			anim.SetBool ("Hit", Input.GetMouseButton (0) || Input.GetMouseButton (1));



		//anim.SetBool ("Hit", Input.GetMouseButton (1));
//		anim.SetBool ("Hit", Input.GetMouseButton (0));
//		anim.SetBool ("Hit", Input.GetMouseButton (1));

//		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo (0);
//		if (Input.GetKeyDown (KeyCode.Space) && stateInfo.nameHash == runStateHash)
//		{
//			anim.SetTrigger (jumpHash);
//		}

	}
}