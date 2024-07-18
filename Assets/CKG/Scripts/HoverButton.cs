using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    public RectTransform imageRectTransform; // UI 이미지의 RectTransform
    public RectTransform selectLineRectTransform;
    private Vector2 imgOriginalSize = new Vector2(160, 200);
    private Vector2 selectLineOriginalSize = new Vector2(170, 210);
    public Vector2 hoverSize = new Vector2(1.1f, 1.1f);

    private void Start()
    {
        button.gameObject.SetActive(false);

        // 초기 크기 설정 (필요시)
        if (imageRectTransform != null)
        {
            imgOriginalSize = imageRectTransform.sizeDelta;
        }
        if (selectLineRectTransform != null)
        {
            selectLineOriginalSize = selectLineRectTransform.sizeDelta;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UIManager.Instance.currentStageIndex != transform.GetSiblingIndex())
        {
            return;
        }

        button.gameObject.SetActive(true);
        if (imageRectTransform != null && selectLineRectTransform != null)
        {
            selectLineRectTransform.gameObject.SetActive(true);

            // 크기 변경 (원래 크기에서 비율을 곱하여 조정)
            imageRectTransform.sizeDelta = new Vector2(imgOriginalSize.x * hoverSize.x, imgOriginalSize.y * hoverSize.y);
            selectLineRectTransform.sizeDelta = new Vector2(selectLineOriginalSize.x * hoverSize.x, selectLineOriginalSize.y * hoverSize.y);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.gameObject.SetActive(false);
        if (imageRectTransform != null && selectLineRectTransform != null)
        {
            // 원래 크기로 되돌림
            imageRectTransform.sizeDelta = imgOriginalSize;
            selectLineRectTransform.sizeDelta = selectLineOriginalSize;
        }
    }
}
