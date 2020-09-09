using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

/// <summary>Used to rotate GUI around itself when mouse is hovering it</summary>
public class SpinUIImage : BaseSelector {

    public float angularSpeed = 5f;
    public LerpEquationTypes lerp;
    public float initialZ = 0, finalZ = -360;
    float currentSpeed = 0;
    float timeCounter;
    RectTransform myRect;

    // Use this for initialization
    protected override void Awake () {
        base.Awake();
        myRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        timeCounter += Time.deltaTime * currentSpeed;
        myRect.localRotation = Quaternion.Euler(0,0,lerp.Lerp(initialZ, finalZ, timeCounter));// Rotate(0, 0, currentSpeed * Time.deltaTime, Space.Self);
        if (timeCounter > 1)
        {
            OnFinish();
            timeCounter = 0;
        }
    }

    public override void OnInteractionTrigger(InteractionModes mode)
    {
        switch (mode)
        {
            case InteractionModes.Over:
                currentSpeed = angularSpeed;
                break;
            case InteractionModes.Out:
            default:
                currentSpeed = 0;
                break;
        }
    }

    protected override void OnFinish()
    {
        base.Finished(this.gameObject);
    }
}
