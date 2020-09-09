using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;


[RequireComponent(typeof(SkinnedMeshRenderer))]
public class BlendShapeSelector : BaseSelector
{
    public int blendShapeIndex = 0;
    public LerpEquationTypes lerp;
    public float totalLerpDuration = 1; 
    protected float countDownToTurnOff;
    float lerpFactor, lerpA, lerpB;
   
    SkinnedMeshRenderer skinnedMeshRenderer;     

    private void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    //QUANDO O BOTAO É ATIVADO, ESSA FUNCAO EH CHAMADA. AQUI VOCE DEVE PROGRAMAR O SEU CODIGO PARA O SEU BOTAO...
    public override void OnInteractionTrigger(InteractionModes modes)
    {
        switch (modes)
        {
            case InteractionModes.Over:
                break;
            case InteractionModes.Out:
                countDownToTurnOff = 0;
                skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, 0);
                break;
            case InteractionModes.Click:
                countDownToTurnOff = totalLerpDuration;
                lerpA = skinnedMeshRenderer.GetBlendShapeWeight(blendShapeIndex);
                lerpB = 100;
                break;
            case InteractionModes.DoubleClick:
                break;
            case InteractionModes.Up:
                countDownToTurnOff = totalLerpDuration;
                lerpA = skinnedMeshRenderer.GetBlendShapeWeight(blendShapeIndex);
                lerpB = 0;
                break;
            case InteractionModes.Grab:
                break;
            default:
                break;
        }
        
    }


    private void Update()
    {
        if(countDownToTurnOff > 0)
        {
            countDownToTurnOff -= Time.deltaTime;            
            lerpFactor = countDownToTurnOff / totalLerpDuration;
            float value = lerp.Lerp(lerpA, lerpB, 1 - lerpFactor);
            skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex,value);
            if (countDownToTurnOff < 0)
                OnFinish();
        }        
    }

    protected override void OnFinish()
    {
        base.Finished(this.gameObject);
    }
}

