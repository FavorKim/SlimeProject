
public enum ObjectType
{
    normal
}

public class ObjectStatus
{
    public string datainfo;
    public string description;
    public int ID;
    public int useCount;
    public int durabillity;
    public int masslimit;
    public int mass;
    public bool holdable;
    public bool collide;
    public bool deletable;
    public ObjectType type;
}

public class AttackObjectStatus : ObjectStatus
{
    public int atk;
    public bool destroyimmediatly;
}
