using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

//-----------------Character Attributes-------------------
	public float speed;


	
    //---------------------------HookCooldown-------------------------
    public float lastLeftHook;   // seperated Hook cd for left and Right.
    public float lastRightHook; // seperated Hook cd for left and Right.
    public float hookDelay;     // Time between left or right hook can be used again


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
               
    }

	// Calculates the vector between player and mous. Controll check for Fire and Movement.
	void Update () {
		lookToMouse ();
		horizontalCharacterMovement ();
        fireHook();

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
        // checks if hook is on cooldown and if left click was done left of the player
        //instantiates a hook and resets the timer.
        if (Time.time - lastLeftHook >= hookDelay &&(Input.GetMouseButtonDown(0) && mousePosition.x <= transform.position.x))
        {
                lastLeftHook = Time.time;
                instantiateHook();                
        }
        // checks if hook is on cooldown and if right click was done left of the player
        //instantiates a hook and resets the timer.
        if ((Input.GetMouseButtonDown(1) && mousePosition.x >= transform.position.x)&& Time.time - lastRightHook >= hookDelay)
        {
                lastRightHook = Time.time;
                instantiateHook();
        }     
    }

    //instantiates a hook. hookDirection is taken by the Hook class 
    // Hook prefab is Kinematic now due to the problem that the hook was initiated within the player and bouth have a hitbox.
    public void instantiateHook()
    {        
        hookDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        GameObject Hook = (GameObject)Instantiate(Resources.Load("Prefabs/Hook"), transform.position, Quaternion.identity);   
    }
    
}


