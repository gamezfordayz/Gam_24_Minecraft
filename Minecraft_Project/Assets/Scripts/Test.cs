using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = new Vector3 (player.transform.position.x, player.transform.position.y + 5f, player.transform.position.z);
		transform.position = pos;
	}
}
