using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isUsed;
    public bool GetIsUsed() { return isUsed; }

    Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void OpenDoor()
    {
        if (!isUsed)
        {
            anim.SetTrigger("Open");
            isUsed = true;
        }
    }

    public void CloseDoor()
    {
        if (isUsed)
        {
            anim.SetTrigger("Close");
            isUsed = false;
        }
    }
}
