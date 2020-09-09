using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

using TMPro;

//Base class to make scripts to detect interaction in a gameobject
public abstract class BaseSelector : MonoBehaviour, IInteractive, IFinishable
{
    public event Action<GameObject> OnExecuted;

    protected ReferenceManagerIndependent referenceManagerIndependent;
    protected ReferenceManagerDependent referenceManagerDependent;

    [SerializeField] protected VRInteractiveItem m_InteractiveItem;
        
    public VRInteractiveItem InteractiveItem { get => m_InteractiveItem; set => m_InteractiveItem = value; }

    protected virtual void Awake()
    {
        if (InteractiveItem == null)
            InteractiveItem = GetComponentInChildren<VRInteractiveItem>();
        InteractiveItem.IncludeNewTarget(this);
        referenceManagerIndependent = ReferenceManagerIndependent.Instance;
        referenceManagerDependent = ReferenceManagerDependent.Instance;
    }

    //Implement this to call Finished for each Selector
    protected abstract void OnFinish();

    protected virtual void Finished(GameObject go)
    {
        //Debug.Log("invoke");
        OnExecuted?.Invoke(go);
    }

    public abstract void OnInteractionTrigger(InteractionModes mode);
}

