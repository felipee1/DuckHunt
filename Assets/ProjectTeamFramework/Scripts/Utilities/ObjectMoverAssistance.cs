using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

using System;

public class ObjectMoverAssistance: MonoBehaviour
{
    public event Action<GameObject> OnExecuted;

    public LerpEquationTypes lerp;
    public Vector3 offset = Vector3.one;
    float lerpFactor;
    float totalLerpDuration;
    public float lerpTime = 0.75f;
    protected float countDownToTurnOff;

    Vector3 lerpPosA, lerpPosB;

    PlatformManager platformManager;
    HandsController handsController;
    Transform currentObjectBeingMoved;
    bool pullOrPush; // true if pull, false if push

    private void Awake()
    {
        platformManager = ReferenceManagerIndependent.Instance.PlatformManager;
        handsController = ReferenceManagerDependent.Instance.HandsController;
        totalLerpDuration = lerpTime;
    }

    public void ActivateMover(GameObject objectOnMyHand, bool pullOrPush )
    {
        countDownToTurnOff = lerpTime;
        currentObjectBeingMoved = objectOnMyHand.transform;
        Rigidbody rigidbody = currentObjectBeingMoved.GetComponent<Rigidbody>();
        if (rigidbody)
        {
            rigidbody.isKinematic = false;
        }

        if (currentObjectBeingMoved.GetComponent<Collider>())
            currentObjectBeingMoved.GetComponent<Collider>().enabled = false;
        this.pullOrPush = pullOrPush;
        UpdateLerpPosition();        
    }  

    void Update()
    {
        if (countDownToTurnOff > 0)
        {
            countDownToTurnOff -= Time.deltaTime;
            lerpFactor = countDownToTurnOff / totalLerpDuration;
            if(pullOrPush)
                lerpPosB = CalculateDestiny();
            currentObjectBeingMoved.position = lerp.Lerp(lerpPosA, lerpPosB, 1 - lerpFactor);
            if (countDownToTurnOff < 0)
            {
                Finished();
            }
        }
    }

    void UpdateLerpPosition()
    {            
        lerpPosA = currentObjectBeingMoved.position;
        lerpPosB = CalculateDestiny();
    }

    Vector3 CalculateDestiny()
    {
        Vector3 offsetToApply;
        if (platformManager.CurrentVRPlatform == VRPlataform.PC)
        {
            offsetToApply = pullOrPush ? Camera.main.transform.TransformDirection(offset) : this.transform.TransformDirection(offset);
        }
        else
        {
            offsetToApply = pullOrPush ? Vector3.zero : this.transform.TransformDirection(offset);
        }                                 

        //Vector3 offsetToApply = pullOrPush ? Camera.main.transform.TransformDirection(offset) : this.transform.TransformDirection(offset);

        return GetTargetBasedOnPlatform() + offsetToApply;
    }

    public void SetDestiny(Vector3 destiny)
    {
        lerpPosB = destiny;
    }

    public void Finished()
    {
        OnExecuted?.Invoke(currentObjectBeingMoved.gameObject);
    }

    //returns camera for PC and hand for RV
    Vector3 GetTargetBasedOnPlatform()
    {
       if (platformManager.CurrentVRPlatform == VRPlataform.PC)
            return pullOrPush ? Camera.main.transform.position : this.transform.position;
       else
            return pullOrPush ? handsController.GetHandTransform(true).position : this.transform.position;
    }

    public void SetOffset(Vector3 newOffset)
    {
        this.offset = newOffset;
    }

    public void SetOffsetY(float param)
    {
        Vector3 newOffset = this.offset;
        newOffset.y = param;
        this.offset = newOffset;
    }

    public void SetOffsetZ(float param)
    {
        Vector3 newOffset = this.offset;
        newOffset.z = param;
        this.offset = newOffset;
    }
}
