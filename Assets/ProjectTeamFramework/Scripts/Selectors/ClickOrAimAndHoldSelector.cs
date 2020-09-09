using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;
using TMPro;

//Classe abstrata para detectar cliques em objetos
public abstract class ClickOrAimAndHoldSelector : BaseSelector, ITimeCounter
{
    [SerializeField] float timeToHold = 0.001f; //o tempo necessario para segurar o botao para concluir a ação (0 seria instantaneo)

    [SerializeField] bool cumulative = false;
    bool increasingNow = false;
    float count;

    private Image imgSelection;

    public float Count { get => count; set => count = value; }
    public Image ImgSelection { get => imgSelection; set => imgSelection = value; }
    public float TimeToHold { get => timeToHold; set => timeToHold = value; }

    protected virtual void Start()
    {
        ImgSelection = ReferenceManagerIndependent.Instance.SelectionRadialSlider;
    }

    protected void OnDisable()
    {     
        TurnOffImageSelection();
    }

    protected virtual void Update()
    {
        ProcessUpdate();
    }

    public override void OnInteractionTrigger(InteractionModes mode)
    {
        switch (mode)
        {
            case InteractionModes.Over:
            case InteractionModes.Click:
                ProcessClick();
                break;
            case InteractionModes.Out:            
            case InteractionModes.Up:
                ProcessStop();
                break;
            case InteractionModes.Grab:
                break;
            case InteractionModes.Release:
                break;
            default:
                break;
        }
    }     

    public void ProcessClick()
    {
        count = Time.deltaTime + (cumulative ? count : 0);
        increasingNow = true;
    }

    public void ProcessStop()
    {
        if(!cumulative)
            count = 0;
        increasingNow = false;
        TurnOffImageSelection();
    }

    public void ProcessUpdate()
    {
        // Setup the radial to have no fill at the start and hide if necessary.
        if (increasingNow)
        {
            count += Time.deltaTime;
            imgSelection.gameObject.SetActive(true);
            imgSelection.fillAmount = count / timeToHold;
        }

        if (count >= timeToHold)
        {
            count = 0;
            increasingNow = false;
            OnFinish();
        }
    }

    protected override void OnFinish()
    {
        base.Finished(this.gameObject);
    }

    public void TurnOffImageSelection()
    {
        if (imgSelection && imgSelection.gameObject)
        {
            imgSelection.gameObject.SetActive(false);
            imgSelection.fillAmount = 0;
        }
    }

    public abstract float ReturnNormalizedTime();
}

