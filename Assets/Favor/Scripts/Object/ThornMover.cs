using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornMover : MonoBehaviour
{
    [SerializeField] LayerMask slimeLayer;
    [SerializeField] bool isMove;
    Animator anim;

    private void Awake()
    {
        if (!isMove)
            gameObject.GetComponent<ThornMover>().enabled = false;
        else
            anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        anim?.SetBool("isMove", true);
    }

    private void OnTriggerExit(Collider other)
    {
        anim?.SetBool("isMove", false);
    }
}
