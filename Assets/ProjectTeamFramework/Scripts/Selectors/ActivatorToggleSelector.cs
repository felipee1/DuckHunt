using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

using UnityEngine.UI;

//Usada para ativar e desativar um objeto a partir do clique nele
public class ActivatorToggleSelector : BaseSelector
{

    private Toggle toggle;

    public override void OnInteractionTrigger(InteractionModes mode)
    {
        // pegar toogle
        toggle = transform.GetComponent<Toggle>();
        if (!toggle.isOn)
        {
            toggle.isOn = !toggle.isOn;
        }
        // ativar toogle e desativor o parent no mesmo nível
        //Debug.Log(toggle.isOn);
    }

    protected override void OnFinish()
    {
        base.Finished(this.gameObject);
    }
}

