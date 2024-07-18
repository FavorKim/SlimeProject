using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Xml.Serialization;
using System.Collections;

public class ChapterSelectionUI : MonoBehaviour
{
    public GameObject ChapterButtonPrefab; // 챕터 버튼 프리팹
    public Transform  ChapterButtonContainer; // 챕터 버튼을 배치할 부모 오브젝트
    public GameObject Scrollview;
    public GameObject SubChapterTitle;
    
    private void Start()
    {
        // NullReferenceException 방지: 필드 할당 여부 확인
        if (ChapterButtonPrefab == null)
        {
            return;
        }
        if (ChapterButtonContainer == null)
        {
            return;
        }
       


        // 챕터 버튼 생성
        for (int i = 0; i < 3; i++)
        {
           
            GameObject button = Instantiate(ChapterButtonPrefab, ChapterButtonContainer);
           
            TextMeshProUGUI textComponent = button.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = "챕터 " + (i + 1);
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
        Scrollview.SetActive(false);
    }

    public void OnChapterTitleClicked()
    {
        if (Scrollview.activeSelf)
        {
            Scrollview.SetActive(false);
        }
        else
            Scrollview.SetActive(true);
    }
    private void OnChapterButtonClicked(int chapterIndex)
    {
        UIManager.Instance.SetupStages(chapterIndex);
        ChapterButtonPrefab.GetComponentInChildren<TextMeshProUGUI>().text = "챕터 " + chapterIndex;
        SubChapterTitleSet(chapterIndex);
    }

    private void SubChapterTitleSet(int chapterIndex)
    {
        switch(chapterIndex)
        {
            case 1:
                SubChapterTitle.GetComponent<TextMeshProUGUI>().text = "모험의 시작";
                break;
            case 2:
                SubChapterTitle.GetComponent<TextMeshProUGUI>().text = "챕터 2 부제목";
                break;
            default:
                SubChapterTitle.GetComponent<TextMeshProUGUI>().text = "챕터 3 부제목";
                return;
        }
    }
}
