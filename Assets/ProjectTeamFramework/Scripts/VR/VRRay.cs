using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace VRStandardAssets.Utils
{
    public abstract class VRRay : MonoBehaviour
    {
        public event Action<RaycastHit> OnRaycasthit;                   // This event is called every frame that the user's gaze is over a collider.
        public event Action<VRInteractiveItem> OnOverSomething, OnOutOfEverything;


        [SerializeField] protected LayerMask m_UILayers;                  // Layers to prioritize
        [SerializeField] protected LayerMask m_ExclusionLayers;           // Layers to exclude from the raycast.        
        [SerializeField] protected VRInput m_VrInput;                     // Used to call input based events on the current VRInteractiveItem.
        
        [SerializeField] protected float m_RayRadius = 0.05f;              // Radius of the spherecast.

        [SerializeField] protected GameObject visibilityIndicator = null;  //
        public bool ShowVisibilityRenderer = true;                         // Laser or sphere pointer visibility
      
        [SerializeField]
        protected VRInteractiveItem m_CurrentInteractible;                //The current interactive item
        protected VRInteractiveItem m_LastInteractible;                   //The last interactive item

        protected Collider[] hits = new Collider[10];

        protected PlatformManager platformManager;        

        // Utility for other classes to get the current interactive item
        public VRInteractiveItem CurrentInteractible
        {
            get { return m_CurrentInteractible; }
        }

        // Utility for other classes to get the current interactive item
        public VRInteractiveItem LastInteractible
        {
            get { return m_LastInteractible; }
        }

        public void ResetInteractable()
        {
            m_CurrentInteractible = null;
        }


        // Start is called before the first frame update
        virtual protected void Start()
        {
            platformManager = ReferenceManagerIndependent.Instance.PlatformManager;
        }

        virtual protected void OnEnable()
        {
            m_VrInput.OnUp += HandleUp;
        }

        virtual protected void OnDisable()
        {
            m_VrInput.OnUp -= HandleUp;
        }

        // Update is called once per frame
        virtual protected void Update()
        {
            RaycastScheme();
        }

        abstract protected void RaycastScheme();

        //Returns the index of the vector of the chosen collider
        protected int ProcessCollisionHits(Collider[] hits, Vector3 worldStartPoint)
        {
            VRInteractiveItem interactible = null;
            Collider hit = hits[0];
            float closestInteractive = Mathf.Infinity;
            int returnValue = 0;
            for (int i = 0; i < hits.Length; i++)
            {
                VRInteractiveItem currentItem;

                if (hits[i].attachedRigidbody)
                    currentItem = hits[i].attachedRigidbody.GetComponent<VRInteractiveItem>();
                else
                    currentItem = hits[i].GetComponent<VRInteractiveItem>();
                float distance = Vector3.Distance(worldStartPoint, currentItem ? currentItem.transform.position : hits[i].transform.position);
                if (distance < closestInteractive && currentItem)
                {
                    closestInteractive = distance;
                    interactible = currentItem; //attempt to get the VRInteractiveItem on the hit object  
                    hit = hits[i];
                    returnValue = i;
                }
            }

#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
                print(hit.transform.name);
#endif

            m_CurrentInteractible = interactible;

            // If we hit an interactive item and it's not the same as the last interactive item, then call Over
            if (interactible && interactible != m_LastInteractible)
                HandleOver(interactible);

            // Deactive the last interactive item 
            if (interactible != m_LastInteractible)
                DeactiveLastInteractible();

            m_LastInteractible = interactible;

            return returnValue;
        }

        protected void NoHits()
        {
            m_CurrentInteractible = null;
            // Nothing was hit, deactive the last interactive item.
            DeactiveLastInteractible();
        }

      

        virtual public void SetActiveVisibilityIndicator(bool show)
        {
            ShowVisibilityRenderer = show;
            visibilityIndicator.SetActive(show);
        }

        protected void DeactiveLastInteractible()
        {
            if (m_LastInteractible == null)
                return;

            HandleOut(m_LastInteractible);
            m_LastInteractible = null;
        }

        protected void HandleUp()
        {
            if (m_CurrentInteractible != null)
                m_CurrentInteractible.Up();
        }

        protected void HandleClick()
        {
            if (m_CurrentInteractible != null)
            {
                m_CurrentInteractible.Click();
            }
        }

        protected void HandleOver(VRInteractiveItem interactible)
        {
            interactible.Over();
            if (m_LastInteractible == null)
                OnOverSomething?.Invoke(interactible);
        }

        protected void HandleOut(VRInteractiveItem interactible)
        {
            interactible.Out();
            if (m_CurrentInteractible == null)
                OnOutOfEverything?.Invoke(interactible);
        }
    }
}
