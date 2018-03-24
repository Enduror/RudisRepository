using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

//-----------------Character Attributes-------------------
	public float speed;
    public float lockedRotation = 0;
    public DistanceJoint2D distanceJoint;
    


	
    //---------------------------HookCooldown and stuff-------------------------
    public float lastLeftHook;   // seperated Hook cd for left and Right.
    public float lastRightHook; // seperated Hook cd for left and Right.
    public float hookDelay;     // Time between left or right hook can be used again

    public bool leftHook;
    public bool rightHook;


    //------------------------Vectors-----------------------------
    public Vector2 hookDirection;    
	public Vector3 mousePosition;
	public Vector2 direction;


	//I used a different Timer for left and right hook and initialize them here
    //-------Problem---->>>  Cant shoot in the first secounds. bettersolution?? 
	void Start ()
    {
        lastLeftHook = Time.time;
        lastRightHook = Time.time;        
        distanceJoint = GetComponent<DistanceJoint2D>();

        leftHook = false;
        rightHook = false;

        
               
    }

	// Calculates the vector between player and mous. Controll check for Fire and Movement.
	void Update () {
       
        lookToMouse ();
		horizontalCharacterMovement ();
        fireHook();
        if (Input.GetMouseButton(0) && leftHook == true)
        {
            distanceJoint.distance = distanceJoint.distance - 0.2f;
        }
        if (Input.GetMouseButton(1) && rightHook == true)
        {
            distanceJoint.distance = distanceJoint.distance - 0.2f;
        }

    }
    // vector between played and mous normalized in screen to world point coordinates
	public void lookToMouse () {
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		direction = new Vector2 (mousePosition.x-transform.position.x, mousePosition.y-transform.position.y);
		}

    // Horizontal input and Movement.  
    // Problem---->>>>> When character falls hes fucked. need translation in world horizontal instead of in relation to player transform.
	public void horizontalCharacterMovement ()
    {
		float axisX = Input.GetAxis ("Horizontal");
		transform.Translate (new Vector2 (axisX, 0) * Time.deltaTime * speed);
    }


        
     // Method to fire a hook  in eather the left or the right side.
    public void fireHook()
    {
        instantiateHook();
    }

    //instantiates a hook. hookDirection is taken by the Hook class 
    // Hook prefab is Kinematic now due to the problem that the hook was initiated within the player and bouth have a hitbox.
    public void instantiateHook()
    {

        if (Time.time - lastLeftHook >= hookDelay && (Input.GetMouseButtonDown(0) && mousePosition.x <= transform.position.x))
        {
            lastLeftHook = Time.time;            
            hookDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            GameObject LeftHook = (GameObject)Instantiate(Resources.Load("Prefabs/Hook"), transform.position, Quaternion.identity);
            leftHook = true;

        }
        // checks if hook is on cooldown and if right click was done left of the player
        //instantiates a hook and resets the timer.
        if ((Input.GetMouseButtonDown(1) && mousePosition.x >= transform.position.x) && Time.time - lastRightHook >= hookDelay)
        {
            lastRightHook = Time.time;
            hookDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            GameObject RightHook = (GameObject)Instantiate(Resources.Load("Prefabs/Hook"), transform.position, Quaternion.identity);
            rightHook = true;
        }
        
        
        

        
    }

    // Method to stop the Character Rotation so taht he cant fall anymore
    public void lockRotation()
    {
        transform.rotation = Quaternion.Euler(lockedRotation, lockedRotation, lockedRotation);
    }
    
}


