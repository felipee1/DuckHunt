using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixateScale : MonoBehaviour
{
    Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.lossyScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.SetToGlobal(initialScale);
    }
}
