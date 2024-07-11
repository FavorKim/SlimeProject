using UnityEditor;
using UnityEngine;

public class PrefabCreator
{
    public static void CreateObjectPrefab(ObjectBase prefab)
    {
        string normalSaveRoot = $"Assets/Favor/Prefabs/Normal/{prefab.datainfo}.prefab";
        string attackSaveRoot = $"Assets/Favor/Prefabs/Attack/{prefab.datainfo}.prefab";

        string root = prefab is AttackObjectBase ? attackSaveRoot : normalSaveRoot;
        Object obj = PrefabUtility.SaveAsPrefabAsset(prefab.gameObject, root);

        if (obj != null)
        {
            Debug.Log($"{prefab.datainfo} was successfully Saved As Prefab");
        }
        else
        {
            Debug.LogWarning($"{prefab.datainfo} failed to Save As Prefab");
        }
    }
}
