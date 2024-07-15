using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChapterSelectionUI : MonoBehaviour
{
    public GameObject chapterButtonPrefab; // é�� ��ư ������
    public Transform chapterButtonContainer; // é�� ��ư�� ��ġ�� �θ� ������Ʈ
    public List<GameObject> chapterCanvases; // �� é���� ĵ������
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
        if (chapterCanvases == null || chapterCanvases.Count == 0)
        {
            return;
        }
       


        // é�� ��ư ����
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
            int chapterIndex = i; // ���� ������ ĸó
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
        // ���� Ȱ��ȭ�� é�� ĵ������ ��Ȱ��ȭ
        foreach (var canvas in chapterCanvases)
        {
            canvas.SetActive(false);
        }

        // ������ é���� ĵ������ Ȱ��ȭ
        chapterCanvases[chapterIndex].SetActive(true);
    }
}
