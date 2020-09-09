using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

//
//Usada para ativar e desativar uma animacao a partir do clique em um objeto
[RequireComponent(typeof(Animation))]
public class AnimationSelector : BaseSelector
{
    private Animation anim;
    string animationName;
    bool currentlyPlaying = false;
    bool isOn;
    public bool interactAgainToRevert = false;

    public bool IsOn(){ return isOn; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        animationName = anim.clip.name;
        foreach (AnimationState state in anim)
        {
            animationName = state.name;
        }
    }

    public override void OnInteractionTrigger(InteractionModes mode)
    {
        if(!interactAgainToRevert)
        {
            if(!currentlyPlaying)
            {
                currentlyPlaying = true;
                anim[animationName].speed = 1;
                anim.Play(animationName);
            }
        }
        {
            if (!currentlyPlaying)
            {
                currentlyPlaying = true;
                anim[animationName].speed = isOn ? -1 : 1;
                if (isOn)
                {
                    anim[animationName].time = anim.isPlaying ? anim[animationName].time : anim[animationName].length;
                }
                else
                {
                    anim[animationName].time = 0;
                }
                anim.Play(animationName);
            }            
        }
    }

    private void Update()
    {
        if(currentlyPlaying && !anim.isPlaying)
        {
            currentlyPlaying = false;
            isOn = !isOn;
            OnFinish();
        }
    }

    protected override void OnFinish()
    {
        base.Finished(this.gameObject);
    }
}
