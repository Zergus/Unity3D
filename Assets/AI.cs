﻿using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {
	[SerializeField] private GameObject fireballPrefab;
	private GameObject _fireball;

	public float enemySpeed = 3.0f;
	public float obstacleRange = 5.0f;

	private bool _alive;

	// Use this for initialization
	void Start () {
		_alive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (_alive) {
			transform.Translate (0, 0, enemySpeed * Time.deltaTime);
			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;
			if (Physics.SphereCast (ray, 0.75f, out hit)) {
				GameObject hitObject = hit.transform.gameObject;
				if (hitObject.GetComponent<PlayerCharacter>()) {
					if (_fireball == null) {
						_fireball = Instantiate(fireballPrefab) as GameObject;
						_fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
						_fireball.transform.rotation = transform.rotation;
					}
				} 
				else if (!hitObject.GetComponent<Fireball>() && hit.distance < obstacleRange) {
					float angle = Random.Range(-110, 110);
					transform.Rotate(0, angle, 0);
				}
			}
		}
	}

	public void SetAlive(bool alive) {
		_alive = alive;
	}
}
