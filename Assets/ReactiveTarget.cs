using UnityEngine;
using System.Collections;

public class ReactiveTarget : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//accessible method from within target game object
	public void ReactToHit () {
		AI behaviour = GetComponent<AI> ();
		if (behaviour != null) {
			behaviour.SetAlive(false);
		}
		StartCoroutine (Die ());
	}

	private IEnumerator Die () {
		this.transform.Rotate (-75, 0, 0);

		yield return new WaitForSeconds (1.5f);
		//destroy object not this component only
		Destroy (this.gameObject);
	}
}
