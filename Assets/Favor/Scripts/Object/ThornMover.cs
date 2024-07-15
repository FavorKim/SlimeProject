using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornMover : MonoBehaviour
{
    [SerializeField]LayerMask slimeLayer;
    [SerializeField] bool isMove;
    Animator anim;

    private void Awake()
    {
        if (!isMove)
            gameObject.GetComponent<ThornMover>().enabled = false;
        else
            anim = GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == slimeLayer)
        {
            Debug.Log("Trig");
            anim.SetBool("isMove", true);
        }
        Debug.Log("Trig");

    }
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == slimeLayer)
        {
            anim.SetBool("isMove", false);
        }
    }
}
