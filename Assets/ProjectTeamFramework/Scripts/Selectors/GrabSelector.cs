using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

using System;

[RequireComponent(typeof(Rigidbody))]
public class GrabSelector : BaseSelector
{    
    [Tooltip("To improve readability in paper forms")]
    [SerializeField] bool duplicateSizeInVR;
    const float extraSize = 2;
    HandsController playerController;
       
    private void Start()
    {
        playerController = ReferenceManagerDependent.Instance.HandsController;
    }

    public override void OnInteractionTrigger(InteractionModes mode)
    {
        Debug.Log("entrou");
        playerController.SetObjectOnHand(this.gameObject, true);
        DuplicateSizeInVR();
        OnFinish();
    }

    protected override void OnFinish()
    {
        base.Finished(this.gameObject);
    }

    public void DuplicateSizeInVR()
    {
         if(duplicateSizeInVR && 
            ReferenceManagerIndependent.Instance.PlatformManager.CurrentVRPlatform != VRPlataform.PC)
            this.transform.localScale *= extraSize; //para formularios em papel
    }
}
