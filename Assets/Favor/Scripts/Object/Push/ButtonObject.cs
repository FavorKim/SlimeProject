using UnityEngine;

public class ButtonObject : MonoBehaviour,IPushable
{
    ObjectBase objectBase;

    [SerializeField] Door LinkedDoor;
    
    [SerializeField]
    GameObject clickPart;
    Rigidbody clickPartRb;

    [SerializeField]float clickPartOriginHeight;
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
        LinkedDoor?.CloseDoor();
    }

    private void Update()
    {
        if (clickPart.transform.localPosition.y>clickPartOriginHeight)
        {
            clickPart.transform.localPosition = new Vector3(clickPart.transform.localPosition.x, clickPartOriginHeight, clickPart.transform.localPosition.z);
        }
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
        else
        {
            UnPush();
        }
    }

}
