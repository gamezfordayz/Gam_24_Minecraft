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
		anim.SetBool ("Hit", Input.GetMouseButton (0) || Input.GetMouseButton (1));

	}
}