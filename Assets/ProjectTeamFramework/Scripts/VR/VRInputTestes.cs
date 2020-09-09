using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.XR;

//Simulando o controle de RV no PC:
//triger botao esquerdo do mouse
//touchpad botao direito do mouse (click) e posicao do mouse (trackpad)
//cancel botao do meio do mouse

public class VRInputTestes : MonoBehaviour
{
    OVRInput.Controller controllerR, controlerL;

    public TextMeshProUGUI buttonUpDown, buttonWhile, positionText;

    float count;

    public float timer = 3;
         
    #region NAO MEXA AQUI
    // Update is called once per frame
    void Update()
    {       
        count -= Time.deltaTime;
        if (count < 0)
        {
            buttonUpDown.text = "";
            buttonWhile.text = "";
            positionText.text = "";
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            //buttonUpDown.text += "Button" + "Fire1";
            count = 3;
        }

        if (Input.GetButtonDown("Oculus_CrossPlatform_Button2"))
        {
           // buttonUpDown.text += "Button" + "Oculus_CrossPlatform_Button2";
            count = 3;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            //buttonUpDown.text += "Button" + "Fire2";
            count = 3;
        }

        if (Input.GetButtonDown("Fire3"))
        {
            //buttonUpDown.text += "Button" + "Fire3";
            count = 3;
        }

        if (Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") != 0)
        {
           // buttonUpDown.text += "Axis" + "Oculus_CrossPlatform_PrimaryIndexTrigger";
            count = 3;
        }
        if (Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger") != 0)
        {
           // buttonUpDown.text += "Axis" + "Oculus_CrossPlatform_PrimaryHandTrigger";
            count = 3;
        }

        if (Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger") != 0)
        {
            //buttonUpDown.text += "Axis" + "Oculus_CrossPlatform_SecondaryIndexTrigger";
            count = 3;
        }
        if (Input.GetAxis("Oculus_CrossPlatform_SecondaryHandTrigger") != 0)
        {
            //buttonUpDown.text += "Axis" + "Oculus_CrossPlatform_SecondaryHandTrigger";
            count = 3;
        }

       /* bool discardedValue;
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);

        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

        if (rightHandDevices[0].TryGetFeatureValue(CommonUsages.gripButton, out discardedValue))
        {
            buttonUpDown.text += "XR Device" + "primaryGRip";
            count = 3;
        }*/

        return;


        bool triggerP = OnButton(OVRInput.Button.PrimaryIndexTrigger, ControllerR)
            || OnButton(OVRInput.Button.PrimaryIndexTrigger, ControllerL);
        // This if statement is to trigger events based on the information gathered before.
        if ((triggerP) ||
            ((Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor) && Input.GetMouseButton(0)))
        {
            OnTrigger();
        }

        bool triggerDown = OnButtonDown(OVRInput.Button.PrimaryIndexTrigger, ControllerR)
            || OnButton(OVRInput.Button.PrimaryIndexTrigger, ControllerL);
        // This if statement is to trigger events based on the information gathered before.
        if ((triggerDown) ||
            ((Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor) && Input.GetMouseButtonDown(0)))
        {
            OnTriggerDown();
        }

        bool triggerUp = OnButtonUp(OVRInput.Button.PrimaryIndexTrigger, ControllerR)
            || OnButton(OVRInput.Button.PrimaryIndexTrigger, ControllerL);
        // This if statement is to trigger events based on the information gathered before.
        if ((triggerUp) ||
            ((Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor) && Input.GetMouseButtonDown(0)))
        {
            OnTriggerUp();
        }


        triggerP = OnButton(OVRInput.Button.PrimaryHandTrigger, ControllerR)
            || OnButton(OVRInput.Button.PrimaryHandTrigger, ControllerL);
        // This if statement is to trigger events based on the information gathered before.
        if ((triggerP) ||
            ((Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor) && Input.GetMouseButton(0)))
        {
            OnTrigger();
        }

        triggerDown = OnButtonDown(OVRInput.Button.PrimaryHandTrigger, ControllerR)
            || OnButton(OVRInput.Button.PrimaryHandTrigger, ControllerL);
        // This if statement is to trigger events based on the information gathered before.
        if ((triggerDown) ||
            ((Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor) && Input.GetMouseButtonDown(0)))
        {
            OnTriggerDown();
        }

        triggerUp = OnButtonUp(OVRInput.Button.PrimaryHandTrigger, ControllerR)
            || OnButton(OVRInput.Button.PrimaryHandTrigger, ControllerL);
        // This if statement is to trigger events based on the information gathered before.
        if ((triggerUp) ||
            ((Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor) && Input.GetMouseButtonDown(0)))
        {
            OnTriggerUp();
        }


        // onCancel
        Vector2 thumbstick = OnAxis(OVRInput.Axis2D.PrimaryThumbstick, ControllerR) +
            OnAxis(OVRInput.Axis2D.PrimaryThumbstick, ControllerL);
        // This if statement is to trigger events based on the information gathered before.
        if ((thumbstick != Vector2.zero) ||
            ((Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor) && Input.GetMouseButton(2)))
        {
            OnTouch();
        }

        if(OVRInput.GetDown(OVRInput.Button.Any, ControllerR))
        {
            buttonUpDown.text += "ControllerR" + OVRInput.GetDown(OVRInput.Button.Any, ControllerR).ToString();
            count = 3;
        }
        
        if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.Active))
        {
            buttonUpDown.text += "Active" + OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.Active).ToString();
            count = 3;
        }

        if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.GetConnectedControllers()))
        {
            buttonUpDown.text += "GetConnectedControllers" + OVRInput.GetDown(OVRInput.Button.Any, OVRInput.GetConnectedControllers()).ToString();
            count = 3;
        }

        
    }

    public OVRInput.Controller ControllerR
    {
        get
        {
            return OVRInput.Controller.RTouch;
        }
    }

    public OVRInput.Controller ControllerL
    {
        get
        {
            return OVRInput.Controller.LTouch;            
        }
    }

    public Vector3 RightControllerRotation
    {
        get
        {
            Quaternion orientation = OVRInput.GetLocalControllerRotation(ControllerR);
            return orientation.eulerAngles;
        }
    }

    public Vector3 LeftControllerRotation
    {
        get
        {
            Quaternion orientation = OVRInput.GetLocalControllerRotation(ControllerL);
            return orientation.eulerAngles;
        }
    }

    public Vector3 RightControllerPosition
    {
        get
        {
            return OVRInput.GetLocalControllerPosition(ControllerR);
        }
    }

    public Vector3 LeftControllerPosition
    {
        get
        {
            return OVRInput.GetLocalControllerPosition(ControllerL);
        }
    }

    #endregion

    private bool OnButton(OVRInput.Button button, OVRInput.Controller controller )
    {
        return OVRInput.Get(button, controller);
    }

    private bool OnButtonDown(OVRInput.Button button, OVRInput.Controller controller)
    {
        return OVRInput.GetDown(button, controller);
    }

    private bool OnButtonUp(OVRInput.Button button, OVRInput.Controller controller)
    {
        return OVRInput.GetUp(button, controller);
    }

    private Vector2 OnAxis(OVRInput.Axis2D axis, OVRInput.Controller controller)
    {
        return OVRInput.Get(axis, controller);
    }

    void printonscreen(string s)
    {
        count = timer;
        buttonUpDown.text += s + "\n";
    }

    /// <summary>
    /// Funcao usada para processar o botao de trigger enquanto ele eh pressionado. 
    /// </summary>
    void OnTrigger()
    {
        buttonWhile.text = "OnTrigger\n";
    }

    /// <summary>
    /// Funcao usada para processar o botao de trigger quando ele eh solto. 
    /// </summary>
    void OnTriggerUp()
    {
        printonscreen("OnTriggerUp");
    }

    /// <summary>
    /// Funcao usada para processar o botao de trigger assim que ele eh pressionado. 
    /// </summary>
    void OnTriggerDown()
    {
        printonscreen("OnTriggerDown");
        printonscreen("OnTriggerDown");
    }

    /// <summary>
    /// Funcao usada para processar o botao de touchpad enquanto que ele eh pressionado. 
    /// </summary>
    void OnTouch()
    {
        buttonWhile.text = "OnTouch\n";
    }

    /// <summary>
    /// Funcao usada para processar o botao de touchpad quando ele eh solto. 
    /// </summary>
    void OnTouchUp()
    {
        printonscreen("OnTouchUp");
    }

    /// <summary>
    /// Funcao usada para processar o botao de touchpad assim que ele eh pressionado. 
    /// </summary>
    void OnTouchDown()
    {
        printonscreen("OnTouchDown");
    }

    /// <summary>
    /// Funcao usada para processar a posicao do toque no touchpad do controle. 
    /// Por ex aqui que poderia ser implementado um swipe.
    /// Directions:
    /// x > 0 --> direita
    /// x < 0 --> esquerda
    /// y > 0 --> cima
    /// y < 0 --> baixo
    /// </summary>
    void OnTouchPosition(Vector2 position)
    {
        printonscreen("OnTrigger");
    }

    /// <summary>
    /// Funcao usada para processar o botao de cancelar enquanto ele eh pressionado. 
    /// </summary>
    void OnCancel()
    {
        buttonWhile.text = "OnCancel\n";
    }

    /// <summary>
    /// Funcao usada para processar o botao de cancelar quando eh solto. 
    /// </summary>
    void OnCancelUp()
    {
        printonscreen("OnCancelUp");
    }

    /// <summary>
    /// Funcao usada para processar o botao de cancelar assim que ele eh pressionado. 
    /// </summary>
    void OnCancelDown()
    {
        printonscreen("OnCancelDown");
    }

}
