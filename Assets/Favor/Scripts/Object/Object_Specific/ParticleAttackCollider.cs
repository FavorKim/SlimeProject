using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAttackCollider : MonoBehaviour
{
    [SerializeField]AttackObjectBase attackObject;

    ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.TryGetComponent(out ObjectBase obj))
        {
            if (obj.durabillity > 0)
            {
                obj.GetDamage(attackObject.atk);
                particle.Stop();
            }
        }
        else if(other.TryGetComponent(out SlimeController slime))
        {
            slime.HealthPoint--;
            slime.ChangeState(new DeadState(slime, attackObject));
        }
    }
}
