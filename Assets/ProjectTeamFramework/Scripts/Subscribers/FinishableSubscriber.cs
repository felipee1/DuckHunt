using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FinishableSubscriber : MonoBehaviour
{
    [SerializeField]
    GameObject eventGO;

    public EventTrigger.TriggerEvent OnEventTriggerCallback;

    // Start is called before the first frame update
    void Start()
    {
        IFinishable finishableComponent = eventGO.GetComponent<IFinishable>();
        if (finishableComponent != null)
            finishableComponent.OnExecuted += OnFinish;
    }

    // Update is called once per frame
    void OnFinish(GameObject GO)
    {
        OnEventTriggerCallback.Invoke(null);
    }
}
