using System;
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
        atkObj.OnAttack += OnAttack_PlayAnim;
    }

    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }
    private void OnDestroy()
    {
        atkObj.OnAttack -= OnAttack_PlayAnim;
        
    }

    private void OnAttack_PlayAnim()
    {
        anim.SetTrigger("Trigger");
    }
}
