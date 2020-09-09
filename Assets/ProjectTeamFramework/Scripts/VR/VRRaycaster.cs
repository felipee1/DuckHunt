using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace VRStandardAssets.Utils
{
    // In order to interact with objects in the scene
    // this class casts a ray into the scene and if it finds
    // a VRInteractiveItem it exposes it for other classes to use.
    // This script should be generally be placed on the camera or the user hands in VR.
    public class VRRaycaster : VRRay
    {
        [SerializeField]
        bool prioritezeUILayer = true;

        [SerializeField] protected Reticle m_Reticle;                     // The reticle, if applicable.

        [SerializeField] protected bool m_ShowDebugRay;                   // Optionally show the debug ray.
        [SerializeField] protected float m_DebugRayLength = 2f;           // Debug ray length.
        [SerializeField] protected float m_DebugRayDuration = 1f;         // How long the Debug ray will remain visible.
        [SerializeField] protected float m_RayLength = 15f;              // How far into the scene the ray is cast.

        LineRenderer m_LineRenderer = null; // For supporting Laser Pointer

        public Reticle GetReticle()
        {
            return m_Reticle;
        }

        override protected void OnEnable()
        {
            base.OnEnable();
            m_VrInput.OnClick += HandleClick;
        }

        override protected void OnDisable()
        {
            base.OnDisable();
            m_VrInput.OnClick -= HandleClick;
        }

        override protected void Start()
        {
            base.Start();
            m_LineRenderer = visibilityIndicator.GetComponent<LineRenderer>();
            if (!m_LineRenderer)
                Debug.LogError("Missing line renderer in VRRaycaster");
        }

        override protected void Update()
        {
            // Show the debug ray if required
            if (m_ShowDebugRay)
            {
                Debug.DrawRay(transform.position, transform.forward * m_DebugRayLength, Color.blue, m_DebugRayDuration);
            }
            
            base.Update();                             
        }


        override protected void RaycastScheme()
        {
            RayCastPosition();
        }


        void RayCastPosition()
        {
            Ray ray;

            Vector3 worldStartPoint;
            Vector3 worldEndPoint;

            worldStartPoint = transform.position;
            worldEndPoint = worldStartPoint + (transform.forward * m_RayLength);
            ray = new Ray(transform.position, transform.forward);

            CastRays(ray, worldStartPoint, worldEndPoint);
        }
        
        private void CastRays(Ray ray, Vector3 worldStartPoint, Vector3 worldEndPoint)
        {
            RaycastHit[] rayhits;
            if (prioritezeUILayer)
            {
                //first look for UI elements
                rayhits = Physics.SphereCastAll(ray, m_RayRadius, m_RayLength, m_UILayers);

                this.hits = rayhits.Select(r => r.collider).ToArray();//.Where(g => g != null).ToArray();

#if UNITY_EDITOR
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    foreach (var item in rayhits)
                    {
                        print(item.transform.name + "_" + item.distance);
                    }
                    print("_______________________");
                }

#endif

                // Do the raycast forwards to see if we hit an interactive item
                if (hits.Length > 0)
                {
                    int indexOfChosenCollider = ProcessCollisionHits(hits, worldStartPoint);

                    worldEndPoint = rayhits[indexOfChosenCollider].point == Vector3.zero ? 
                        worldStartPoint : rayhits[indexOfChosenCollider].point;

                    RenderLine(worldStartPoint, worldEndPoint);

                    if (m_Reticle)
                        m_Reticle.SetPosition(worldEndPoint);

                    return;
                }
            }


            //if there is no hit, then look for other colliders
            rayhits = Physics.SphereCastAll(ray, m_RayRadius, m_RayLength, ~m_ExclusionLayers);
            
            this.hits = rayhits.Select(r => r.collider).ToArray();//.Where(g => g != null).ToArray();


#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                foreach (var item in hits)
                {
                    print(item.transform.name);
                }
                print("_______________________");
            }
#endif

            // Do the raycast forwards to see if we hit an interactive item
            if (hits.Length > 0)
            {
                int indexOfChosenCollider = ProcessCollisionHits(hits, worldStartPoint);

                worldEndPoint = rayhits[indexOfChosenCollider].point == Vector3.zero ?
                    worldStartPoint : rayhits[indexOfChosenCollider].point;                
            }
            else
            {
                NoHits();
            }
             
            RenderLine(worldStartPoint, worldEndPoint);

            if (m_Reticle)
                m_Reticle.SetPosition(worldEndPoint);
        }


        protected void RenderLine(Vector3 worldStartPoint, Vector3 worldEndPoint)
        {
            if (m_LineRenderer != null && ShowVisibilityRenderer)
            {
                m_LineRenderer.enabled = ShowVisibilityRenderer;
                m_LineRenderer.SetPosition(0, worldStartPoint);
                m_LineRenderer.SetPosition(1, worldEndPoint);
            }
        }

    }
}