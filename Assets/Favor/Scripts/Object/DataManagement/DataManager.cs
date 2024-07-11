using System;
using System.Xml.Linq;

public class DataManager : SingletonMono<DataManager>
{

    //public Dictionary<string, ObjectStatus> LoadedObjectList;
    //public Dictionary<string, AttackObjectStatus> AttackObjectList;

    private readonly string dataRootPath = "C:/Users/KGA/Desktop/SlimeData";
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
        //LoadedObjectList = new Dictionary<string, ObjectStatus>();
        XDocument doc = XDocument.Load($"{dataRootPath}/{normalTableName}.xml");
        var dataElements = doc.Descendants("data");

        foreach (var data in dataElements)
        {
            var objectStatus = new ObjectStatus();
            objectStatus.datainfo = data.Attribute(nameof(objectStatus.datainfo)).Value;
            objectStatus.description = data.Attribute(nameof(objectStatus.description)).Value;
            objectStatus.ID = int.Parse(data.Attribute(nameof(objectStatus.ID)).Value);
            objectStatus.useCount = int.Parse(data.Attribute(nameof(objectStatus.useCount)).Value);
            objectStatus.durabillity = int.Parse(data.Attribute(nameof(objectStatus.durabillity)).Value);
            objectStatus.masslimit = int.Parse(data.Attribute(nameof(objectStatus.masslimit)).Value);
            objectStatus.mass = int.Parse(data.Attribute(nameof(objectStatus.mass)).Value);
            objectStatus.holdable = data.Attribute(nameof(objectStatus.holdable)).Value == "TRUE";
            objectStatus.collide = data.Attribute(nameof(objectStatus.collide)).Value == "TRUE";
            objectStatus.deletable = data.Attribute(nameof(objectStatus.deletable)).Value == "TRUE";
            objectStatus.type = (ObjectType)Enum.Parse(typeof(ObjectType), data.Attribute(nameof(objectStatus.type)).Value);

            PrefabCreator.CreateObjectPrefab(objectStatus);
        }

        /*
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {

            connection.Open();

            string query = "SELECT * FROM objectTable";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var objectStatus = new ObjectStatus();

                        objectStatus.datainfo = reader["datainfo"].ToString();
                        objectStatus.description = reader["description"].ToString();
                        objectStatus.ID = int.Parse(reader["ID"].ToString());
                        objectStatus.useCount = int.Parse(reader["useCount"].ToString());
                        objectStatus.durabillity = int.Parse(reader["durabillity"].ToString());
                        objectStatus.masslimit = int.Parse(reader["durabillity"].ToString());
                        objectStatus.mass = int.Parse(reader["mass"].ToString());
                        objectStatus.holdable = reader["holdable"].ToString() == "TRUE";
                        objectStatus.collide = reader["collide"].ToString() == "TRUE";
                        objectStatus.deletable = reader["deletable"].ToString() == "TRUE";
                        objectStatus.type = (ObjectType)Enum.Parse(typeof(ObjectType), reader["type"].ToString());

                        LoadedObjectList.Add(objectStatus.description, objectStatus);
                    }
                }
                connection.Close();
            }
        }
        */
    }

    void LoadAttackObjectTable()
    {
        //AttackObjectList = new();
        XDocument doc = XDocument.Load($"{dataRootPath}/{attackTableName}.xml");
        var dataElements = doc.Descendants("data");

        foreach (var data in dataElements)
        {
            var objectStatus = new AttackObjectStatus();
            objectStatus.datainfo = data.Attribute(nameof(objectStatus.datainfo)).Value;
            objectStatus.description = data.Attribute(nameof(objectStatus.description)).Value;
            objectStatus.ID = int.Parse(data.Attribute(nameof(objectStatus.ID)).Value);
            objectStatus.useCount = int.Parse(data.Attribute(nameof(objectStatus.useCount)).Value);
            objectStatus.durabillity = int.Parse(data.Attribute(nameof(objectStatus.durabillity)).Value);
            objectStatus.masslimit = int.Parse(data.Attribute(nameof(objectStatus.masslimit)).Value);
            objectStatus.mass = int.Parse(data.Attribute(nameof(objectStatus.mass)).Value);
            objectStatus.holdable = data.Attribute(nameof(objectStatus.holdable)).Value == "TRUE";
            objectStatus.collide = data.Attribute(nameof(objectStatus.collide)).Value == "TRUE";
            objectStatus.deletable = data.Attribute(nameof(objectStatus.deletable)).Value == "TRUE";
            objectStatus.type = (ObjectType)Enum.Parse(typeof(ObjectType), data.Attribute(nameof(objectStatus.type)).Value);
            
            objectStatus.atk = int.Parse(data.Attribute(nameof(objectStatus.atk)).Value);
            objectStatus.destroyimmediatly = data.Attribute(nameof(objectStatus.destroyimmediatly)).Value == "TRUE";
            PrefabCreator.CreateObjectPrefab(objectStatus);
        }
    }

}
