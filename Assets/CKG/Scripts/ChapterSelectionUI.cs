using UnityEngine;
using UnityEngine.UI;

public class ChapterSelectionUI : MonoBehaviour
{
    public GameObject chapterButtonPrefab;
    public Transform chapterButtonContainer;

    private void Start()
    {
        // 챕터 버튼 생성 (예시: 챕터 1, 2, 3)
        for (int i = 1; i <= 3; i++)
        {
            GameObject button = Instantiate(chapterButtonPrefab, chapterButtonContainer);
            button.GetComponentInChildren<Text>().text = "Chapter " + i;
            int chapterIndex = i; // 로컬 변수로 캡처
            button.GetComponent<Button>().onClick.AddListener(() => OnChapterButtonClicked(chapterIndex));
        }
    }

    private void OnChapterButtonClicked(int chapter)
    {
        UIManager.Instance.SetupStages(chapter);
        UIManager.Instance.ShowStageSelection();
    }
}
