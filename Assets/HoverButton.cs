using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    public Button button;
    private void Start()
    {
        button.gameObject.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        button.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.gameObject.SetActive(false);
    }
}
