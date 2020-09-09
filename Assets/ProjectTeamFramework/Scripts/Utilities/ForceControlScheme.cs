using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceControlScheme : MonoBehaviour
{
    PlatformManager platformManager;
    
    public VRControlScheme newControlScheme = VRControlScheme.Laser;

    public bool onlyOnStart;

    public bool onlyOnPlatform = false;

    public VRPlataform checkPlatform = VRPlataform.OculusQuest;


    // Start is called before the first frame update
    void Start()
    {
        if(onlyOnStart)
        {
            ForceControls();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!onlyOnStart)
        {
            ForceControls();
        }
    }

    void ForceControls()
    {
        if (!platformManager)
            platformManager = ReferenceManagerIndependent.Instance.PlatformManager;
        if (platformManager)
        {
            if (!onlyOnPlatform || platformManager.CurrentVRPlatform == checkPlatform)
            {
                if(platformManager.CurrentVRControlScheme != newControlScheme)
                    platformManager.SetVRControlScheme(newControlScheme);
            }
        }
        else
            Debug.LogError("Configure o platform manager na cena.");
    }
}
