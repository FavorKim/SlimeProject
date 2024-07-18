
using Player;
using System;
using UnityEngine;

public class AttackObjectBase : ObjectBase
{
    public int atk;
    public bool destroyimmediatly;
    public bool Pinable = false;

    public event Action OnAttack;

    private void OnEnable()
    {
        OnAttack += OnAttack_DecreaseUseCount;
    }

    private void OnDisable()
    {
        OnAttack -= OnAttack_DecreaseUseCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (UseCount <= 0) return;
        if (other.TryGetComponent(out ObjectBase obj))
        {

            if (destroyimmediatly && obj.deletable)
            {
                obj.gameObject.SetActive(false);
            }
            else
            {
                obj.GetDamage(atk);
            }

            if (obj.ID == 31001 && Pinable)
            {
                if (obj.TryGetComponent(out Rigidbody rb))
                {
                    rb.velocity = Vector3.zero;
                    rb.useGravity = false;
                }
                obj.transform.SetParent(transform, false);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
            }

        }
        OnAttack.Invoke();
    }

    private void OnAttack_DecreaseUseCount()
    {
        Debug.Log("OnAttack");
        UseCount = usecount - 1;
    }
}
