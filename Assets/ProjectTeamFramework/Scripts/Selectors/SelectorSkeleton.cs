using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class SelectorSkeleton : BaseSelector
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Implement the interaction desired here
    public override void OnInteractionTrigger(InteractionModes mode)
    {
        //My code goes here...


        OnFinish();
    }


    protected override void OnFinish()
    {
        base.Finished(this.gameObject);
    }
}
