using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/MovementController")]
public class MovementController : MonoBehaviour {
	//define speed
	public float speed = 6.0f;

	//define negative force to keep player on ground using collision
	public float gravity = -9.8f;

	//char contr component
	private CharacterController _charController;

	// Use this for initialization
	void Start () {
		//find component
		_charController = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		//find delta to create transform
		float deltaX = Input.GetAxis ("Horizontal") * speed;
		float deltaZ = Input.GetAxis ("Vertical") * speed;

		//create vector of movement
		Vector3 movement = new Vector3 (deltaX, 0, deltaZ);
		//restrict movement vector cant overlap speed to prevent infinity
		movement = Vector3.ClampMagnitude (movement, speed);
		//keep player on ground by continuesly move it down
		movement.y = gravity;

		//accelerate player up to max speed s = m * t;
		movement *= Time.deltaTime;
		//create transform movement in global
		movement = transform.TransformDirection (movement);
		//move played using global vector
		_charController.Move (movement);
	}
}
