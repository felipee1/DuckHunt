using System;
using TMPro;
using UnityEngine;

namespace VRStandardAssets.Utils
{
    // This class encapsulates all the input required for most VR games.
    // It has events that can be subscribed to by classes that need specific input.
    // This class must exist in every scene and so can be attached to the main
    // camera for ease.
    public class VRInput : MonoBehaviour
    {
        //Swipe directions
        public enum SwipeDirection
        {
            NONE,
            UP,
            DOWN,
            LEFT,
            RIGHT
        };


        public event Action<SwipeDirection> OnSwipe;                // Called every frame passing in the swipe, including if there is no swipe.
        public event Action OnClick;                                // Called when Fire1 is released and it's not a double click.
        public event Action OnDown;                                 // Called when Fire1 is pressed.
        public event Action OnUp;                                   // Called when Fire1 is released.
        public event Action OnCancel;                               // Called when Cancel is pressed.
        public event Action OnGrab;                                 // Called when hand trigger is pressed.


        [SerializeField] private float m_SwipeWidth = 0.3f;         //The width of a swipe

        
        private Vector2 m_MouseDownPosition;                        // The screen position of the mouse when Fire1 is pressed.
        private Vector2 m_MouseUpPosition;                          // The screen position of the mouse when Fire1 is released.
        private float m_LastMouseUpTime;                            // The time when Fire1 was last released.
        private float m_LastHorizontalValue;                        // The previous value of the horizontal axis used to detect keyboard swipes.
        private float m_LastVerticalValue;                          // The previous value of the vertical axis used to detect keyboard swipes.
        float lastTriggerValue, lastHandTriggerValue;

        PlatformManager platformManager;

        private void Start()
        {
            platformManager = ReferenceManagerIndependent.Instance.PlatformManager;
        }

        private void Update()
        {      
            CheckInput();
        }


        private void CheckInput()
        {

            float trigger = 0;
            if (platformManager.CurrentVRControlScheme == VRControlScheme.Laser)
                trigger = Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger");
            else
                trigger = Input.GetAxis("Oculus_CrossPlatform_SecondaryHandTrigger");

            // This if statement is to gather information about the mouse when the button is up.
            if ((trigger == 0 && lastTriggerValue != 0) ||
               platformManager.CurrentVRPlatform == VRPlataform.PC && Input.GetButtonUp("Click")
               || Application.platform == RuntimePlatform.WindowsEditor && Input.GetButtonUp("Click"))
            {           
                // If anything has subscribed to OnUp call it.
                if (OnUp != null)
                    OnUp();
            }
            
            // This if statement is to trigger events based on the information gathered before.
            if ((trigger != 0 && lastTriggerValue == 0 ) ||
                platformManager.CurrentVRPlatform == VRPlataform.PC && Input.GetButtonDown("Click")
                || Application.platform == RuntimePlatform.WindowsEditor && Input.GetButtonDown("Click"))
            {
                if (platformManager.CurrentVRControlScheme == VRControlScheme.Laser)
                {
                    // If anything has subscribed to OnClick call it.
                    if (OnClick != null)
                        OnClick();
                }
                else
                {
                    // If anything has subscribed to OnGrab call it.
                    if (OnGrab != null)
                        OnGrab();
                }
            }

            lastTriggerValue = trigger;

        }

        private void OnDestroy()
        {
            // Ensure that all events are unsubscribed when this is destroyed.
            OnSwipe = null;
            OnClick = null;
            OnDown = null; 
            OnUp = null;
            OnGrab = null;
        }
    }
}