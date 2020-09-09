using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

/// <summary>Usada para mexer dinamicamente em propriedades de um objeto da GUI,
/// como posição, rotação e escala, alem tambem da cor do objeto .</summary>
public abstract class LerpVisualElementMany : MonoBehaviour
{
    [Header("Target configuration")]
    [SerializeField]
    GameObject target;
    [SerializeField]
    bool impactEveryChildren = false;
    int sizeOfChildren;

    public RendererType rendererType;

    Material material;
    Material [] materials;
    Image myImage;
    Image []myImages;

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
    protected Color []initialColors;

    protected Vector3 lerpPosA, lerpPosB, lerpEulerA, lerpEulerB, lerpScaleA, lerpScaleB;
    protected Color lerpColorA, lerpColorB;

    protected Color [] lerpColorsA;
    protected Color [] lerpColorsB;

    public event Action<GameObject> OnExecuted;
    
    // Use this for initialization
    async virtual protected void Awake () {
        await new WaitForEndOfFrame();
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localEulerAngles;
        initialLocalScale = transform.localScale;
        GetMyInitialColor();
    }

    protected virtual void GetMyInitialColor()
    {
        if (!target)
            target = this.gameObject;

        if (impactEveryChildren)
        {          
            switch (rendererType)
            {
                case RendererType.Image:
                    myImages = target.GetComponentsInChildren<Image>(true);
                    sizeOfChildren = myImages.Length;
                    if (sizeOfChildren == 0)
                        initialColor = myImage.color;
                    else
                        Debug.LogError("Material or Image or component not found.");
                    break;
                case RendererType.Mesh:
                    MeshRenderer[] meshes = target.GetComponentsInChildren<MeshRenderer>(true);
                    sizeOfChildren = meshes.Length;
                    materials = new Material[sizeOfChildren];
                    initialColors = new Color[sizeOfChildren];
                    for (int i = 0; i < sizeOfChildren; i++)
                    {
                        materials[i] = meshes[i].material;
                        initialColors[i] = materials[i].color;
                    }
                    if (sizeOfChildren == 0)
                        Debug.LogError("Material or Image or component not found.");
                    break;
                case RendererType.Line:
                    LineRenderer[] lines = target.GetComponentsInChildren<LineRenderer>(true);
                    sizeOfChildren = lines.Length;
                    materials = new Material[sizeOfChildren];
                    initialColors = new Color[sizeOfChildren];
                    for (int i = 0; i < sizeOfChildren; i++)
                    {
                        materials[i] = lines[i].material;
                        initialColors[i] = materials[i].color;
                    }
                    if (materials.Length == 0)
                        Debug.LogError("Material or Image or component not found.");
                    break;
                default:
                    break;
            }
            lerpColorsA = new Color[sizeOfChildren];
            lerpColorsB = new Color[sizeOfChildren];
        }
        else
        {
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
        if (impactEveryChildren)
        {
            switch (rendererType)
            {
                case RendererType.Image:
                    for (int i = 0; i < sizeOfChildren; i++)
                    {
                        myImages[i].color = lerp.Lerp(lerpColorsA[i], lerpColorsB[i], 1 - lerpFactor);
                    }
                    break;
                case RendererType.Mesh:
                case RendererType.Line:
                    for (int i = 0; i < sizeOfChildren; i++)
                    {
                        materials[i].color = lerp.Lerp(lerpColorsA[i], lerpColorsB[i], 1 - lerpFactor);
                    }
                    break;
                default:
                    break;
            }
        }
        else
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
        if (impactEveryChildren)
        {
            for (int i = 0; i < sizeOfChildren; i++)
            {
                lerpColorsA[i] = desiredColor;
                lerpColorsB[i] = initialColors[i];
            }
        }
        else
        {
            lerpColorA = desiredColor;
            lerpColorB = initialColor;
        }
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
        if (impactEveryChildren)
        {
            for (int i = 0; i < sizeOfChildren; i++)
            {
                lerpColorsA[i] = initialColors[i];
                lerpColorsB[i] = desiredColor;
            }
        }
        else
        {
            lerpColorA = initialColor;
            lerpColorB = desiredColor;
        }
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
