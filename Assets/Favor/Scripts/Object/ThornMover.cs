using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornMover : MonoBehaviour
{
    int slimeLayer = 1 << 6;
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
        if (other.gameObject.layer == slimeLayer)
        {
            anim.SetBool("isMove", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == slimeLayer)
        {
            anim.SetBool("isMove", false);
        }
    }
}
