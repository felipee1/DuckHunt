using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

//
//Usada para ativar e desativar uma animacao a partir do clique em um objeto
[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(OpenOrCloseSelector))]
public class AnimationOverTimeSelector : MonoBehaviour
{
    private Animation anim;
    string animationName;
    bool isOn = false;
    OpenOrCloseSelector openOrCloseSelectorAttached;

    // Start is called before the first frame update
    void Start()
    {
        openOrCloseSelectorAttached = GetComponent<OpenOrCloseSelector>();
        openOrCloseSelectorAttached.OnOpen += Finished;
        openOrCloseSelectorAttached.OnClose += Finished;
        anim = GetComponent<Animation>();
        animationName = anim.clip.name;
        foreach (AnimationState state in anim)
        {
            animationName = state.name;
        }
    }

    void Update()
    {
        anim[animationName].time = Mathf.Clamp(openOrCloseSelectorAttached.ReturnNormalizedTime(),0,0.99f);
        anim.Play(animationName);
    }

    void Finished(GameObject go)
    {

    }
}
