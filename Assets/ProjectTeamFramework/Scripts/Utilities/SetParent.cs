using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParent : MonoBehaviour
{
    public Transform newParent;

    public void SetNewParent()
    {
        transform.parent = newParent;
    }
}
