using System;
using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace VRStandardAssets.Utils
{
    // This class should be added to any gameobject in the scene
    // that should react to input based on the user's gaze, aim or hand position.
    // It contains events that can be subscribed to by classes that
    // need to know about input specifics to this gameobject.

    public enum InteractionModes
    {
        Over = 0,
        Out = 1,
        Click = 2,
        DoubleClick = 3,
        Up = 4,
        Grab = 5,
        Release = 6
    }


    public class VRInteractiveItem : MonoBehaviour
    {
        public bool OnOverEvent = false;
        public bool OnOutEvent = false;
        public bool OnClickEvent = true;
        public bool OnDoubleClickEvent = false;
        public bool OnUpEvent = false;
        public bool OnGrabEvent = false;
        public bool OnReleaseEvent = false;

        [SerializeField]
        List<BaseSelector> targets = new List<BaseSelector>();

        protected bool m_IsOver;

        public void IncludeNewTarget(BaseSelector bs)
        {
            if(targets == null)
                targets = new List<BaseSelector>();
            targets.Add(bs);
        }

        public bool IsOver
        {
            get { return m_IsOver; }              // Is the gaze currently over this object?
        }


        // The below functions are called by the VREyeRaycaster when the appropriate input is detected.
        // They in turn call the appropriate events should they have subscribers.
        public void Over()
        {
            m_IsOver = true;
            if (!OnOverEvent)
                return;

            FireInteraction(InteractionModes.Over);
        }


        public void Out()
        {
            m_IsOver = false;

            if (!OnOutEvent)
                return;

            FireInteraction(InteractionModes.Out);
        }


        public void Click()
        {
            if (!OnClickEvent)
                return;

            FireInteraction(InteractionModes.Click);
        }


        public void Up()
        {
            if (!OnUpEvent)
                return;

            FireInteraction(InteractionModes.Up);
        }

        public void Grab()
        {
            if (!OnGrabEvent)
                return;

            FireInteraction(InteractionModes.Grab);
        }

        public void Release()
        {
            if (!OnReleaseEvent)
                return;

            FireInteraction(InteractionModes.Release);
        }

        void FireInteraction(InteractionModes mode)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].OnInteractionTrigger(mode);
            }
        }
    }
}