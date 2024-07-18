using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChapterSelectionUI : MonoBehaviour
{
    public GameObject chapterButtonPrefab; // 챕터 버튼 프리팹
    public Transform chapterButtonContainer; // 챕터 버튼을 배치할 부모 오브젝트
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
       


        // 챕터 버튼 생성
        for (int i = 0; i < 3; i++)
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
            int chapterIndex = i+1; // 로컬 변수로 캡처
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
        UIManager.Instance.SetupStages(chapterIndex);
        chapterButtonPrefab.GetComponentInChildren<TextMeshProUGUI>().text = "Chapter " + chapterIndex;
    }
}
