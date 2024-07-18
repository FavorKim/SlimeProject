using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] float springPow;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * springPow);
        }
        else if (other.gameObject.transform.root.TryGetComponent(out Rigidbody rbParent))
        {
            rbParent.velocity = Vector3.zero;
            rbParent.AddForce(Vector3.up * springPow);
        }
    }

}
