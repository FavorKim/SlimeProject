using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SelectChapterManager : MonoBehaviour
{
    public List<GameObject> chapterButtons; // 챕터 버튼들
    public List<GameObject> chapterInfoImages; // 챕터 정보 이미지들
    public List<Button> stageSelectionButtons; // 스테이지 선택 버튼들

    private ChapterClearData chapterClearData;
    private string saveFilePath;

    private void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "chapterClearData.json");
        LoadChapterClearData();

        // 모든 챕터 정보 이미지와 스테이지 선택 버튼 비활성화
        foreach (var infoImage in chapterInfoImages)
        {
            infoImage.SetActive(false);
        }

        // 챕터 버튼 클릭 이벤트 연결
        for (int i = 0; i < chapterButtons.Count; i++)
        {
            int chapterIndex = i; // 로컬 변수로 캡처
            Button buttonComponent = chapterButtons[i].GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => OnChapterButtonClicked(chapterIndex));
            }
        }

        // 챕터 클리어 상태에 따른 스테이지 선택 버튼 활성화/비활성화
        UpdateStageSelectionButtons();
    }

    private void OnChapterButtonClicked(int chapterIndex)
    {
        // 모든 챕터 정보 이미지 비활성화
        foreach (var infoImage in chapterInfoImages)
        {
            infoImage.SetActive(false);
        }

        // 선택한 챕터의 정보 이미지 활성화
        chapterInfoImages[chapterIndex].SetActive(true);
    }

    private void UpdateStageSelectionButtons()
    {
        // 챕터 클리어 상태에 따른 버튼 활성화/비활성화
        for (int i = 0; i < stageSelectionButtons.Count; i++)
        {
            stageSelectionButtons[i].interactable = chapterClearData.chapterCleared[i];
        }
    }

    public void ClearChapter(int chapterIndex)
    {
        // 챕터 클리어 상태 업데이트
        chapterClearData.chapterCleared[chapterIndex] = true;
        SaveChapterClearData();

        // 스테이지 선택 버튼 업데이트
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

