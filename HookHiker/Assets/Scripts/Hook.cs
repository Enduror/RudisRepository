using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {
    //-----------Timer------------// 
	public float deathTimer;
    public float timeCounter;
   

    //-----------Bullet------------// 
    public float projectileVelocity;
    public Vector2 shootingDirection;


    //-----------PlayerControllerReference------------// 

    public PlayerController playerController;  

  
    

	
	void Start () {
        //setting counter to gamtime
        timeCounter = Time.time;


        // BulletDeathtimer and Velocity
        deathTimer = 1;
        projectileVelocity = 3;


        //Referencing the playcontroller and giving a fixed shooting direction for every bullet
        playerController = FindObjectOfType<PlayerController>();
        shootingDirection = playerController.hookDirection;
	}	

	// Update is called once per frame
	void Update ()
    {       
         travelToTarget(shootingDirection);
        
    }

    //Method that kills Bullet after " Deathtimer" seconds
	public void killBullet(){
        Destroy(this.gameObject);
	}   

   //translate Funktion for bullet movement back and forth

    public void travelToTarget(Vector2 target)
    {
        if (Time.time - timeCounter >= deathTimer)
        {
            target =new Vector2 (playerController.transform.position.x-transform.position.x,playerController.transform.position.y- transform.position.y);
            projectileVelocity =3+target.magnitude/80;           
          
        }
        transform.Translate(target * Time.deltaTime * projectileVelocity);
        
        
    }
    
}
