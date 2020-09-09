using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPositionAfterDropped : MonoBehaviour
{
    Transform originalParent;
    Transform lastParent;
    Vector3 originalPosition;
    Vector3 originalScale;
    Quaternion originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        originalParent = transform.parent;
        originalPosition = transform.position;
        originalScale = transform.localScale;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent == null && (transform.position - originalPosition).sqrMagnitude > 0.01f)
        {
            transform.parent = originalParent;
            transform.position = originalPosition;
            transform.localScale = originalScale;
            transform.rotation = originalRotation;
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if (rigidbody)
            {
                rigidbody.isKinematic = true;
            }
        }
    }
}
