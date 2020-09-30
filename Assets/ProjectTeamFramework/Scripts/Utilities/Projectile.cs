using UnityEngine;
using System.Collections;
using System;

public class Projectile : MonoBehaviour {
    //Exercício 5

    public float TravelSpeed; //how fast will this object move?
    [NonSerialized]public Vector3 travelDirection; // Vector3 indicating travel direction
    private bool destroy = true;
    public Vector3 directionToMove;
    public float speed = 1;

	void Start () {



        //Rotate Projectile to "point to mouse direction": Lazy Way
        //transform.LookAt(Input.mousePosition); //rotate object to face mousePosition

        //Rotate Projectile to "point to mouse direction" "Right" Way
        
        //find angle between travel direction and projectile direction
        float angle = Mathf.Atan2(travelDirection.y - transform.right.y, travelDirection.x - transform.right.x) * Mathf.Rad2Deg;
        //rotate it by this angle
        transform.Rotate(new Vector3(0, 0, angle));

	}
	
	
	void Update () {

        // transform.Translate(travelDirection.normalized * TravelSpeed, Space.World);
        directionToMove = transform.forward.normalized;        
        transform.position += (directionToMove * speed);
        if (destroy)
        {
            destroy = false;
            Destroy(gameObject, 5);
        }
        
	}
}
