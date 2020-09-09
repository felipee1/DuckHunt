using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class PrintOnConsoleSelector : BaseSelector
{
    public override void OnInteractionTrigger(InteractionModes mode)
    {      
        print($"Interacted here: {mode} with {this.gameObject.name} !");
        OnFinish();
        //Do something specific
        switch (mode)
        {
            case InteractionModes.Over:
                break;
            case InteractionModes.Out:
                break;
            case InteractionModes.Click:
                break;
            case InteractionModes.DoubleClick:
                break;
            case InteractionModes.Up:
                break;
            case InteractionModes.Grab:
                break;
            case InteractionModes.Release:
                break;
            default:
                break;
        }
    }

    protected override void OnFinish()
    {
        base.Finished(this.gameObject);
    }
}