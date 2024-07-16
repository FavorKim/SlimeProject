using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChapterSelectionUI : MonoBehaviour
{
    public GameObject chapterButtonPrefab; // é�� ��ư ������
    public Transform chapterButtonContainer; // é�� ��ư�� ��ġ�� �θ� ������Ʈ
    public GameObject scrollview;
    
    private void Start()
    {
        // NullReferenceException ����: �ʵ� �Ҵ� ���� Ȯ��
        if (chapterButtonPrefab == null)
        {
            return;
        }
        if (chapterButtonContainer == null)
        {
            return;
        }
       


        // é�� ��ư ����
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
            int chapterIndex = i+1; // ���� ������ ĸó
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
