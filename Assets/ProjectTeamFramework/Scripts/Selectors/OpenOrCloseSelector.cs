using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

using UnityEngine.UI;

[RequireComponent(typeof(Animation))]
//Usada para ativar e desativar um objeto a partir do clique nele
public class OpenOrCloseSelector : ClickOrAimAndHoldSelector, IOpenable, IClosable
{
    public event Action<GameObject> OnOpen, OnClose;
           
    private Animation anim;
    string animationName;
    [SerializeField]
    bool isOpen = false;
    float lastNormalizedTime;

    override protected void Start()
    {
        base.Start();
        PrepareAnimation();
    }

    void PrepareAnimation()
    {
        anim = GetComponent<Animation>();
        animationName = anim.clip.name;
        foreach (AnimationState state in anim)
        {
            animationName = state.name;
        }
        anim[animationName].speed = 0;
    }

    override protected void Update()
    {
        float normalizedTime = ReturnNormalizedTime();
        anim[animationName].time = Mathf.Clamp(normalizedTime, 0, 0.99f);
        if (normalizedTime != lastNormalizedTime)
        {
            anim.Play(animationName);
        }

        base.Update();

        lastNormalizedTime = normalizedTime;

    }


    protected override void OnFinish()
    {
        isOpen = !isOpen;
        if(isOpen)
            OnOpen?.Invoke(this.gameObject);
        else
            OnClose?.Invoke(this.gameObject);
        base.Finished(this.gameObject);
    }

    override public float ReturnNormalizedTime()
    {
        float counter = isOpen ? TimeToHold - Count : Count;
        return counter / TimeToHold;
    }

    public bool GetIsOpen() { return isOpen; }

    public void SetIsOpen(bool value) { isOpen = value;}
}

