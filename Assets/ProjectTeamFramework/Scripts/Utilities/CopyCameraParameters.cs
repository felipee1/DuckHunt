using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Camera))]
public class CopyCameraParameters : MonoBehaviour
{

    public Camera camToCopy;

    Camera myCam;
    // Start is called before the first frame update
    void Start()
    {
        myCam = GetComponent<Camera>();
        myCam.cullingMask = camToCopy.cullingMask;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
