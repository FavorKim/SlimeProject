using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SelectChapterManager : MonoBehaviour
{
    public List<GameObject> chapterButtons; // é�� ��ư��
    public List<GameObject> chapterInfoImages; // é�� ���� �̹�����
    public List<Button> chapterSelectionButtons; // �������� ���� ��ư��
    public Sprite unlockedChapterSprite;
    private ChapterClearData chapterClearData;
    public GameObject ClearFlagPrefab;
    private string saveFilePath;

    private void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "chapterClearData.json");
        LoadChapterClearData();

        // ��� é�� ���� �̹����� �������� ���� ��ư ��Ȱ��ȭ
        foreach (var infoImage in chapterInfoImages)
        {
            infoImage.SetActive(false); 
        }
        //ù��° é�� �����̹��� Ȱ��ȭ
        if (chapterInfoImages.Count > 0)
        {
            chapterInfoImages[0].SetActive(true);
        }
        // é�� ��ư Ŭ�� �̺�Ʈ ����
        for (int i = 0; i < chapterButtons.Count; i++)
        {
            int chapterIndex = i; // ���� ������ ĸó
            Button buttonComponent = chapterButtons[i].GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => OnChapterButtonClicked(chapterIndex));
            }
        }

        // é�� Ŭ���� ���¿� ���� �������� ���� ��ư Ȱ��ȭ/��Ȱ��ȭ
        UpdateChapterSelectionButtons();
    }

    private void OnChapterButtonClicked(int chapterIndex)
    {
        // ��� é�� ���� �̹��� ��Ȱ��ȭ
        foreach (var infoImage in chapterInfoImages)
        {
            infoImage.SetActive(false);
        }

        // ������ é���� ���� �̹��� Ȱ��ȭ
        chapterInfoImages[chapterIndex].SetActive(true);
    }

    private void UpdateChapterSelectionButtons()
    {
        // é�� Ŭ���� ���¿� ���� ��ư Ȱ��ȭ/��Ȱ��ȭ
        for (int i = 1; i < chapterSelectionButtons.Count; i++)
        {
            if (chapterClearData.chapterCleared[i-1])
            {
                chapterSelectionButtons[i].interactable = true;
                Transform imageTransform = chapterInfoImages[i].transform.GetChild(0);
                Image imageComponent = imageTransform.GetComponent<Image>();
                imageComponent.sprite = unlockedChapterSprite;
                if (imageTransform.Find("ClearFlagPrefab") == null)
                {
                    RectTransform clearFlagPos = imageTransform.GetComponent<RectTransform>();
                    GameObject clearFlagInstance = Instantiate(ClearFlagPrefab, clearFlagPos);
                    clearFlagInstance.name = "ClearFlagPrefab"; // �ν��Ͻ� �̸� ����
                }
            }
            else
            {
                chapterSelectionButtons[i].interactable = false;
                chapterInfoImages[i].transform.GetChild(0).GetComponent<Image>().color = Color.gray;
            }
        }
    }

    public void ClearChapter(int chapterIndex)
    {
        // é�� Ŭ���� ���� ������Ʈ
        chapterClearData.chapterCleared[chapterIndex] = true;
        SaveChapterClearData();

        // �������� ���� ��ư ������Ʈ
        UpdateChapterSelectionButtons();
    }

    private void LoadChapterClearData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            chapterClearData = JsonUtility.FromJson<ChapterClearData>(json);
        }
        else
        {
            chapterClearData = new ChapterClearData(chapterSelectionButtons.Count);
        }
    }

    private void SaveChapterClearData()
    {
        string json = JsonUtility.ToJson(chapterClearData);
        File.WriteAllText(saveFilePath, json);
    }
}

