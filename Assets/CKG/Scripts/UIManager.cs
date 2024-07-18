using System.Collections;
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

    public GameObject[] stagePrefab;
    public Transform contentTransform;
    public GameObject SelectLine;
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
        // Lerp�� ����Ͽ� �ε巴�� �̵� contentTransform�� �⺻ �����ǿ��� targetX�� lerp�� ����Ͽ� �̵�
        RectTransform rectTransform = contentTransform.GetComponent<RectTransform>();
        float newX = Mathf.Lerp(rectTransform.anchoredPosition.x, targetX, Time.deltaTime * moveSpeed);
        rectTransform.anchoredPosition = new Vector2(newX, rectTransform.anchoredPosition.y);
    }

    public void SetupStages(int chapter)
    {
        // é�Ϳ� ���� �������� ������ �ٸ��� ����
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
            stages[i] = Instantiate(stagePrefab[i], contentTransform);
            RectTransform rectTransform = stages[i].GetComponent<RectTransform>();
            float newX = 35 + (i * 180);
            rectTransform.anchoredPosition = new Vector2(newX, 0);

            // HoverButton ������Ʈ�� �߰�
            HoverButton hoverButton = stages[i].AddComponent<HoverButton>();
            hoverButton.button = stages[i].GetComponentInChildren<Button>();
            hoverButton.imageRectTransform = rectTransform;
            hoverButton.selectLineRectTransform = SelectLine.GetComponent<RectTransform>();
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
        StartCoroutine(PointCurrentStage());
    }

    IEnumerator PointCurrentStage() // � ���������� �����ϰ� �ִ��� �˷��ִ� �ƿ�����
    {
        if (currentStageIndex >= 0 && currentStageIndex < stages.Length)
        {
            // ���� ���õ� ���������� RectTransform�� ������
            RectTransform selectedStageRect = stages[currentStageIndex].GetComponent<RectTransform>();
            // SelectLine�� RectTransform�� ������
            RectTransform selectLineRect = SelectLine.GetComponent<RectTransform>();
            // SelectLine�� ��ġ�� ���õ� ���������� ��ġ�� ����
            SelectLine.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            SelectLine.SetActive(true);
            selectLineRect.anchoredPosition = new Vector2(selectedStageRect.anchoredPosition.x, selectLineRect.anchoredPosition.y);
            if (currentStageIndex > 1)
            {
                selectLineRect.anchoredPosition = new Vector2(275, 0);
            }
        }
    }
}
