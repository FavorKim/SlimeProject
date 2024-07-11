using UnityEngine;

public enum ObjectType
{
    normal
}

public class ObjectBase : MonoBehaviour
{
    public string datainfo;
    public string description;
    public int ID;
    public int usecount;
    public int durabillity;
    public int masslimit;
    public int mass;
    public bool holdable;
    public bool collide;
    public bool deletable;
    public ObjectType type;
}
