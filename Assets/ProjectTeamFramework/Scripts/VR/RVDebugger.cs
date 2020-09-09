using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using TMPro;
using System;
using VRStandardAssets.Utils;

public class RVDebugger : MonoBehaviour
{
    public GameObject[] listOfGameObjects;

    public Camera[] listOfCameras;

    public MonoBehaviour[] listOfMonoBehaviors;

    VRRay [] VRRaycaster;

    public TextMeshProUGUI txt, txtLogger;
    string lastLog;
    public float counter;
    public string debugLogger;


    // Start is called before the first frame update
    void Start()
    {
        VRRaycaster = ReferenceManagerIndependent.Instance.VRRays;
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "";
        foreach (var item in listOfGameObjects)
        {
            txt.text += item.name + " is " + (item.activeInHierarchy ? "": "not ") + "active";
            txt.text += "\n";
        }

        foreach (var item in listOfMonoBehaviors)
        {
            txt.text += item.name + " is " + (item.enabled ? "" : "not ") + "enabled";
            txt.text += "\n";
        }
        foreach (var item in listOfCameras)
        {
            txt.text += item.name + " is " + (item.enabled ? "" : "not ") + "enabled";
            txt.text += "\n";
        }

        for (int i = 0; i < VRRaycaster.Length; i++)
        {
            txt.text += "Interactable: " + VRRaycaster[i].CurrentInteractible?.name;
            txt.text += "\n";
        }

        txt.text += "\n";

#if NESTLE_RV
        GameObject hand = ReferenceManagerDependent.Instance.HandsController.GetObjectOnHand(true);
        if (hand)
            txt.text += "Mao R: " + hand.name;
        txt.text += "\n";
#endif

        txtLogger.text = "Log: " + debugLogger;

        if(lastLog == debugLogger)
        {
            counter += Time.deltaTime;
            if (counter > 5)
            {
                debugLogger = "";
                counter = 0;
            }
        }
        else
            lastLog = debugLogger;



    }
}
