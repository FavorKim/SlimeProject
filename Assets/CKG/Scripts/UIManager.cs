using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

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

    public GameObject stagePrefab; // �ϳ��� �������� ������
    public Transform contentTransform;
    public int currentStageIndex = 0;
    private float targetX;
    private float moveSpeed = 20;
    public GameObject[] stages;

    private void Start()
    {
        SetupStages(1);
    }

    private void Update()
    {
        // Lerp�� ����Ͽ� �ε巴�� �̵�
        RectTransform rectTransform = contentTransform.GetComponent<RectTransform>();
        float newX = Mathf.Lerp(rectTransform.anchoredPosition.x, targetX, Time.deltaTime * moveSpeed);
        rectTransform.anchoredPosition = new Vector2(newX, rectTransform.anchoredPosition.y);
    }

    public void SetupStages(int chapter)
    {
        // é�Ϳ� ���� �������� ���� ����
        int stageCount = GetStageCountForChapter(chapter);

        // ���� ���������� �ִٸ� ����
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }

        // ���ο� �������� �迭 �ʱ�ȭ
        stages = new GameObject[stageCount];

        // �������� �ν��Ͻ�ȭ
        for (int i = 0; i < stageCount; i++)
        {
            stages[i] = Instantiate(stagePrefab, contentTransform);

            // �������� �ؽ�Ʈ ������Ʈ
            Transform childTranform = stages[i].transform.Find("Img_StageTitle");
            TextMeshProUGUI stageText = childTranform.GetComponentInChildren<TextMeshProUGUI>();
            if (stageText != null)
            {
                stageText.text =  chapter + " - " + (i + 1);
            }

            // HoverButton ������Ʈ�� �߰�
            HoverButton hoverButton = stages[i].AddComponent<HoverButton>();
            hoverButton.button = stages[i].GetComponentInChildren<Button>();
            hoverButton.imageRectTransform = stages[i].GetComponent<RectTransform>();
        }

        currentStageIndex = 0;
        UpdateStagePosition();
    }

    private int GetStageCountForChapter(int chapter)
    {
        // é�Ϳ� ���� �������� ���� ���� ����
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
            targetX = 0; // �⺻ ��ġ
        }
    }

    
}
