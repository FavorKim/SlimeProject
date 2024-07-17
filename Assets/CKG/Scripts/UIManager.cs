using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject stagePrefab; // 하나의 스테이지 프리팹
    public Transform contentTransform;
    public int currentStageIndex = 0;
    private float targetX;
    private float moveSpeed = 20;
    public GameObject[] stages;

    private string saveFilePath;
    private StageClearData stageClearData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "stageClearData.json");
        LoadStageClearData();
        SetupStages(1);
    }

    private void Update()
    {
        // Lerp를 사용하여 부드럽게 이동
        RectTransform rectTransform = contentTransform.GetComponent<RectTransform>();
        float newX = Mathf.Lerp(rectTransform.anchoredPosition.x, targetX, Time.deltaTime * moveSpeed);
        rectTransform.anchoredPosition = new Vector2(newX, rectTransform.anchoredPosition.y);
    }

    public void SetupStages(int chapter)
    {
        // 챕터에 따른 스테이지 개수 설정
        int stageCount = GetStageCountForChapter(chapter);

        // 기존 스테이지가 있다면 제거
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }

        // 새로운 스테이지 배열 초기화
        stages = new GameObject[stageCount];

        // 스테이지 인스턴스화
        for (int i = 0; i < stageCount; i++)
        {
            stages[i] = Instantiate(stagePrefab, contentTransform);

            // 스테이지 텍스트 업데이트
            Transform childTranform = stages[i].transform.Find("Img_StageTitle");
            TextMeshProUGUI stageText = childTranform.GetComponentInChildren<TextMeshProUGUI>();
            if (stageText != null)
            {
                stageText.text = chapter + " - " + (i + 1);
            }

            // HoverButton 컴포넌트를 추가
            HoverButton hoverButton = stages[i].AddComponent<HoverButton>();
            hoverButton.button = stages[i].GetComponentInChildren<Button>();
            hoverButton.imageRectTransform = stages[i].GetComponent<RectTransform>();

            // 스테이지 클리어 상태 업데이트
            UpdateStageClearStatus(i);
        }

        currentStageIndex = 0;
        UpdateStagePosition();
    }

    private int GetStageCountForChapter(int chapter)
    {
        // 챕터에 따른 스테이지 개수 설정 로직
        switch (chapter)
        {
            case 1:
                return 5;
            case 2:
                return 7;
            default:
                return 3;
        }
    }

    public void MoveStageLeft()
    {
        if (currentStageIndex > 0)
        {
            currentStageIndex--;
            UpdateStagePosition();
        }
    }

    public void MoveStageRight()
    {
        if (currentStageIndex < stages.Length - 1)
        {
            currentStageIndex++;
            UpdateStagePosition();
        }
    }

    private void UpdateStagePosition()
    {
        if (currentStageIndex > 1)
        {
            targetX = -120f;
            if (currentStageIndex > 2)
            {
                targetX = -(120f + 180 * (currentStageIndex - 2));
            }
        }
        else
        {
            targetX = 0; // 기본 위치
        }
    }

    private void UpdateStageClearStatus(int stageIndex)
    {
        if (stageClearData != null && stageIndex < stageClearData.stageCleared.Count)
        {
            Transform childTranform = stages[stageIndex].transform.Find("Img_StageTitle");
            Image stageImage = childTranform.GetComponent<Image>();

            if (stageIndex == 0)
            {
                // 첫 번째 스테이지는 항상 빨간색
                stageImage.color = Color.red;
            }
            else
            {
                if (stageClearData.stageCleared[stageIndex])
                {
                    stageImage.color = Color.green; // 클리어된 스테이지
                }
                if (stageClearData.stageCleared[stageIndex+1])
                {
                    stageImage.color = Color.red;
                }
                else
                {
                    stageImage.color = Color.gray; // 클리어되지 않은 스테이지
                }
            }
        }
    }

    public void ClearStage(int stageIndex)
    {
        if (stageClearData != null && stageIndex < stageClearData.stageCleared.Count)
        {
            stageClearData.stageCleared[stageIndex] = true;
            SaveStageClearData();
            UpdateStageClearStatus(stageIndex);
        }
    }

    private void LoadStageClearData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            stageClearData = JsonUtility.FromJson<StageClearData>(json);
        }
        else
        {
            int totalStages = GetStageCountForChapter(1) + GetStageCountForChapter(2); // 모든 챕터의 총 스테이지 수
            stageClearData = new StageClearData(totalStages);
        }
    }

    private void SaveStageClearData()
    {
        string json = JsonUtility.ToJson(stageClearData);
        File.WriteAllText(saveFilePath, json);
    }
}


