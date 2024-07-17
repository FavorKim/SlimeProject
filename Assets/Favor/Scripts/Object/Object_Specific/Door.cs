using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isUsed;

    Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void OpenDoor()
    {
        anim.SetTrigger("Open");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isUsed && other.CompareTag("Key"))
        {
            isUsed = true;
            OpenDoor();
            other.gameObject.SetActive(false);
        }
    }
}
