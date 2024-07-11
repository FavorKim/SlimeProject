using UnityEditor;
using UnityEngine;

public class PrefabCreator
{
    public static void CreateObjectPrefab(ObjectBase prefab)
    {
        string normalSaveRoot = $"Assets/Favor/Prefabs/Normal/{prefab.datainfo}.prefab";
        string attackSaveRoot = $"Assets/Favor/Prefabs/Attack/{prefab.datainfo}.prefab";
        var resource = Resources.Load<GameObject>($"Favor/OutLook/{prefab.datainfo}");
        if (resource != null)
        {
            GameObject outLook = GameObject.Instantiate(resource, prefab.gameObject.transform);
            if (outLook != null)
            {
                outLook.transform.localRotation = Quaternion.identity;
                outLook.transform.localPosition = Vector3.zero;
            }
        }
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
