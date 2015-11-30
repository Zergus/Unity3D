using UnityEngine;
using System.Collections;

public class LookController : MonoBehaviour {
	//speed horiz and vert
	public float sensivityHor = 9.0f;
	public float sensivityVert = 9.0f;

	//enum for configs of look
	public enum RotationAxes {
		MouseXAndY = 0,
		MouseX = 1,
		MouseY = 2
	}

	//create comparing retrevial variables
	public RotationAxes axes = RotationAxes.MouseXAndY;

	//min max values for X rotate
	public float minVert = -65.0f;
	public float maxVert = 80.0f;

	//starting rotation
	private float _rotationX = 0;

	// Use this for initialization
	void Start () {
		//disable pshysics rotation
		Rigidbody body = GetComponent<Rigidbody> ();
		if (body != null) {
			body.freezeRotation = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//checks for direction of mouse
		if (axes == RotationAxes.MouseX) {
			//simple rotate for value of X prevented of continues rotation by freeze for optimization
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensivityHor, 0);
		} else if (axes == RotationAxes.MouseY) {
			//definitions of X rotation
			float rotationY = transform.localEulerAngles.y;
			_rotationX -= Input.GetAxis("Mouse Y") * sensivityVert;
			//fixing between min and max
			_rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);
			//rotation itself
			transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
		} else {
			//delta of mouses X movement
			float delta = Input.GetAxis("Mouse X") * sensivityHor;
			//rotation based on delta
			float rotationY = transform.localEulerAngles.y + delta;
			//rotations around X
			_rotationX -= Input.GetAxis("Mouse Y") * sensivityVert;
			_rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);
			
			transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
		}
	}
}