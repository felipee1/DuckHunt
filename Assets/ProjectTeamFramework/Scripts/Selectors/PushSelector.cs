using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

using System;

[RequireComponent(typeof(ObjectMoverAssistance))]
public class PushSelector : BaseSelector
{

    ObjectMoverAssistance objectAttracting;
    HandsController playerController;
    bool currentlyEnabledToFixateOnDropTarget = false;
    GameObject objectToFixate;
    Vector3 objectRotationAfterFixated;

    private void Start()
    {
        playerController = ReferenceManagerDependent.Instance.HandsController;
        objectAttracting = GetComponent<ObjectMoverAssistance>();
        objectAttracting.enabled = false;
    }
    
    public void ActivatePushing(GameObject go)
    {
        objectAttracting.ActivateMover(go, false);
        objectAttracting.OnExecuted += Finished;
        objectAttracting.enabled = true;
    }

    protected override void Finished(GameObject go)
    {
        objectAttracting.OnExecuted -= Finished;
        objectAttracting.enabled = false;
        if (objectToFixate)
        {
            if (go == objectToFixate)
            {
                objectToFixate.transform.eulerAngles = objectRotationAfterFixated;
                ExecuteDropOfObject(go);                
            }
            else
                playerController.DropObject(go.transform, false, null);
        }
        else
        {
            playerController.DropObject(go.transform, false, null);
            base.Finished(go);
        }          
    }

    public void SetFixationAfterDropping(GameObject objectToDrop, bool fixated, Vector3 objectRotationAfterFixated)
    {
        currentlyEnabledToFixateOnDropTarget = fixated;
        this.objectToFixate = objectToDrop;
        this.objectRotationAfterFixated = objectRotationAfterFixated;
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnTriggerEnter(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {        
        GameObject go = other.gameObject;
        //print("Tentando por " + go.name + " no " + this.name);
        if (currentlyEnabledToFixateOnDropTarget == false && objectToFixate != null && go == objectToFixate)
        {
            ExecuteDropOfObject(go);
        }        
        else
            base.Finished(go);
    }

    void ExecuteDropOfObject(GameObject go)
    {
        playerController.DropObject(go.transform, currentlyEnabledToFixateOnDropTarget, this.transform);
        currentlyEnabledToFixateOnDropTarget = false;
        objectToFixate = null;
        base.Finished(go);
    }

    public override void OnInteractionTrigger(InteractionModes mode)
    {
        throw new NotImplementedException();
    }

    protected override void OnFinish()
    {
        Finished(this.gameObject);
    }
}
