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

    }
	
	void Update () {

        //transform.Translate(travelDirection.normalized * TravelSpeed, Space.World);
        directionToMove = transform.forward;
        transform.position += (directionToMove * speed);
        // transform.rotation = (transform.rotation);
        if (destroy)
        {
            destroy = false;
            Destroy(gameObject, 5);
        }
        
	}
}
