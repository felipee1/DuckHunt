using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsControls : MonoBehaviour
{
    public Transform rightFingerIndicator;
    [SerializeField] private Transform m_TrackingSpace = null;   // Tracking space (for line renderer)
    public float speed = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (rightFingerIndicator.position - transform.position).normalized * speed * Time.deltaTime;
    }

    public Vector3 WorldRightHandPoint
    {
        get
        {
            if (rightFingerIndicator == null)
                rightFingerIndicator = GameObject.Find("rightfingerindicator").transform;
            if (rightFingerIndicator)
                return rightFingerIndicator.position;
            else
            {
                Vector3 localStartPoint = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
                Matrix4x4 localToWorld = m_TrackingSpace.localToWorldMatrix;
                Vector3 worldStartPoint = localToWorld.MultiplyPoint(localStartPoint);
                return worldStartPoint;
            }
        }
    }
}
