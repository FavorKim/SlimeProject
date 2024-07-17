using UnityEngine;

public enum ObjectType
{
    normal
}

public class ObjectBase : MonoBehaviour
{
    [Space(20)]
    
    [Tooltip("데이터 정보")]
    public string datainfo;
    [Tooltip("오브젝트 이름")]
    public string description;
    
    [Space(20)]

    public int ID;
    [Tooltip("사용제한횟수")]
    public int usecount;
    public int UseCount
    {
        get { return usecount; }
        set 
        {
            if (value != usecount)
            {
                usecount = value;
                if (usecount <= 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
    [Tooltip("내구도")]
    public int durabillity;
    public int Durabillity
    {
        get { return durabillity; }
        set
        {
            if (durabillity != value)
            {
                durabillity = value;
                if (durabillity <= 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    [Tooltip("밟기 상호작용을 일으키는 무게 최소값")]
    public int masslimit;
    [Tooltip("물체의 질량(무게)")]
    public int mass;
    
    [Space(20)]

    [Tooltip("들수 있음의 여부")]
    public bool holdable;
    [Tooltip("충돌하는지의 여부")]
    public bool collide;
    [Tooltip("소멸가능한지의 여부")]
    public bool deletable;
    
    [Space(20)]

    [Tooltip("오브젝트의 타입")]
    public ObjectType type;


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out AttackObjectBase obj))
        {
            if (obj.destroyimmediatly && deletable)
            {
                gameObject.SetActive(false);
            }
            else
            {
                this.Durabillity = durabillity - obj.atk;
            }
            obj.UseCount = obj.usecount - 1;
        }
    }
}
