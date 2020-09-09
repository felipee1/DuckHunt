using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using VRStandardAssets.Utils;
using TMPro;

public class ReferenceManagerDependent : SingletonGameObject<ReferenceManagerDependent>
{

    HandsController handsController;
    public HandsController HandsController { get => handsController; }

#if NESTLE_RV

    GameObject longeFrame;
    SimulationStateController simulationStateController;
    NPCController npcController;
    StepInstructions stepInstructions;
    QuestionarioController questionMenu;
    RVDebugger rvDebugger;
    GameObject frameAvisos;
    TextMeshProUGUI frameAvisosText;
    GameObject frameBiscoitos;
    TextMeshProUGUI frameBiscoitosText;
    InfoController infoController;
    GameObject modeloBolacha;

    
    public GameObject LongeFrame { get => longeFrame; }
    public GameObject FrameAvisos { get => frameAvisos; }
    public GameObject ModeloBolacha { get => modeloBolacha; }
    public TextMeshProUGUI FrameAvisosText { get => frameAvisosText; }
    public GameObject FrameBiscoitos { get => frameBiscoitos; }
    public TextMeshProUGUI FrameBiscoitosText { get => frameBiscoitosText; }
    public SimulationStateController SimulationStateController { get => simulationStateController; }
    public NPCController NPCController { get => npcController; }
    public StepInstructions StepInstructions { get => stepInstructions; }
    public QuestionarioController QuestionMenu { get => questionMenu; set => questionMenu = value; }
    public RVDebugger RVDebugger { get => rvDebugger; set => rvDebugger = value; }
    public InfoController InfoController { get => infoController; set => infoController = value; }

    public GameObject prefabParticleFallOnGround;    
    public GameObject prefabToggle;

#endif

#if PROJECT_LAR

     [SerializeField] ReflectionProbe reflectionProbe;
    ZoomGroupController zoomGroupController;

    public ReflectionProbe ReflectionProbe { get => reflectionProbe; }
    public ZoomGroupController ZoomGroupController { get => zoomGroupController; }

#endif

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();

        FindAssets();        
    }

    void FindAssets()
    {
        handsController = (HandsController)FindObjectOfType(typeof(HandsController));
        if (!HandsController)
            Debug.LogError("PlayerController not found");

#if NESTLE_RV
        NestleRV();
#endif

#if PROJECT_LAR
        ProjectLar();
#endif

    }

#if NESTLE_RV
    void NestleRV()
    {     
        modeloBolacha = Extensions.FindEvenInactive("ModeloBolacha");
        if (!ModeloBolacha)
            Debug.Log("ModeloBolacha not found");
        else
            ModeloBolacha.SetActive(false);

        longeFrame = Extensions.FindEvenInactive("LongeFrame");
        if (!LongeFrame)
            Debug.Log("Longe frame not found");
        else
            LongeFrame.SetActive(false);

        frameBiscoitos = Extensions.FindEvenInactive("FrameBiscoitos");
        if (!FrameBiscoitos)
            Debug.Log("Frame de biscoitos not found");
        else
            FrameBiscoitos.SetActive(false);

        frameBiscoitosText = Extensions.FindEvenInactive("FrameBiscoitosText")?.GetComponent<TextMeshProUGUI>();
        if (!FrameBiscoitosText)
            Debug.Log("Texto do Frame de biscoitos not found");

        frameAvisos = Extensions.FindEvenInactive("FrameAvisos");
        if (!FrameAvisos)
            Debug.Log("Frame de avisos not found");
        else
            FrameAvisos.SetActive(false);

        frameAvisosText = Extensions.FindEvenInactive("FrameAvisosText")?.GetComponent<TextMeshProUGUI>();
        if (!FrameAvisosText)
            Debug.Log("Texto do Frame de avisos not found");

        simulationStateController = (SimulationStateController)FindObjectOfType(typeof(SimulationStateController));
        if (!SimulationStateController)
            Debug.Log("simulationStateController not found");

        npcController = (NPCController)FindObjectOfType(typeof(NPCController));
        if (!npcController)
            Debug.Log("NPC not found");

        stepInstructions = (StepInstructions)FindObjectOfType(typeof(StepInstructions));
        if (!StepInstructions)
            Debug.Log("instrucoes nao achadas");
        // else
        //    stepInstructions.gameObject.SetActive(false);

        questionMenu = (QuestionarioController)FindObjectOfType(typeof(QuestionarioController));
        if (!QuestionMenu)
            Debug.Log("Menu de questionario nao encontrado");
        else
            QuestionMenu.gameObject.SetActive(false);

        rvDebugger = (RVDebugger)FindObjectOfType(typeof(RVDebugger));
        if (!RVDebugger)
            Debug.Log("RV Debugger nao achado");

        infoController = (InfoController)FindObjectOfType(typeof(InfoController));
        if (!infoController)
            Debug.Log("Info Controller nao encontrado");
    }
#endif

#if PROJECT_LAR
    void ProjectLar()
    {
      if (!ReflectionProbe)
            reflectionProbe = GameObject.Find("Reflection Probe")?.GetComponent<ReflectionProbe>();
        if (!ReflectionProbe)
            Debug.LogError("Reflection Probe not found");

        zoomGroupController = (ZoomGroupController)FindObjectOfType(typeof(ZoomGroupController));
        if (!ZoomGroupController)
            Debug.LogError("ZoomGroupController not found");

           LoadAndStartAssets();
    }

    void LoadAndStartAssets()
    {
        foreach (PrefabLightmapData go in Resources.FindObjectsOfTypeAll(typeof(PrefabLightmapData)) as PrefabLightmapData[])
        {
            if (go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave)
                continue;

            bool isNotOnScene = go.gameObject.scene.name != UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            if (isNotOnScene)
                continue;

            go.LoadAssetsForCurrentScene();
        }

        ReflectionProbe?.gameObject.SetActive(true);
        ReflectionProbe?.RenderProbe();

        //Shader.WarmupAllShaders();
    }
#endif
}
