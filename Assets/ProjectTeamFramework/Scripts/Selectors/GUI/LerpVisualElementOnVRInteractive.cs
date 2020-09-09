using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class LerpVisualElementOnVRInteractive : LerpVisualElement
{    
    [SerializeField]
    VRRay [] vrRaycaster;

    async override protected void Awake()
    {
        base.Awake();
        await new WaitForEndOfFrame();       
    }

    async override protected void OnEnable()
    {
        base.OnEnable();
        await new WaitForSeconds(0.2f);
        for (int i = 0; i < vrRaycaster.Length; i++)
        {
            vrRaycaster[i].OnOverSomething += OnOver;
            vrRaycaster[i].OnOutOfEverything += OnOut;
        }       
    }

    override protected void OnDisable()
    {
        base.OnDisable();
        for (int i = 0; i < vrRaycaster.Length; i++)
        {
            vrRaycaster[i].OnOverSomething -= OnOver;
            vrRaycaster[i].OnOutOfEverything -= OnOut;
        }
    }

    void OnOver(VRInteractiveItem interactiveItem)
    {
        HandleOver();
    }

    void OnOut(VRInteractiveItem interactiveItem)
    {
        HandleOut();
    }

}
