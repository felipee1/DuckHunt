using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Simulando o controle de RV no PC:
//triger botao esquerdo do mouse
//touchpad botao direito do mouse (click) e posicao do mouse (trackpad)
//cancel botao do meio do mouse

public class VRInputGeneric : MonoBehaviour
{
    OVRInput.Controller controller;

    #region NAO MEXA AQUI
    // Update is called once per frame
    void Update()
    {
        float trigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.GetConnectedControllers());
        // This if statement is to trigger events based on the information gathered before.
        if ((trigger != 0) ||
            ((Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor) && Input.GetMouseButton(0)))
        {
            OnTrigger();
        }

        bool triggerDown = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.GetConnectedControllers());
        // This if statement is to trigger events based on the information gathered before.
        if ((triggerDown) ||
            ((Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor) && Input.GetMouseButtonDown(0)))
        {
            OnTriggerDown();
        }

        // ontouch
        bool touch = OVRInput.Get(OVRInput.Touch.PrimaryTouchpad, OVRInput.GetConnectedControllers());
        // This if statement is to trigger events based on the information gathered before.
        if ((touch) ||
            ((Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor) && Input.GetMouseButton(1)))
        {
            OnTouch();
        }

        // ontouchUp
        bool touchUp = OVRInput.GetUp(OVRInput.Touch.PrimaryTouchpad, OVRInput.GetConnectedControllers());
        // This if statement is to trigger events based on the information gathered before.
        if ((touchUp) ||
            ((Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor) && Input.GetMouseButtonUp(1)))
        {
            OnTouchUp();
        }

        // ontouchDown
        bool touchDown = OVRInput.GetDown(OVRInput.Touch.PrimaryTouchpad, OVRInput.GetConnectedControllers());
        // This if statement is to trigger events based on the information gathered before.
        if ((touchDown) ||
            ((Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor) && Input.GetMouseButtonDown(1)))
        {
            OnTouchDown();
        }

        // ontouchposition
        Vector2 position;
        // This if statement is to trigger events based on the information gathered before.
        if ((touch) ||
            ((Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor) && Input.GetMouseButtonDown(0)))
        {
            if(Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor)
            {
                position = Input.mousePosition;
            } else
            {
                position = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, OVRInput.Controller.RTrackedRemote);
            }

            OnTouchPosition(position);
        }

        // onCancel
        bool cancel = OVRInput.Get(OVRInput.Button.Back, OVRInput.GetConnectedControllers());
        // This if statement is to trigger events based on the information gathered before.
        if ((cancel) ||
            ((Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor) && Input.GetMouseButton(2)))
        {
            OnCancel();
        }

        // onCancelUp
        bool cancelUp = OVRInput.GetUp(OVRInput.Button.Back, OVRInput.GetConnectedControllers());
        // This if statement is to trigger events based on the information gathered before.
        if ((cancelUp) ||
            ((Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor) && Input.GetMouseButtonUp(2)))
        {
            OnCancelUp();
        }

        // cancelDown
        bool cancelDown = OVRInput.GetDown(OVRInput.Button.Back, OVRInput.GetConnectedControllers());
        // This if statement is to trigger events based on the information gathered before.
        if ((cancelDown) ||
            ((Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor) && Input.GetMouseButtonDown(2)))
        {
            OnCancelDown();
        }
    }

    public OVRInput.Controller Controller
    {
        get
        {
            OVRInput.Controller controller = OVRInput.GetConnectedControllers();
            if ((controller & OVRInput.Controller.LTrackedRemote) == OVRInput.Controller.LTrackedRemote)
            {
                return OVRInput.Controller.LTrackedRemote;
            }
            else if ((controller & OVRInput.Controller.RTrackedRemote) == OVRInput.Controller.RTrackedRemote)
            {
                return OVRInput.Controller.RTrackedRemote;
            }
            return OVRInput.GetActiveController();
        }
    }

    public Vector3 ControllerRotation
    {
        get
        {
            Quaternion orientation = OVRInput.GetLocalControllerRotation(Controller);
            return orientation.eulerAngles;
        }
    }

    #endregion


    /// <summary>
    /// Funcao usada para processar o botao de trigger enquanto ele eh pressionado. 
    /// </summary>
    void OnTrigger()
    {

    }

    /// <summary>
    /// Funcao usada para processar o botao de trigger quando ele eh solto. 
    /// </summary>
    void OnTriggerUp()
    {

    }

    /// <summary>
    /// Funcao usada para processar o botao de trigger assim que ele eh pressionado. 
    /// </summary>
    void OnTriggerDown()
    {

    }

    /// <summary>
    /// Funcao usada para processar o botao de touchpad enquanto que ele eh pressionado. 
    /// </summary>
    void OnTouch()
    {

    }

    /// <summary>
    /// Funcao usada para processar o botao de touchpad quando ele eh solto. 
    /// </summary>
    void OnTouchUp()
    {

    }

    /// <summary>
    /// Funcao usada para processar o botao de touchpad assim que ele eh pressionado. 
    /// </summary>
    void OnTouchDown()
    {

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

    }

    /// <summary>
    /// Funcao usada para processar o botao de cancelar enquanto ele eh pressionado. 
    /// </summary>
    void OnCancel()
    {

    }

    /// <summary>
    /// Funcao usada para processar o botao de cancelar quando eh solto. 
    /// </summary>
    void OnCancelUp()
    {

    }

    /// <summary>
    /// Funcao usada para processar o botao de cancelar assim que ele eh pressionado. 
    /// </summary>
    void OnCancelDown()
    {

    }

}
