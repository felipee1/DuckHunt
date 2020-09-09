using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemotePivot : MonoBehaviour
{
    public Transform myPivot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = myPivot.position;
    }
}
