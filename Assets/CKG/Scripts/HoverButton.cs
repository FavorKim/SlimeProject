using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    public RectTransform imageRectTransform; // UI 이미지의 RectTransform
    private Vector2 imgOriginalSize = new Vector2(160, 200);
    public Vector2 hoverSize = new Vector2(1.1f, 1.1f);

    private void Start()
    {
        button.gameObject.SetActive(false);

        // 초기 크기 설정 (필요시)
        if (imageRectTransform != null)
        {
            imgOriginalSize = imageRectTransform.sizeDelta;
        }

        button.onClick.AddListener(OnStageButtonClicked);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UIManager.Instance.currentStageIndex == transform.GetSiblingIndex())
        {
            if (UIManager.Instance.stageClearData.stageCleared[transform.GetSiblingIndex()] || transform.GetSiblingIndex() == 0)
            {
                button.gameObject.SetActive(true);
                if (imageRectTransform != null)
                {
                    // 크기 변경 (원래 크기에서 비율을 곱하여 조정)
                    imageRectTransform.sizeDelta = new Vector2(imgOriginalSize.x * hoverSize.x, imgOriginalSize.y * hoverSize.y);
                }
            }
        }
        else return;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.gameObject.SetActive(false);
        if (UIManager.Instance.currentStageIndex == transform.GetSiblingIndex())
        {
            if (imageRectTransform != null)
            {
                // 원래 크기로 되돌림
                imageRectTransform.sizeDelta = imgOriginalSize;
            }
        }
    }

    public void OnStageButtonClicked()
    {
        if(UIManager.Instance.stageClearData.stageCleared[transform.GetSiblingIndex()] || transform.GetSiblingIndex() == 0)
        {
            SceneManager.LoadScene(transform.GetSiblingIndex()+4);
        }
    }
}
