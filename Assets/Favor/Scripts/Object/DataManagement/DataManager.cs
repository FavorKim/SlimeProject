using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class DataManager : SingletonMono<DataManager>
{

    public Dictionary<string, ObjectStatus> LoadedObjectList;
    public Dictionary<string, AttackObjectStatus> AttackObjectList;

    //public readonly string rootPath = "C:/Users/KGA/Desktop/ProjectD_Data";

    public string connectionString = "server=3.36.69.87;database=projectd;user=root;password=1234";


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadAllPerkData();
    }

    void LoadAllPerkData()
    {
        LoadedObjectList = new Dictionary<string, ObjectStatus>();
        AttackObjectList = new();

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
            }
        }

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {

            connection.Open();

            string query = "SELECT * FROM attackobjecttable";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var objectStatus = new AttackObjectStatus();

                        objectStatus.datainfo = reader["datainfo"].ToString();
                        objectStatus.description = reader["description"].ToString();
                        objectStatus.ID = int.Parse(reader["ID"].ToString());
                        objectStatus.useCount = int.Parse(reader["useCount"].ToString());
                        objectStatus.durabillity = int.Parse(reader["durabillity"].ToString());
                        objectStatus.masslimit = int.Parse(reader["durabillity"].ToString());
                        objectStatus.mass = int.Parse(reader["mass"].ToString());
                        objectStatus.atk = int.Parse(reader["atk"].ToString());
                        objectStatus.holdable = reader["holdable"].ToString() == "TRUE";
                        objectStatus.collide = reader["collide"].ToString() == "TRUE";
                        objectStatus.deletable = reader["deletable"].ToString() == "TRUE";
                        objectStatus.destroyimmediatly = reader["destroyimmediatly"].ToString() == "TRUE";
                        objectStatus.type = (ObjectType)Enum.Parse(typeof(ObjectType), reader["type"].ToString());


                        AttackObjectList.Add(objectStatus.description, objectStatus);
                    }
                }
            }
        }


    }

}
