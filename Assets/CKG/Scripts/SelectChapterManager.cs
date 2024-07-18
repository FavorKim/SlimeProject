using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SelectChapterManager : MonoBehaviour
{
    public List<GameObject> chapterButtons; // é�� ��ư��
    public List<GameObject> chapterInfoImages; // é�� ���� �̹�����
    public List<Button> stageSelectionButtons; // �������� ���� ��ư��

    private ChapterClearData chapterClearData;
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
        UpdateStageSelectionButtons();
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

    private void UpdateStageSelectionButtons()
    {
        // é�� Ŭ���� ���¿� ���� ��ư Ȱ��ȭ/��Ȱ��ȭ
        for (int i = 0; i < stageSelectionButtons.Count; i++)
        {
            stageSelectionButtons[i].interactable = chapterClearData.chapterCleared[i];
        }
    }

    public void ClearChapter(int chapterIndex)
    {
        // é�� Ŭ���� ���� ������Ʈ
        chapterClearData.chapterCleared[chapterIndex] = true;
        SaveChapterClearData();

        // �������� ���� ��ư ������Ʈ
        UpdateStageSelectionButtons();
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
            chapterClearData = new ChapterClearData(stageSelectionButtons.Count);
        }
    }

    private void SaveChapterClearData()
    {
        string json = JsonUtility.ToJson(chapterClearData);
        File.WriteAllText(saveFilePath, json);
    }
}

