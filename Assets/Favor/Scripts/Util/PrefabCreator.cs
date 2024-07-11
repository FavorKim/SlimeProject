using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PrefabCreator
{
    public static void CreateObjectPrefab(ObjectStatus prefab)
    {
        var inst = new GameObject().AddComponent<ObjectBase>();
        inst.InitStatus(prefab);
        
        Object obj = PrefabUtility.SaveAsPrefabAsset(inst.gameObject,$"Assets/Favor/Prefabs/{prefab.datainfo}.prefab");
        if(obj != null)
        {
            Debug.Log($"{inst.GetStatus().datainfo} was successfully Saved As Prefab");
        }
        else
        {
            Debug.LogWarning($"{inst.GetStatus().datainfo} failed to Save As Prefab");
        }
    }
}
