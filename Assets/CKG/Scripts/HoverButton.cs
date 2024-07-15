using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    public RectTransform imageRectTransform; // UI �̹����� RectTransform
    public RectTransform selectLineRectTransform;
    private Vector2 imgOriginalSize = new Vector2(160, 200);
    private Vector2 selectLineOriginalSize = new Vector2(170, 210);
    public Vector2 hoverSize = new Vector2(1.1f, 1.1f);

    private void Start()
    {
        button.gameObject.SetActive(false);

        // �ʱ� ũ�� ���� (�ʿ��)
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

            // ũ�� ���� (���� ũ�⿡�� ������ ���Ͽ� ����)
            imageRectTransform.sizeDelta = new Vector2(imgOriginalSize.x * hoverSize.x, imgOriginalSize.y * hoverSize.y);
            selectLineRectTransform.sizeDelta = new Vector2(selectLineOriginalSize.x * hoverSize.x, selectLineOriginalSize.y * hoverSize.y);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.gameObject.SetActive(false);
        if (imageRectTransform != null && selectLineRectTransform != null)
        {
            // ���� ũ��� �ǵ���
            imageRectTransform.sizeDelta = imgOriginalSize;
            selectLineRectTransform.sizeDelta = selectLineOriginalSize;
        }
    }
}
