using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {
	public float deathTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		killBullet ();
	}

	void killBullet(){
		deathTimer += 1.0F * Time.deltaTime;
		if (deathTimer >= 1) {
			GameObject.Destroy(gameObject);
		}
	}
}
