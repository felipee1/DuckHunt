using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

using UnityStandardAssets.Characters.FirstPerson;

//Usada para telerportar um objeto a partir do clique em um objeto
public class TeleportObjectorSelector : BaseSelector
{
    public GameObject who;
    [Tooltip("Use to dinamically find who to teleport")]
    public string whoIsName;

    public GameObject where;
    [Tooltip("Use to dinamically find target")]
    public string whereName;

    public Vector3 teleportDestiny;

    VRCameraFade cameraFade;

    public float fadeTime = 0.25f;
    
    protected Transform player;

    public AudioClip audioClip;

    protected bool teleportIsOver = true;

    void Start()
    {
        if (who == null)
            who = GameObject.Find(whoIsName);
        if (who == null)
            Debug.LogError("Teleporte sem objeto a ser teleportado");

        if (where == null)
            where = GameObject.Find(whereName);
        if (where == null)
        {
            if (teleportDestiny == Vector3.zero)
                Debug.LogError("Teleporte sem alvo para ser teleportado");
        }
        else
            teleportDestiny = where.transform.position;
                
        player = referenceManagerIndependent.Player;
        cameraFade = ReferenceManagerIndependent.Instance.VRCameraFade;
    }

    async public override void OnInteractionTrigger(InteractionModes mode)
    {
        teleportIsOver = false;

        StartCoroutine(cameraFade.BeginFadeOut(fadeTime, false));

        if (audioClip)
            ReferenceManagerIndependent.Instance.PlayAudioClip(audioClip);

        CharacterController playerChar = who.GetComponent<CharacterController>();
        if (playerChar)
        {
            referenceManagerIndependent.PlatformManager.GetPlayerController().enabled = false;
            playerChar.enabled = false;
        }

        await new WaitForSeconds(fadeTime);
        who.transform.position = teleportDestiny;


        StartCoroutine(cameraFade.BeginFadeIn(fadeTime, false));

        if (playerChar)
        {
            referenceManagerIndependent.PlatformManager.GetPlayerController().enabled = true;
            playerChar.enabled = true;
        }

        teleportIsOver = true;

        OnFinish();
    }
    protected override void OnFinish()
    {
        base.Finished(this.gameObject);
    }
}

