using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    [SerializeField] AttackObjectBase atkObj;

    private void Update()
    {
        RayCastSlime();
    }

    void RayCastSlime()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1.5f))
        {
            if(hit.collider.TryGetComponent(out ObjectBase obj))
            {
                obj.GetDamage(atkObj.atk);
            }
        }
        Debug.DrawRay(transform.position, transform.forward * 1.5f, Color.red);
    }
}
