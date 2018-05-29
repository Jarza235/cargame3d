using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

	public Cinemachine.CinemachineVirtualCamera virtualCamera;

	public GameObject tyreFrontLeft;
	public GameObject tyreFrontRight;
	public GameObject tyreBackLeft;
	public GameObject tyreBackRight;

	public GameObject body;
	private Rigidbody bodyRb;

	public GameObject floor;
	private Rigidbody floorRb;

	private Rigidbody tyreFrontLeftRb;
	private Rigidbody tyreFrontRightRb;
	private Rigidbody tyreBackLeftRb;
	private Rigidbody tyreBackRightRb;

	private HingeJoint hingeJointFrontLeft;
	private HingeJoint hingeJointFrontRight;
	private HingeJoint hingeJointBackLeft;
	private HingeJoint hingeJointBackRight;

	private TyreBehaviour tbFrontLeft;
	private TyreBehaviour tbFrontRight;
	private TyreBehaviour tbBackLeft;
	private TyreBehaviour tbBackRight;

	public float angularVelocity = 10f;
	public float torqueRotationForce = 100f;
	public float downForce = 100f;

	public float onAirTorque = 100f;

	private bool accelerating = false;
	private bool braking = false;

	// Use this for initialization
	void Start () {
		tyreFrontLeftRb  = tyreFrontLeft.GetComponent<Rigidbody>();
		tyreFrontRightRb = tyreFrontRight.GetComponent<Rigidbody>();
		tyreBackLeftRb   = tyreBackLeft.GetComponent<Rigidbody>();
		tyreBackRightRb  = tyreBackRight.GetComponent<Rigidbody>();

		bodyRb = body.GetComponent<Rigidbody>();
		floorRb = floor.GetComponent<Rigidbody>();

		hingeJointFrontLeft  = tyreFrontLeft.GetComponent<HingeJoint>();
		hingeJointFrontRight = tyreFrontRight.GetComponent<HingeJoint>();
		hingeJointBackLeft   = tyreBackLeft.GetComponent<HingeJoint>();
		hingeJointBackRight  = tyreBackRight.GetComponent<HingeJoint>();

		tbFrontLeft  = tyreFrontLeft.GetComponent<TyreBehaviour>();
		tbFrontRight = tyreFrontRight.GetComponent<TyreBehaviour>();
		tbBackLeft   = tyreBackLeft.GetComponent<TyreBehaviour>();
		tbBackRight  = tyreBackRight.GetComponent<TyreBehaviour>();

		virtualCamera.Follow = floor.transform;
		virtualCamera.LookAt = floor.transform;
	}
	
	// Update is called once per frame
	void Update () {

		// Downforce
		bodyRb.AddForce(new Vector3(0f, -1f, 0f) * downForce);

		if(IsGrounded()) {
			AccelerateAndBrake();
			Turn();
		}
		else {
			TiltOnAir();
		}

		Reset();
	}

	private bool IsGrounded() {
		if(tbFrontLeft.isGrounded || tbFrontRight.isGrounded || tbBackLeft.isGrounded || tbBackRight.isGrounded) {
			return true;
		}
		return false;
	}

	private void AccelerateAndBrake() {
		Vector3 localForward = tyreFrontLeft.transform.right; //body.transform.right;

		// Set max values for angular velocity (Because setting just angular velocity isn't enough)
		tyreFrontLeftRb.maxAngularVelocity = angularVelocity;
		tyreFrontRightRb.maxAngularVelocity = angularVelocity;
		tyreBackLeftRb.maxAngularVelocity = angularVelocity;
		tyreBackRightRb.maxAngularVelocity = angularVelocity;

		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			tyreFrontLeftRb.angularVelocity  = localForward * angularVelocity;
			tyreFrontRightRb.angularVelocity = localForward * angularVelocity;

			tyreBackLeftRb.angularVelocity  = localForward * angularVelocity;
			tyreBackRightRb.angularVelocity = localForward * angularVelocity;
		}
		else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			tyreFrontLeftRb.angularVelocity  = -localForward * angularVelocity;
			tyreFrontRightRb.angularVelocity = -localForward * angularVelocity;

			tyreBackLeftRb.angularVelocity  = -localForward * angularVelocity;
			tyreBackRightRb.angularVelocity = -localForward * angularVelocity;
		}
	}

	private void Turn() {
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {

			tyreFrontLeft.transform.localEulerAngles = new Vector3(0f, 20f, 0f);
			tyreFrontRight.transform.localEulerAngles = new Vector3(0f, 20f, 0f);
		}
		else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {

			tyreFrontLeft.transform.localEulerAngles = new Vector3(0f, -20f, 0f);
			tyreFrontRight.transform.localEulerAngles = new Vector3(0f, -20f, 0f);
		}
	}

	private void TiltOnAir() {
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			floorRb.AddRelativeTorque(new Vector3(-1f, 0f, 0f) * onAirTorque);
		}
		else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			floorRb.AddRelativeTorque(new Vector3(1f, 0f, 0f) * onAirTorque);
		}

		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			floorRb.AddRelativeTorque(new Vector3(0f, 0f, -1f) * onAirTorque);
		}
		else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			floorRb.AddRelativeTorque(new Vector3(0f, 0f, 1f) * onAirTorque);
		}
	}

	private void Reset() {
		if(Input.GetKeyDown(KeyCode.R)) {
			GameObject clone = Instantiate (Resources.Load ("Car2")) as GameObject;
			Destroy(gameObject);
		}
	}
}
