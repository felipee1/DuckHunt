using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;


/// <summary>Usada para apagar um objeto da GUI, mexendo no seu alpha,
/// quando o mouse esta em cima dele.</summary>
[RequireComponent(typeof(VRInteractiveItem))]
public class LerpUIImageAlpha : BaseSelector {

    public float transitionInDuration = 0.5f, transitionOutDuration = 0.5f;
    public LerpEquationTypes lerp;

    float lerpFactor, lerpA, lerpB;
    float totalLerpDuration;
    Image myImageComponent;
    protected float countDownToTurnOff;
    Color myColor = new Color(0, 0, 0, 0);

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        myImageComponent = GetComponent<Image>();
        myColor = myImageComponent.color;
        if (m_InteractiveItem == null)
            m_InteractiveItem = GetComponent<VRInteractiveItem>();
        ResetButtonVisual();
    }

    void Update()
    {        
        if (countDownToTurnOff > 0)
        {
            countDownToTurnOff -= Time.deltaTime;
            lerpFactor = countDownToTurnOff / totalLerpDuration;
            myColor = myImageComponent.color;
            myColor.a = lerp.Lerp(lerpA, lerpB, 1 - lerpFactor);
            myImageComponent.color = myColor;
            if (countDownToTurnOff < 0)
                OnFinish();
        }
    }


    private void OnEnable()
    {
        ResetButtonVisual();
    }

    public override void OnInteractionTrigger(InteractionModes mode)
    {
        switch (mode)
        {
            case InteractionModes.Over:
                totalLerpDuration = transitionOutDuration;
                countDownToTurnOff = totalLerpDuration;
                lerpA = 0;
                lerpB = 1;
                myColor = myImageComponent.color;
                break;
            case InteractionModes.Out:
            default:
                // When the user looks away from the rendering of the scene, hide the radial.
                totalLerpDuration = transitionInDuration;
                countDownToTurnOff = totalLerpDuration;
                lerpA = 1;
                lerpB = 0;
                break;
        }
    }


    public void ResetButtonVisual()
    {
        countDownToTurnOff = 0;
        myColor = myImageComponent.color;
        myColor.a = 0;
        myImageComponent.color = myColor;
    }

    protected override void OnFinish()
    {
        base.Finished(this.gameObject);
    }
}
