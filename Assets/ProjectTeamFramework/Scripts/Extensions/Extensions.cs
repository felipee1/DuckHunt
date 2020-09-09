using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class Extensions
{
    public static void SetText(this Text text, string value)
    {
        text.text = value;
    }

    public static void SetValueWithEventsIntact(this Toggle toggle, bool value)
    {
        toggle.isOn = value;
    }

    public static int GetActiveChildrenCount<T>(this Transform transform) where T : Component
    {
        int activeChildCount = 0;

        foreach (T a in transform.GetComponentsInChildren<T>(false))
        {

            if (a.gameObject.activeInHierarchy == true)
            {
                activeChildCount++;
            }

        }
        return activeChildCount;
    }

    public static List<ComponentType> GetFirstOrderChildren<ComponentType>(this ComponentType transform) where ComponentType : Component
    {
        List<ComponentType> finalList = new List<ComponentType>();

        foreach (var t in transform.GetComponentsInChildren<ComponentType>(true))
        {
            if (t.transform.parent == transform)
                finalList.Add(t);
        }

        return finalList;
    }

    public static GameObject FindEvenInactive(string value)
    {
        GameObject retorno = null;
        foreach (GameObject go in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (go.name != value)
                continue;

            if (go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave)
                continue;
#if UNITY_EDITOR
            bool isThisNotAPrefab = PrefabUtility.GetPrefabInstanceStatus(go) == PrefabInstanceStatus.NotAPrefab
                && PrefabUtility.GetPrefabAssetType(go) == PrefabAssetType.NotAPrefab;
       
            if (!isThisNotAPrefab)
                continue;
#endif    
            return go;
        }
        Debug.LogError("Objeto " + value + " nao encontrado na cena.");
        return retorno;
    }

    public static GameObject FindAndTurnOnTarget(string targetName)
    {
        GameObject target = Extensions.FindEvenInactive(targetName);
        if (!target)
            Debug.LogError(string.Format("Objeto {0} nao existente!", targetName));
        target?.SetActive(true);
        return target;
    }

    public static void SetToGlobal(this Transform trans, Vector3 Scale)
    {
        
        //this simulates what it would be like to set lossyScale considering the way unity treats it
        var m = trans.worldToLocalMatrix;
        m.SetColumn(3, new Vector4(0f, 0f, 0f, 1f));
        trans.localScale = m.MultiplyPoint(Scale);
       
    }

    public static void SetToGlobal(Transform trans, Vector3 Scale, bool bSetScaleOnGlobalAxes)
    {
        
        trans.localScale = Vector3.one;
        var m = trans.worldToLocalMatrix;
        if (bSetScaleOnGlobalAxes)
        {
            m.SetColumn(0, new Vector4(m.GetColumn(0).magnitude, 0f));
            m.SetColumn(1, new Vector4(0f, m.GetColumn(1).magnitude));
            m.SetColumn(2, new Vector4(0f, 0f, m.GetColumn(2).magnitude));
        }
        m.SetColumn(3, new Vector4(0f, 0f, 0f, 1f));
        trans.localScale = m.MultiplyPoint(Scale);
        
    }

    public static Vector2 RemoveHeight(this Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.z);
    }
}
