using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

//-----------------Character Attributes-------------------
	public float speed;


	//--------------------------------------------------------

	public Hook leftHook;
	//private List<GameObject> Projectiles = new List<GameObject>();
	public float projectileVelocity;
	public Vector2 projectileDirection;
	public Vector3 mousePosition;
	public Vector2 direction;
	public bool isReadyToShoot;
	public bool isBulletAlive;



	// Use this for initialization
	void Start () {
		speed = 20;
		projectileVelocity = 2;
		isReadyToShoot = true;
		}

	// Update is called once per frame
	void Update () {
		lookToMouse ();
		horizontalCharacterMovement ();
		fireLeftHook ();

		}
	public void lookToMouse () {
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		direction = new Vector2 (mousePosition.x-transform.position.x, mousePosition.y-transform.position.y);
		}

	public void horizontalCharacterMovement (){
		float axisX = Input.GetAxis ("Horizontal");
		transform.Translate (new Vector2 (axisX, 0) * Time.deltaTime * speed);
		}

	public void fireLeftHook () {
		if (Input.GetMouseButtonDown (0)) {
			if (isReadyToShoot) {
				GameObject lefty = (GameObject)Instantiate (Resources.Load("Prefabs/Hook"), transform.position, Quaternion.identity);
				projectileDirection = direction;

			}
		}
			//leftHook.transform.Translate (projectileDirection * Time.deltaTime * projectileVelocity);
	}
}

