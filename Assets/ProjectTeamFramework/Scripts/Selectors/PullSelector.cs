using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

using System;

[RequireComponent(typeof(ObjectMoverAssistance))]
public class PullSelector : BaseSelector, IGetItemOnHand
{
    Transform playerController;

    HandsController handController;

    ObjectMoverAssistance objectBeingPushed;
        
    [SerializeField]
    bool rotateAfterPulling;
    [SerializeField]
    Vector3 objectRotationAfterPulling;
    
    private void Start()
    {
        playerController = ReferenceManagerIndependent.Instance.Player;
        handController = ReferenceManagerDependent.Instance.HandsController;
        objectBeingPushed = GetComponent<ObjectMoverAssistance>();
        objectBeingPushed.enabled = false;
    }
    
    async public override void OnInteractionTrigger(InteractionModes mode)
    {
        if (CheckIfMinDistance())
        {
#if NESTLE_RV
            ReferenceManagerDependent.Instance.LongeFrame.SetActive(true);
#endif
            Debug.Log("Too far to get object:" + Vector3.Distance(playerController.transform.position, this.transform.position));
            return;
        }
        await new WaitForEndOfFrame();
        handController.SetObjectOnHand(objectBeingPushed.gameObject, true);

        objectBeingPushed.enabled = true;
        objectBeingPushed.ActivateMover(this.gameObject, true);
        objectBeingPushed.OnExecuted += Finished;        
    }

    bool CheckIfMinDistance()
    {
        return Vector3.Distance(playerController.transform.position, this.transform.position) > HandsController.minDistanceToEnableInteraction;
    }

    protected override void Finished(GameObject go)
    {
        SendMessage("DuplicateSizeInVR", this, SendMessageOptions.DontRequireReceiver);
        objectBeingPushed.OnExecuted -= Finished;
        objectBeingPushed.enabled = false;
        if(rotateAfterPulling)
            this.transform.localEulerAngles = objectRotationAfterPulling;
        base.Finished(go);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Chao")
        {
#if NESTLE_RV
            Instantiate(ReferenceManagerDependent.Instance.prefabParticleFallOnGround, collision.GetContact(0).point, ReferenceManagerDependent.Instance.prefabParticleFallOnGround.transform.rotation);
            referenceManagerDependent.SimulationStateController.FailInState("Tente não soltar objetos no chão.", ErrorType.droppedObject);
#endif
            if (handController.GetObjectOnHand(true) == null)
            {
                if (referenceManagerIndependent.PlatformManager.CurrentVRPlatform == VRPlataform.PC)
                {                    
                    handController.SetObjectOnHand(objectBeingPushed.gameObject, true);
                    objectBeingPushed.enabled = true;
                    objectBeingPushed.ActivateMover(this.gameObject, true);
                    objectBeingPushed.OnExecuted += Finished;                    
                }
                else
                {
                    this.transform.position = handController.GetHandPosition(true);
                    handController.SetObjectOnHand(this.gameObject, true);
                }                
            }
        }
    }

    protected override void OnFinish()
    {
        Finished(this.gameObject);
    }
}
