using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;


//Usada para trocar de skybox a partir do clique em um objeto
public class SkyboxSelector : BaseSelector
{
    public Material newSkybox;
    Material oldSkybox;
    bool isOn = false;

    void Start()
    {
        oldSkybox = RenderSettings.skybox;
    }

    public override void OnInteractionTrigger(InteractionModes mode)
    {
        isOn = !isOn;
        RenderSettings.skybox = isOn ? newSkybox : oldSkybox;
        OnFinish();
    }

    protected override void OnFinish()
    {
        base.Finished(this.gameObject);
    }
}

