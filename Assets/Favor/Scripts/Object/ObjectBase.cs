using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBase : MonoBehaviour
{
    ObjectStatus status;
    public ObjectStatus GetStatus() {  return status; }
    public void InitStatus(ObjectStatus status)
    {
        this.status = status;
    }
}
