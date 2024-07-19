using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject stagePrefab;
    public Transform contentTransform;
    public int currentStageIndex = 0;
    private float targetX;
    private float moveSpeed = 20;
    public GameObject[] stages;
    public ChapterClearData chapterClearData;
    private string chapterClearFilePath;
    public StageClearData stageClearData;
    private string stageClearFilePath;
    public int SelectChapter = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        chapterClearFilePath = Path.Combine(Application.persistentDataPath, "chapterClearData.json");
        stageClearFilePath = Path.Combine(Application.persistentDataPath, "stageClearData.json");

        LoadChapterClearData();
        LoadStageClearData();

        int chapterIndex = SelectChapter;
        SetupStages(chapterIndex + 1);
    }

    private void Update()
    {
        // Lerp를 사용하여 부드럽게 이동
        RectTransform rectTransform = contentTransform.GetComponent<RectTransform>();
        float newX = Mathf.Lerp(rectTransform.anchoredPosition.x, targetX, Time.deltaTime * moveSpeed);
        rectTransform.anchoredPosition = new Vector2(newX, rectTransform.anchoredPosition.y);
    }

    private void LoadChapterClearData()
    {
        if (File.Exists(chapterClearFilePath))
        {
            string json = File.ReadAllText(chapterClearFilePath);
            chapterClearData = JsonUtility.FromJson<ChapterClearData>(json);
        }
        else
        {
            chapterClearData = new ChapterClearData(3);
        }
    }

    private void LoadStageClearData()
    {
        if (File.Exists(stageClearFilePath))
        {
            string json = File.ReadAllText(stageClearFilePath);
            stageClearData = JsonUtility.FromJson<StageClearData>(json);
        }
        else
        {
            int totalStages = GetStageCountForChapter(1) + GetStageCountForChapter(2);
            stageClearData = new StageClearData(totalStages);
        }
    }

    public void SetupStages(int chapter)
    {
        int stageCount = GetStageCountForChapter(chapter);

        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }

        stages = new GameObject[stageCount];

        for (int i = 0; i < stageCount; i++)
        {
            stages[i] = Instantiate(stagePrefab, contentTransform);

            Transform childTranform = stages[i].transform.Find("Img_StageTitle");
            TextMeshProUGUI stageText = childTranform.GetComponentInChildren<TextMeshProUGUI>();
            if (stageText != null)
            {
                stageText.text = chapter + " - " + (i + 1);
            }

            HoverButton hoverButton = stages[i].AddComponent<HoverButton>();
            hoverButton.button = stages[i].GetComponentInChildren<Button>();
            hoverButton.imageRectTransform = stages[i].GetComponent<RectTransform>();

            UpdateStageClearStatus(i);
        }

        currentStageIndex = 0;
        UpdateStagePosition();
    }

    private int GetStageCountForChapter(int chapter)
    {
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
            targetX = 0;
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
                stageImage.color = Color.red;
            }
            else
            {
                if (stageClearData.stageCleared[stageIndex])
                {
                    stageImage.color = Color.green;
                }
                if (stageClearData.stageCleared[stageIndex + 1])
                {
                    stageImage.color = Color.red;
                }
                else
                {
                    stageImage.color = Color.gray;
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

    private void SaveStageClearData()
    {
        string json = JsonUtility.ToJson(stageClearData);
        File.WriteAllText(stageClearFilePath, json);
    }
}


