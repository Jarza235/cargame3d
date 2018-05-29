using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject objectToFollow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(objectToFollow.transform.position.x, objectToFollow.transform.position.y + 4f, objectToFollow.transform.position.z - 10f);
		//transform.eulerAngles = new Vector3(30f, objectToFollow.transform.eulerAngles.y, 0f);
	}
}
