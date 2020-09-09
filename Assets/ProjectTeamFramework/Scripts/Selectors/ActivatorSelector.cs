using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;


//Usada para ativar e desativar um objeto a partir do clique nele
public class ActivatorSelector : BaseSelector
{
    public GameObject target;
    [Tooltip("Use to dinamically find target")]
    public string targetName;
    [Tooltip("True to activate and false do deactivate target")]
    public bool willActivate;
    [Tooltip("True to enable undo")]
    public bool rollBack = false;
    bool timeToRollback = false;

    public float delayToProcess = 0;

    void Start()
    {
        if (target == null)
            target = GameObject.Find(targetName);
    }


    async public override void OnInteractionTrigger(InteractionModes mode)
    {
        if (delayToProcess > 0)
            await new WaitForSeconds(delayToProcess);
        target.SetActive(rollBack ? (timeToRollback ? !willActivate : willActivate) : willActivate);
        timeToRollback = !timeToRollback;
        OnFinish();
    }

    protected override void OnFinish()
    {
        base.Finished(this.gameObject);
    }
}

