﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    public GameObject projectile;
	public Vector3 mousePos;
    public float speed = 3;
	public float distanceToSpawnFromPlayer = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     //   mousePos.z = this.transform.position.z;
	//	Vector3 direction = Vector3.zero;  // = ?? 

        if (Input.GetMouseButtonDown(0))
        {
            // GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject; 
            // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // // mousePosition.z = 0;
            // proj.GetComponent<Projectile>().travelDirection = mousePosition - transform.position;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = transform.position + (mousePos - transform.position).normalized * distanceToSpawnFromPlayer;
            Instantiate(projectile, direction, Camera.main.transform.rotation);
        }
    }
}
