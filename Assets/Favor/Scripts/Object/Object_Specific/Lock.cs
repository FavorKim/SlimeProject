using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    Door door;

    private void Start()
    {
        door = GetComponentInParent<Door>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key") && !door.GetIsUsed())
        {
            other.gameObject.SetActive(false);
            door.OpenDoor();
            gameObject.SetActive(false);
        }
    }
}
