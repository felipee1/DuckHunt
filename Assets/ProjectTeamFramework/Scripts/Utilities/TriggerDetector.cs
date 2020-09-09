using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{

    public event Action<Collider> OnTriggerEnterEvent;
    public event Action<Collider> OnTriggerStayEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        OnTriggerStayEvent?.Invoke(other);
    }

    

}
