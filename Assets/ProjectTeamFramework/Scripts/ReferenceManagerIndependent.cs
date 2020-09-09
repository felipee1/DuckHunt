using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using VRStandardAssets.Utils;

public class ReferenceManagerIndependent : SingletonGameObject<ReferenceManagerIndependent>
{

    #region application_independent
    Image selectionRadialSlider;    
    VRCameraFade vrCameraFade;
    Transform player;
    PlatformManager platformManager;    
    VRRay [] vrRays;    
    VRInput vrInput; 

    public Image SelectionRadialSlider { get => selectionRadialSlider;}    
    public VRCameraFade VRCameraFade { get => vrCameraFade;}
    public Transform Player { get => player; }
    public PlatformManager PlatformManager { get => platformManager; }
    
    public VRRay [] VRRays { get => vrRays; }

    public void UpdateVRObjects()
    {
        FindAssets();
    }
    
    public VRInput VRInput { get => vrInput; }


    [SerializeField] AudioSource audioSource;

    #endregion
    

    void FindAssets()
    {
        ApplicationIndependent();     
    }
    
    void ApplicationIndependent()
    {
        platformManager = (PlatformManager)FindObjectOfType(typeof(PlatformManager));
        if (!PlatformManager)
            Debug.LogError("PlatformManager not found");

        if(platformManager.CurrentVRPlatform == VRPlataform.PC)
            selectionRadialSlider = Extensions.FindEvenInactive("UISelectionBarPC")?.GetComponent<Image>();
        else
            selectionRadialSlider = Extensions.FindEvenInactive("UISelectionBarRV")?.GetComponent<Image>();

        if (!SelectionRadialSlider)
            Debug.LogError("UISelectionBar not found");
        //SelectionRadialSlider.gameObject.SetActive(false);

        vrCameraFade = (VRCameraFade)FindObjectOfType(typeof(VRCameraFade));
        if (!VRCameraFade)
            Debug.LogError("VRCameraFade not found");

        player = Extensions.FindAndTurnOnTarget("Player").transform;
        if (!Player)
            Debug.LogError("Player not found");

        if (!audioSource)
            Debug.LogError("Audio source not found");

        vrRays = FindObjectsOfType<VRRay>();
        if (VRRays == null)
            Debug.LogError("vrRaycaster not found");

        vrInput = (VRInput)FindObjectOfType(typeof(VRInput));
        if (!VRInput)
            Debug.LogError("vrInput not found");
    }

    private IEnumerator WaitBeforeFaidOut()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(vrCameraFade.BeginFadeIn(3, false));
    }  
    
    public void PlayAudioClip(AudioClip audioClip, float volume)
    {
        audioSource.volume = volume;
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayAudioClip(AudioClip audioClip)
    {
        audioSource.volume = 0.1f;
        audioSource.PlayOneShot(audioClip);
    }
}
