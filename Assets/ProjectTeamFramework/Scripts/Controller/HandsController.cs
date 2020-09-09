using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;
using System;

public class HandsController : MonoBehaviour
{
    [SerializeField]
    GameObject currentObjectOnRightHand, currentObjectOnLeftHand;
    Transform rightHandPosition, leftHandPosition;    
   // VRRaycaster vrRaycaster;
    FirstPersonController fps;
    public const float minDistanceToEnableInteraction = 3;
    public GameObject vrMode;
    float countDown;
    
    PlatformManager platformManager;
    public bool viewBlocked;

    protected VRInput m_VrInput;       
     
    protected Transform player;

    event Action<GameObject> OnObjectGrabbed;
    event Action<GameObject> OnObjectDropped;

    private void Awake()
    {
       // vrRaycaster = ReferenceManagerIndependent.Instance.VRRaycaster;
        fps = (FirstPersonController)FindObjectOfType(typeof(FirstPersonController));
        m_VrInput = ReferenceManagerIndependent.Instance.VRInput;
        platformManager = ReferenceManagerIndependent.Instance.PlatformManager;
        player = ReferenceManagerIndependent.Instance.Player;        
    }

    private void Start()
    {
        if (platformManager.CurrentVRPlatform != VRPlataform.PC)
        {
            rightHandPosition = ReferenceManagerIndependent.Instance.PlatformManager.GetRightHand();
            leftHandPosition = ReferenceManagerIndependent.Instance.PlatformManager.GetLeftHand();
        }
    }

    bool pauseRotation = false;

    protected void OnEnable()
    {
        m_VrInput.OnClick += ProcessHand;
        m_VrInput.OnUp += OnGrabUp;
    }

    protected void OnDisable()
    {
        m_VrInput.OnClick -= ProcessHand;
        m_VrInput.OnUp -= OnGrabUp;
    }

    private void Update()
    {
        if(platformManager.CurrentVRPlatform == VRPlataform.PC)
            HandlePause();    

    }

    void HandlePause()
    {        
        if (Input.GetKeyDown(KeyCode.M))
            Cursor.visible = !Cursor.visible;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.P))
        {
            pauseRotation = !pauseRotation;
            fps.enabled = !pauseRotation;
        }
        
    }

    private void OnGrabUp()
    {
        if (platformManager.CurrentVRControlScheme == VRControlScheme.Laser)
            return;
		ProcessHand();
    }

    void ProcessHand()
    {
        if (currentObjectOnRightHand)
        {
            ProcessObjectInHand(currentObjectOnRightHand, true);  
        }
        // if (currentObjectOnLeftHand)
        //     ProcessHand(false);//mao esquerda
    }

    public void ProcessObjectInHand(GameObject goInHand, bool isHandRight)
    {
        PushSelector currentPushSelectorObject = ((VRRaycaster)FindObjectOfType(typeof(VRRaycaster))).CurrentInteractible?.GetComponent<PushSelector>();
        if (currentPushSelectorObject)
        {
            if (CheckIfMinDistance(currentPushSelectorObject.transform.position))
            {
                Debug.Log("Too far to drop object:" + Vector3.Distance(player.position, currentPushSelectorObject.transform.position));
#if NESTLE_RV
                ReferenceManagerDependent.Instance.LongeFrame.SetActive(true);
#endif
                return;
            }
            currentPushSelectorObject.ActivatePushing(goInHand);

        }
        else
        {
            if (goInHand)
            {
                DropObject(goInHand.transform, false, null);

            }

        }
        SetObjectOnHand(null, isHandRight);
    }

    bool CheckIfMinDistance(Vector3 objectPosition)
    {
        return Vector3.Distance(player.position, objectPosition ) > minDistanceToEnableInteraction;
    }

    public void GrabObject(Transform objectToGrab)
    {
        objectToGrab.parent = platformManager.CurrentVRPlatform == VRPlataform.PC ?
            Camera.main.transform.GetComponentInChildren<Canvas>().transform : 
            GetHandTransform(true);
        
        ChangeObjectInHandPhysics(objectToGrab, true);
    }

    public void DropObject(Transform objectToGrab, bool fixateObject, Transform dropTarget)
    {
        objectToGrab.parent = fixateObject ? dropTarget : null;
        
        ChangeObjectInHandPhysics(objectToGrab, fixateObject);

        OnObjectDropped?.Invoke(objectToGrab.gameObject);
    }


    void ChangeObjectInHandPhysics(Transform objectToGrab, bool fixateObject)
    {
        Rigidbody r = objectToGrab.GetComponent<Rigidbody>();
        if (r)
        {            
           
            r.isKinematic = fixateObject;
            r.useGravity = !fixateObject;
            
        }
        objectToGrab.GetComponent<Collider>().enabled = !fixateObject;
    }

    public GameObject GetObjectOnHand(bool isHandRight)
    {
        return isHandRight ? currentObjectOnRightHand : currentObjectOnLeftHand;
    }

    public void SetObjectOnHand(GameObject go, bool isHandRight)
    {
        if (isHandRight)
        {            
            currentObjectOnRightHand = go;            
        }
        else
        {            
            currentObjectOnLeftHand = go;
        }
        if (go)
        {
            GrabObject(go.transform);
            OnObjectGrabbed?.Invoke(go);
        }
    }


    public bool CheckIfObjectIsInEitherHands(GameObject check)
    {
        return check == currentObjectOnRightHand || check == currentObjectOnLeftHand;
    }

    public Transform GetHandTransform(bool isRightHand)
    {
        return isRightHand ? rightHandPosition : leftHandPosition;
    }

    public Vector3 GetHandPosition(bool isRightHand)
    {
        return isRightHand ? rightHandPosition.position : leftHandPosition.position;
    }

}
