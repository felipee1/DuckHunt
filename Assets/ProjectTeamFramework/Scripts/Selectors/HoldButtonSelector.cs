using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

using UnityEngine.UI;


enum buttonHoldingStates
{
    notstarted = 0,
    holdStart = 1,
    holdOver = 2
}


public class HoldButtonSelector : BaseSelector
{
    public event Action<GameObject> OnHoldStart, OnHoldOver, OnFail;

    public float totalHoldDuration = 3;
    public float afterHoldMaxTime = -1;
    protected float countDown, holdingTime;
    
    private Image m_Selection;
    buttonHoldingStates currentState = buttonHoldingStates.notstarted;

    private void Start()
    {
        m_Selection = ReferenceManagerIndependent.Instance.SelectionRadialSlider;
    }       

    protected void OnDisable()
    {
        if (m_Selection && m_Selection.gameObject)
            m_Selection.gameObject.SetActive(false);
    }

    //QUANDO O BOTAO É ATIVADO, ESSA FUNCAO EH CHAMADA. AQUI VOCE DEVE PROGRAMAR O SEU CODIGO PARA O SEU BOTAO...
    override public void OnInteractionTrigger(InteractionModes mode)
    {
        switch (mode)
        {
            case InteractionModes.Out:
            case InteractionModes.Up:
                switch (currentState)
                {
                    case buttonHoldingStates.notstarted:
                    default:
                        break;
                    case buttonHoldingStates.holdStart:
                        TreatFailing();
                        break;
                    case buttonHoldingStates.holdOver:
                        currentState = buttonHoldingStates.notstarted;
                        OnFinish();
                        break;
                }
                m_Selection.gameObject.SetActive(false);
                break;
            case InteractionModes.Click:
                currentState = buttonHoldingStates.notstarted;
                switch (currentState)
                {
                    case buttonHoldingStates.notstarted:
                    default:
                        countDown = Time.deltaTime;
                        OnHoldStart?.Invoke(this.gameObject);
                        currentState = buttonHoldingStates.holdStart;
                        break;
                    case buttonHoldingStates.holdStart:
                        break;
                    case buttonHoldingStates.holdOver:
                        break;
                }
                break;
        }
        
    }

    private void Update()
    {
        if (!m_InteractiveItem.IsOver)
            return;
        switch (currentState)
        {
            case buttonHoldingStates.notstarted:
            default:
                break;
            case buttonHoldingStates.holdStart:
                countDown += Time.deltaTime;
                if (countDown >= totalHoldDuration)
                {
                    OnHoldOver?.Invoke(this.gameObject);
                    holdingTime = Time.deltaTime;
                    currentState = buttonHoldingStates.holdOver;
                    if(afterHoldMaxTime < 0)
                    {
                        OnInteractionTrigger(InteractionModes.Out);
                    }
                }
                break;
            case buttonHoldingStates.holdOver:
                holdingTime += Time.deltaTime;
                m_Selection.gameObject.SetActive(true);
                m_Selection.fillAmount = holdingTime / afterHoldMaxTime;
                
                if (holdingTime >= afterHoldMaxTime)
                {
                    TreatFailing();
                    OnFinish();
                }
                break;
        }
    }
    void TreatFailing()
    {
        OnFail?.Invoke(this.gameObject);
    }

    protected override void OnFinish()
    {
        base.Finished(this.gameObject);
    }
}

