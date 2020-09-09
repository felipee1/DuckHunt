using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

//Usada para ativar e desativar um objeto a partir do clique nele
public class ActivateSelector : ClickOrAimAndHoldSelector
{         

    override public float ReturnNormalizedTime()
    {
        return Count / TimeToHold;
    }
}

