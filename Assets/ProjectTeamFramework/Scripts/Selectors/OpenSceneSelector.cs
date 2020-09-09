using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VRStandardAssets.Utils;
using UnityEngine.SceneManagement;
using TMPro;


public class OpenSceneSelector : ClickOrAimAndHoldSelector
{
    public float lerpTime = 0.5f;
    public LerpEquationTypes lerp;
    VRCameraFade cameraFade;
    [SerializeField] private string m_SceneToLoad;                      // The name of the scene to load.
 
    override protected void Start()
    {       
        base.Start();
        cameraFade = referenceManagerIndependent.VRCameraFade;
    }

    override public float ReturnNormalizedTime()
    {
        return Count / TimeToHold;        
    }

    
    //QUANDO O BOTAO É ATIVADO, ESSA FUNCAO EH CHAMADA. AQUI VOCE DEVE PROGRAMAR O SEU CODIGO PARA O SEU BOTAO...
    protected override void OnFinish()
    {
        StartCoroutine(ChangeSceneWithFadeCamera());
        base.Finished(this.gameObject);
    }       

    public IEnumerator ChangeSceneWithFadeCamera()//para fazer o fade-in fade-out da camera entre teleporte
    {
        yield return StartCoroutine(cameraFade.BeginFadeOut(lerpTime, false));
        SceneManager.LoadScene(m_SceneToLoad, LoadSceneMode.Single);
    }

}


