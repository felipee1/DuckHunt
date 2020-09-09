using System;
using TMPro;
using UnityEngine;

namespace VRStandardAssets.Utils
{
    // In order to interact with objects in the scene
    // this class casts a OverlapSphere into the scene and if it finds
    // a VRInteractiveItem it exposes it for other classes to use.
    // This script should be generally be placed on the user hands.
    public class VRRaygrabber : VRRay
    {

        override protected void OnEnable()
        {
            base.OnEnable();
            m_VrInput.OnGrab += HandleGrab;
        }

        override protected void OnDisable()
        {
            base.OnDisable();
            m_VrInput.OnGrab -= HandleGrab;
        }

        override protected void RaycastScheme()
        {
            HandGrabber();
        }      

        private void HandGrabber()
        {     
            Vector3 worldEndPoint = visibilityIndicator.transform.position;

            this.hits = Physics.OverlapSphere(worldEndPoint, m_RayRadius);
           
            // Do the raycast forwards to see if we hit an interactive item
            if (hits.Length > 0)
            {
                ProcessCollisionHits(hits, worldEndPoint);
                return;
            }
            else
            {
                NoHits();
            }
        }


        private void HandleGrab()
        {
            if (m_CurrentInteractible != null)
                m_CurrentInteractible.Grab();
        }
    }
}