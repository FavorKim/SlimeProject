using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Xml.Serialization;
using System.Collections;

public class ChapterSelectionUI : MonoBehaviour
{
    public GameObject ChapterButtonPrefab; // é�� ��ư ������
    public Transform  ChapterButtonContainer; // é�� ��ư�� ��ġ�� �θ� ������Ʈ
    public GameObject Scrollview;
    public GameObject SubChapterTitle;
    
    private void Start()
    {
        // NullReferenceException ����: �ʵ� �Ҵ� ���� Ȯ��
        if (ChapterButtonPrefab == null)
        {
            return;
        }
        if (ChapterButtonContainer == null)
        {
            return;
        }
       


        // é�� ��ư ����
        for (int i = 0; i < 3; i++)
        {
           
            GameObject button = Instantiate(ChapterButtonPrefab, ChapterButtonContainer);
           
            TextMeshProUGUI textComponent = button.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = "é�� " + (i + 1);
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
        ChapterButtonPrefab.GetComponentInChildren<TextMeshProUGUI>().text = "é�� " + chapterIndex;
        SubChapterTitleSet(chapterIndex);
    }

    private void SubChapterTitleSet(int chapterIndex)
    {
        switch(chapterIndex)
        {
            case 1:
                SubChapterTitle.GetComponent<TextMeshProUGUI>().text = "������ ����";
                break;
            case 2:
                SubChapterTitle.GetComponent<TextMeshProUGUI>().text = "é�� 2 ������";
                break;
            default:
                SubChapterTitle.GetComponent<TextMeshProUGUI>().text = "é�� 3 ������";
                return;
        }
    }
}
