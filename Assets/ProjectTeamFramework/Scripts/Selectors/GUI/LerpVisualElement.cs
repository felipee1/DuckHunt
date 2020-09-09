using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public enum RendererType
{
    Image = 0,
    Mesh = 1,
    Line = 2
}

/// <summary>Usada para mexer dinamicamente em propriedades de um objeto da GUI,
/// como posição, rotação e escala, alem tambem da cor do objeto .</summary>
public abstract class LerpVisualElement : MonoBehaviour
{
    [Header("Target configuration")]
    [SerializeField]
    GameObject target;

    public RendererType rendererType;

    Material material;
    Image myImage;

    [Header("Lerp General configuration")]
    public LerpEquationTypes lerp;
    public float transitionInDuration = 0.5f, transitionOutDuration = 0.5f;

    protected float lerpFactor, lerpA, lerpB;
    protected float totalLerpDuration;
    protected float countDownToTurnOff;

    [Header("Lerp Transform configuration")]
    public bool lerpTransform = true;
    public Vector3 desiredLocalPosition;
    public Vector3 desiredLocalRotation;
    public Vector3 desiredLocalScale;

    [Header("Lerp Color configuration")]
    public bool lerpColor = true;
    public Color desiredColor = Color.black;

    protected Vector3 initialLocalPosition, initialLocalRotation, initialLocalScale;
    protected Color initialColor;

    protected Vector3 lerpPosA, lerpPosB, lerpEulerA, lerpEulerB, lerpScaleA, lerpScaleB;
    protected Color lerpColorA, lerpColorB;
   
    public event Action<GameObject> OnExecuted;
    
    // Use this for initialization
    virtual protected void Awake () { 
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localEulerAngles;
        initialLocalScale = transform.localScale;
        GetMyInitialColor();
    }

    protected virtual void GetMyInitialColor()
    {        
        if (!target)
            target = this.gameObject;
        switch (rendererType)
        {
            case RendererType.Image:
                myImage = target.GetComponent<Image>();
                if (myImage)
                    initialColor = myImage.color;
                else
                    Debug.LogError("Material or Image or component not found.");
                break;
            case RendererType.Mesh:
                material = target.GetComponent<MeshRenderer>().material;
                if (material)
                    initialColor = material.color;
                else
                    Debug.LogError("Material or Image or component not found.");
                break;
            case RendererType.Line:
                material = target.GetComponent<LineRenderer>().material;
                if (material)
                    initialColor = material.color;
                else
                    Debug.LogError("Material or Image or component not found.");
                break;
            default:
                break;
        }

        
    }
    
    void Update()
    {
        countDownToTurnOff -= Time.deltaTime;
        if (countDownToTurnOff > 0)
        {
            lerpFactor = countDownToTurnOff / totalLerpDuration;

            if (lerpTransform)
            {
                if (desiredLocalPosition != Vector3.zero)
                    transform.localPosition = lerp.Lerp(lerpPosA, lerpPosB, 1 - lerpFactor);

                if (desiredLocalRotation != Vector3.zero)
                    transform.localEulerAngles = lerp.Lerp(lerpEulerA, lerpEulerB, 1 - lerpFactor);

                if (desiredLocalScale != Vector3.zero)
                    transform.localScale = lerp.Lerp(lerpScaleA, lerpScaleB, 1 - lerpFactor);
            }

            if (lerpColor && desiredColor != Color.black)
                SetNewColor();
        }
    }

    protected virtual void SetNewColor()
    {

        switch (rendererType)
        {
            case RendererType.Image:
                myImage.color = lerp.Lerp(lerpColorA, lerpColorB, 1 - lerpFactor);
                break;
            case RendererType.Mesh:
            case RendererType.Line:
                material.color = lerp.Lerp(lerpColorA, lerpColorB, 1 - lerpFactor);
                break;
            default:
                break;
        }            
    }

    virtual protected void OnEnable()
    {
        Reset();
    }


    virtual protected void OnDisable()
    {
        Reset();
    }

    public void HandleOut()
    {
        // When the user looks away from the rendering of the scene, hide the radial.
        totalLerpDuration = transitionInDuration;
        countDownToTurnOff = totalLerpDuration;        

        lerpPosA = desiredLocalPosition;
        lerpPosB = initialLocalPosition;
        lerpEulerA = desiredLocalRotation;
        lerpEulerB = initialLocalRotation;
        lerpScaleA = desiredLocalScale;
        lerpScaleB = initialLocalScale;
        lerpColorA = desiredColor;
        lerpColorB = initialColor;
    }

    public void HandleOver()
    {
        totalLerpDuration = transitionOutDuration;
        countDownToTurnOff = totalLerpDuration;

        lerpPosA = initialLocalPosition;
        lerpPosB = desiredLocalPosition;
        lerpEulerA = initialLocalRotation;
        lerpEulerB = desiredLocalRotation;
        lerpScaleA = initialLocalScale;
        lerpScaleB = desiredLocalScale;
        lerpColorA = initialColor;
        lerpColorB = desiredColor;
    }

    public void Reset()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
            return;
#endif     
        countDownToTurnOff = 0;
        transform.localPosition = initialLocalPosition;
        transform.localEulerAngles = initialLocalRotation;
        transform.localScale = initialLocalScale;
    }

}
