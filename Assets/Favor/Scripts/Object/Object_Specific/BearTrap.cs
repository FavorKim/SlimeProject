using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    Animator anim;
    AttackObjectBase atkObj;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        atkObj = GetComponent<AttackObjectBase>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ObjectBase obj) && atkObj.UseCount>0)
        {
            if(obj.ID == 31001)
            {
                anim.SetTrigger("Trigger");
                obj.transform.SetParent(transform, false);
                atkObj.UseCount = atkObj.usecount - 1;
            }
        }
    }
}
