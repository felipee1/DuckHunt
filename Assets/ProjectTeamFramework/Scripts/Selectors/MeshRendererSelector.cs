using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using Newtonsoft.Json.Utilities;


//Usada para ativar e desativar a parte visual de um modelo 3d, a partir do clique em um objeto
public class MeshRendererSelector : BaseSelector
{
    public MeshRenderer target;
    [Tooltip("Use to dinamically find target")]
    public string targetName;
    [Tooltip("True to activate and false do deactivate target")]
    public bool willActivate;
    [Tooltip("True to enable undo")]
    public bool rollBack = false;
    bool timeToRollback = false;

    void Start()
    {
        //if (target == null)
         //   target = GameObject.Find(targetName).GetComponent<typeof(target)>();
    }

    public override void OnInteractionTrigger(InteractionModes mode)
    {        
        target.enabled = (rollBack ? (timeToRollback ? !willActivate : willActivate) :  willActivate);
        timeToRollback = !timeToRollback;
        OnFinish();
    }

    public void SetTargetName(string s)
    {
        targetName = s;
    }

    protected override void OnFinish()
    {
        base.Finished(this.gameObject);
    }
}

