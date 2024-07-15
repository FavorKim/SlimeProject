using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChapterSelectionUI : MonoBehaviour
{
    public GameObject chapterButtonPrefab; // 챕터 버튼 프리팹
    public Transform chapterButtonContainer; // 챕터 버튼을 배치할 부모 오브젝트
    public List<GameObject> chapterCanvases; // 각 챕터의 캔버스들
    public GameObject scrollview;
    
    private void Start()
    {
        // NullReferenceException 방지: 필드 할당 여부 확인
        if (chapterButtonPrefab == null)
        {
            return;
        }
        if (chapterButtonContainer == null)
        {
            return;
        }
        if (chapterCanvases == null || chapterCanvases.Count == 0)
        {
            return;
        }
       


        // 챕터 버튼 생성
        for (int i = 0; i < chapterCanvases.Count; i++)
        {
           
            GameObject button = Instantiate(chapterButtonPrefab, chapterButtonContainer);
           
            TextMeshProUGUI textComponent = button.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = "Chapter " + (i + 1);
            }
            else
            {
                
            }
            int chapterIndex = i; // 로컬 변수로 캡처
            Button buttonComponent = button.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => OnChapterButtonClicked(chapterIndex));
            }
            else
            {
               
            }
        }
        scrollview.SetActive(false);
    }

    public void OnChapterTitleClicked()
    {
        if (scrollview.activeSelf)
        {
            scrollview.SetActive(false);
        }
        else
            scrollview.SetActive(true);
    }
    private void OnChapterButtonClicked(int chapterIndex)
    {
        // 현재 활성화된 챕터 캔버스를 비활성화
        foreach (var canvas in chapterCanvases)
        {
            canvas.SetActive(false);
        }

        // 선택한 챕터의 캔버스를 활성화
        chapterCanvases[chapterIndex].SetActive(true);
    }
}
