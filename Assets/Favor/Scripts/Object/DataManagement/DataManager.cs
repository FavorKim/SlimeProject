using System;
using System.Xml.Linq;
using UnityEngine;

public class DataManager : SingletonMono<DataManager>
{
    private readonly string relativePath = "Favor/DataTable";

    private readonly string normalTableName = "NormalObjectTable";
    private readonly string attackTableName = "AttackObjectTable";

    //public string connectionString = "server=3.36.69.87;database=slime;user=root;password=1234";


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadAllObjectData();
    }

    void LoadAllObjectData()
    {
        LoadNormalObjectTable();
        LoadAttackObjectTable();
    }

    void LoadNormalObjectTable()
    {
        //LoadedObjectList = new Dictionary<string, ObjectBase>();
        XDocument doc = XDocument.Load($"{Application.dataPath}/{relativePath}/{normalTableName}.xml");

        bool isUpateOnly = gameObject.name == "TableUpdater" ? true : false;

        var dataElements = doc.Descendants("data");

        foreach (var data in dataElements)
        {
            var objectStatus = new GameObject().AddComponent<ObjectBase>();

            objectStatus.datainfo = data.Attribute(nameof(objectStatus.datainfo)).Value;
            objectStatus.description = data.Attribute(nameof(objectStatus.description)).Value;
            objectStatus.ID = int.Parse(data.Attribute(nameof(objectStatus.ID)).Value);
            objectStatus.usecount = int.Parse(data.Attribute(nameof(objectStatus.usecount)).Value);
            objectStatus.durabillity = int.Parse(data.Attribute(nameof(objectStatus.durabillity)).Value);
            objectStatus.masslimit = int.Parse(data.Attribute(nameof(objectStatus.masslimit)).Value);
            objectStatus.mass = int.Parse(data.Attribute(nameof(objectStatus.mass)).Value);
            objectStatus.holdable = data.Attribute(nameof(objectStatus.holdable)).Value == "TRUE";
            objectStatus.collide = data.Attribute(nameof(objectStatus.collide)).Value == "TRUE";
            objectStatus.deletable = data.Attribute(nameof(objectStatus.deletable)).Value == "TRUE";
            objectStatus.type = (ObjectType)Enum.Parse(typeof(ObjectType), data.Attribute(nameof(objectStatus.type)).Value);

            PrefabCreator.CreateObjectPrefab(objectStatus, isUpateOnly);
        }
    }


    void LoadAttackObjectTable()
    {
        //AttackObjectList = new();
        XDocument doc = XDocument.Load($"{Application.dataPath}/{relativePath}/{attackTableName}.xml");
        bool isUpateOnly = gameObject.name == "TableUpdater" ? true : false;

        var dataElements = doc.Descendants("data");

        foreach (var data in dataElements)
        {
            var objectStatus = new GameObject().AddComponent<AttackObjectBase>();
            objectStatus.datainfo = data.Attribute(nameof(objectStatus.datainfo)).Value;
            objectStatus.description = data.Attribute(nameof(objectStatus.description)).Value;
            objectStatus.ID = int.Parse(data.Attribute(nameof(objectStatus.ID)).Value);
            objectStatus.usecount = int.Parse(data.Attribute(nameof(objectStatus.usecount)).Value);
            objectStatus.durabillity = int.Parse(data.Attribute(nameof(objectStatus.durabillity)).Value);
            objectStatus.masslimit = int.Parse(data.Attribute(nameof(objectStatus.masslimit)).Value);
            objectStatus.mass = int.Parse(data.Attribute(nameof(objectStatus.mass)).Value);
            objectStatus.holdable = data.Attribute(nameof(objectStatus.holdable)).Value == "TRUE";
            objectStatus.collide = data.Attribute(nameof(objectStatus.collide)).Value == "TRUE";
            objectStatus.deletable = data.Attribute(nameof(objectStatus.deletable)).Value == "TRUE";
            objectStatus.type = (ObjectType)Enum.Parse(typeof(ObjectType), data.Attribute(nameof(objectStatus.type)).Value);
            
            objectStatus.atk = int.Parse(data.Attribute(nameof(objectStatus.atk)).Value);
            objectStatus.destroyimmediatly = data.Attribute(nameof(objectStatus.destroyimmediatly)).Value == "TRUE";
            
            PrefabCreator.CreateObjectPrefab(objectStatus, isUpateOnly);


        }
    }

}
