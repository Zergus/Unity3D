using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class Shooter : MonoBehaviour {
	//camera component
	private Camera _camera;
	//left button value
	private int leftBtn = 0;
	
	// Use this for initialization
	void Start () {
		//find camera
		_camera = GetComponent<Camera> ();
		//lock mouse in game UI
		Cursor.lockState = CursorLockMode.Locked;
		//hide cursor
		Cursor.visible = false;
	}

	void OnGUI () {
		int size = 12;
		// position of rect stars
		float posX = _camera.pixelWidth / 2 - size / 4;
		float posY = _camera.pixelHeight / 2 - size / 2;
		//creating label inside of rect
		GUI.Label (new Rect (posX, posY, size, size), "*");
	}
	
	// Update is called once per frame
	void Update () {
		//on click
		if (Input.GetMouseButtonDown (leftBtn)) {
			//create vector as point without length
			Vector3 point = new Vector3(_camera.pixelWidth/2, _camera.pixelHeight/2, 0);
			//create ray using vector
			Ray ray = _camera.ScreenPointToRay(point);
			//define hit data container
			RaycastHit hit;
			//check hit and save data to hit variable
			if (Physics.Raycast(ray, out hit)) {
				//find shot target
				GameObject hitObject = hit.transform.gameObject;
				//find component of target
				ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

				if (target != null) {
					//if it was found - it was enemy
					target.ReactToHit();
				} else {
					//else hit wall with shpere and run iterator
					StartCoroutine(SphereIndicator(hit.point));
				}

			}
		}
	}
	//enumerator/iterator at point of hit
	private IEnumerator SphereIndicator(Vector3 pos) {
		//create sphere
		GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		//place at hit position
		sphere.transform.position = pos;
		//return to parent context
		yield return new WaitForSeconds (1);
		//after 1 sec destroy sphere
		Destroy (sphere);
	}
}
