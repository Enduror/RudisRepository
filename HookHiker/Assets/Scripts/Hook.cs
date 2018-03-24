using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {
    //-----------Timer------------// 
	public float deathTimer;
    public float timeCounter;
    public float timeAfterHitBulletDies;
   

    //-----------Bullet------------// 
    public float projectileVelocity;
    public Vector2 shootingDirection;
    public bool hitSomething;
    public Vector2 maxShootingDistanceVector;
    public int maxShootingRangeInt;
    public bool onTheWayBack;



    //-----------PlayerControllerReference------------// 

    public PlayerController playerController;
    public DistanceJoint2D grapplingHook;


    // Collision variables
    public GameObject collisionTarget;

    

    void Start () {
        //setting counter to gamtime
        timeCounter = Time.time;
        

        // BulletDeathtimer and Velocity
        deathTimer = 1;
        projectileVelocity = 1;
        maxShootingRangeInt = 5;


        //Initiating some Booleans
        onTheWayBack = false;

        //Referencing the playcontroller and giving a fixed shooting direction for every bullet
        playerController = FindObjectOfType<PlayerController>();
        shootingDirection = playerController.hookDirection;
        //Referencing the hook
        grapplingHook = playerController.GetComponent<DistanceJoint2D>();
    }	

	// Update is called once per frame
	void Update ()
    {       
         travelToTarget(shootingDirection);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rock")
        {
            hitSomething = true;
            grapplingHook.enabled = true;
           
            grapplingHook.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
            grapplingHook.distance = Vector2.Distance(transform.position, playerController.transform.position);

            killBullet();
        }
        if (collision.gameObject.tag == "Player" && onTheWayBack == true)
        {           
            hitSomething = true;
            killBullet();
        }

    }



    //Method that kills Bullet after " Deathtimer" seconds
    public void killBullet(){

        Destroy(this.gameObject);
	}   

   //translate Funktion for bullet movement back and forth

    public void travelToTarget(Vector2 target)
    {
        // as long as you didnt hit anything
        if (!hitSomething)
        {
            // if shooting cooldown is rdy
            if (Time.time - timeCounter >= deathTimer)
            {
                target = new Vector2(playerController.transform.position.x - transform.position.x, playerController.transform.position.y - transform.position.y);

                projectileVelocity = 3 + target.magnitude / 80;
                onTheWayBack = true;
            }
            transform.Translate(target * Time.deltaTime * projectileVelocity);
        }
        else
        {
            //Debug.Log("Ja hier passier");
            //timeCounter = Time.time;
            transform.Translate(0,0,0);
            //if (Time.time - timeCounter >= deathTimer)
            //{
            //    killBullet();
            //}

        }
        
    }
    public GameObject returnCollisionTarget(GameObject gameObject)
    {
        return gameObject;
    }
    

    
}
