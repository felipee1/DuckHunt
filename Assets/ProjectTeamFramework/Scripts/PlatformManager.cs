using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using UnityStandardAssets.Characters.FirstPerson;
using VRStandardAssets.Utils;

public enum VRPlataform
{
    PC = 0, 
    OculusGO = 1,
    OculusQuest = 2,
    OculusRift = 3,
    WebGL = 4,
    Cardboard = 5,
    Daydream = 6
}


public enum VRControlScheme
{
    Laser = 0,
    Grab = 1
}

public class PlatformManager : MonoBehaviour
{
    private VRPlataform currentVRPlatform = VRPlataform.PC;
    string loadedDeviceName;

    [SerializeField] GameObject pcCam, oculusCam, hudRPC, hudRRV, hudLPC, hudLRV;
    MonoBehaviour pcController, oculusController;
        
    Transform rightHand, leftHand;

    private VRControlScheme currentVRControlScheme = VRControlScheme.Laser;
    VRRay [] vrRays;

    public GameObject vrMode;
    float countDown;

    public bool playTestVRInPC;

    public VRControlScheme CurrentVRControlScheme { get => currentVRControlScheme;}
    public VRPlataform CurrentVRPlatform { get => currentVRPlatform;}

    // Start is called before the first frame update
    public void Awake()
    {
        currentVRPlatform = VRPlataform.PC;

        if(playTestVRInPC)
            currentVRPlatform = VRPlataform.OculusQuest;

        pcController = (FirstPersonController)FindObjectOfType(typeof(FirstPersonController));
        oculusController = (OVRPlayerController)FindObjectOfType(typeof(OVRPlayerController));

        if (XRDevice.isPresent)
        {         
            OnDeviceLoadAction(XRDevice.model);
        }
       // else
       // {
       //     XRDevice.deviceLoaded += OnDeviceLoadAction;
       // }
        SetPlatform();
        if (CurrentVRPlatform == VRPlataform.PC)
            Cursor.visible = false;
    }

    void OnDeviceLoadAction(string newLoadedDeviceName)
    {
        loadedDeviceName = newLoadedDeviceName;        
        if (loadedDeviceName.ToLower().Contains("oculus"))
        {
            currentVRPlatform = VRPlataform.OculusQuest;
        }        
    }

    private void SetPlatform()
    {
        bool isPc = false, isOculus = false;
        switch (CurrentVRPlatform)
        {
            case VRPlataform.PC:
            default:
                isPc = true;
                break;
            case VRPlataform.OculusGO:
            case VRPlataform.OculusQuest:
                isOculus = true;
                break;
        }
        pcCam?.SetActive(isPc);
        oculusCam?.SetActive(isOculus);

#if NESTLE_RV
        if (hudRPC != hudRRV)
        {
            if (!isPc && hudRPC)
            {
                hudRPC.SetActive(false);
                DestroyImmediate(hudRPC);
            }
            if (!isOculus && hudRRV)
            {
                hudRRV.SetActive(false);
                DestroyImmediate(hudRRV);
            }
        }
        if (hudLPC != hudLRV)
        {
            if (!isPc && hudLPC)
            {
                hudLPC.SetActive(false);
                DestroyImmediate(hudLPC);
            }
            if (!isOculus && hudLRV)
            {
                hudLRV.SetActive(false);
                DestroyImmediate(hudLRV);
            }
        }

#endif

        pcController.enabled = isPc;
        oculusController.enabled = isOculus;

        if (isOculus)
            SetHandsTransforms();

        ReferenceManagerIndependent.Instance.UpdateVRObjects();

        vrRays = ReferenceManagerIndependent.Instance.VRRays;

    }
    
    void SetHandsTransforms()
    {
        rightHand = GameObject.Find("RightHandAnchor").transform;
        leftHand = GameObject.Find("LeftHandAnchor").transform;
    }

    public Transform GetRightHand()
    {
        if (!rightHand)
            Debug.LogError("Acessing null right hand");
        return rightHand;
    }

    public Transform GetLeftHand()
    {
        if (!leftHand)
            Debug.LogError("Acessing null left hand");
        return leftHand;
    }

    public MonoBehaviour GetPlayerController()
    {
        if(CurrentVRPlatform == VRPlataform.PC)
        {
            return pcController;
        }
        else
        {
            return oculusController;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("ControlScheme") || Input.GetButtonDown("Oculus_CrossPlatform_Button1"))
        {
            ChangeVRControlScheme();            
        }
        if(countDown > 0)
        {
            countDown -= Time.deltaTime;
            if (countDown < 0)
            {
                vrMode.SetActive(false);
            }
        }
       
    }

    public void ChangeVRControlScheme()
    {
        currentVRControlScheme = CurrentVRControlScheme == VRControlScheme.Laser ? VRControlScheme.Grab : VRControlScheme.Laser;
        for (int i = 0; i < vrRays.Length; i++)
        {
            vrRays[i].ResetInteractable();
            if (CurrentVRControlScheme == VRControlScheme.Laser)
            {
                vrRays[i].GetComponent<VRRaycaster>()?.GetReticle().Show();
                vrRays[i].SetActiveVisibilityIndicator(show: true);
                VRRaygrabber aux = vrRays[i].GetComponent<VRRaygrabber>();
                if (aux)
                    aux.enabled = false;
            }
            else
            {
                vrRays[i].GetComponent<VRRaycaster>()?.GetReticle().Hide();
                vrRays[i].SetActiveVisibilityIndicator(show: false);
                VRRaycaster aux = vrRays[i].GetComponent<VRRaycaster>();
                if (aux)
                    aux.enabled = false;
            }
        }
        if (vrMode)
        {
            vrMode.SetActive(true);
            vrMode.GetComponent<TextMeshProUGUI>().text = ("Você trocou o modo de controle para " + CurrentVRControlScheme);
            countDown = 3;
        }
    }

    public void SetVRControlScheme(VRControlScheme newVRControlScheme)
    {
        currentVRControlScheme = newVRControlScheme == VRControlScheme.Laser ? VRControlScheme.Grab : VRControlScheme.Laser;
        ChangeVRControlScheme();
    }

}
