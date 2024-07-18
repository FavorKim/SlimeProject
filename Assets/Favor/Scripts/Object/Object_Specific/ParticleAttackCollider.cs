using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAttackCollider : MonoBehaviour
{
    [SerializeField]AttackObjectBase attackObject;

    

    private void OnParticleCollision(GameObject other)
    {
        if(other.TryGetComponent(out ObjectBase obj))
        {
            obj.GetDamage(attackObject.atk);
        }
    }
}
