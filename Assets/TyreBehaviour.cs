using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyreBehaviour : MonoBehaviour {

	public bool isGrounded = false;

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag.Equals("Ground")) {
			isGrounded = true;
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if(collision.collider.tag.Equals("Ground")) {
			isGrounded = false;
		}
	}
}
