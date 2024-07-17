using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Button : MonoBehaviour,IPushable
{
    ObjectBase objectBase;

    [SerializeField] Door LinkedDoor;
    
    [SerializeField]
    GameObject clickPart;
    [SerializeField]Rigidbody clickPartRb;

    [SerializeField] float clickPartOriginHeight;
    [SerializeField] float resist;
    
    private void Awake()
    {
        objectBase = GetComponent<ObjectBase>();
        clickPartOriginHeight = clickPart.transform.localPosition.y;
        clickPartRb = clickPart.GetComponent<Rigidbody>();
    }

    public void Push()
    {
        LinkedDoor?.OpenDoor();
    }
    public void UnPush()
    {

    }

    private void Update()
    {
        if (clickPart.transform.localPosition.y < clickPartOriginHeight)
        {
            clickPartRb.AddForce(Vector3.up * objectBase.masslimit * Time.deltaTime * resist);
        }
        else
        {
            clickPartRb.velocity = Vector3.zero;
        }
        if (clickPart.transform.localPosition.y <= 0)
        {
            Push();
        }
    }

}
