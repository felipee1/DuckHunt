using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;


public class LerpVisualElementOnContact : LerpVisualElement
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<VRInteractiveItem>())
        {
            HandleOver();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<VRInteractiveItem>())
        {
            HandleOut();
        }
    }
}
