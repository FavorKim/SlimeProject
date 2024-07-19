using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class SelectChapterManager : MonoBehaviour
{
    public List<GameObject> chapterButtons; // 챕터 버튼들
    public List<GameObject> chapterInfoImages; // 챕터 정보 이미지들
    public List<Button> chapterSelectionButtons;
    public List<GameObject> SlimePrefabs;// 스테이지 선택 버튼들

    private ChapterClearData chapterClearData;
    public GameObject ClearFlagPrefab;
    public Sprite aliveSlimeSprite;
    public Sprite deadSlimeSprite;
    public Sprite unlockedChapterSprite;
    public Button ExitButtonPrefab;
    public GameObject ExitNoticePrefab;

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
        foreach (var SlimePrefab in SlimePrefabs)
        {
            SlimePrefab.SetActive(false);
        }
        //첫번째 챕터 정보이미지 활성화
        if (chapterInfoImages.Count > 0 || SlimePrefabs.Count > 0)
        {
            chapterInfoImages[0].SetActive(true);
            SlimePrefabs[0].SetActive(true);
        }
        // 챕터 버튼 클릭 이벤트 연결
        for (int i = 0; i < chapterButtons.Count; i++)
        {
            int chapterIndex = i; // 로컬 변수로 캡처
            Button buttonComponent = chapterButtons[i].GetComponent<Button>();
            Button buttonSelectComponent = chapterSelectionButtons[i].GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => OnChapterButtonClicked(chapterIndex));
                buttonSelectComponent.onClick.AddListener(() => OnChapterSelectionButtonClicked(chapterIndex));
            }
        }

        // 챕터 클리어 상태에 따른 스테이지 선택 버튼 활성화/비활성화
        UpdateChapterInfoImage();
        UpdateChaptertButtonImage();
        UpdateSlimeStateImage();
    }

    private void OnChapterButtonClicked(int chapterIndex)
    {
        // 모든 챕터 정보 이미지 비활성화
        foreach (var infoImage in chapterInfoImages)
        {
            infoImage.SetActive(false);
        }
        foreach (var SlimePrefab in SlimePrefabs)
        {
            SlimePrefab.SetActive(false);
        }

        // 선택한 챕터의 정보 이미지 활성화
        chapterInfoImages[chapterIndex].SetActive(true);
        SlimePrefabs[chapterIndex].SetActive(true);
    }

    private void OnChapterSelectionButtonClicked(int chapterIndex)
    {
        chapterClearData.selectedChapter = chapterIndex;
        SaveChapterClearData();

        SceneManager.LoadScene(2);
    }

    public void OnclickExitButton()
    {
        ExitNoticePrefab.SetActive(true);
    }

    public void OnClickExitNoticeYesButton()
    {
        Application.Quit();
    }
    public void OnclickExitNoticeNoButton()
    {
        ExitNoticePrefab.SetActive(false);
    }

    private void UpdateChapterInfoImage()
    {
        // 챕터 클리어 상태에 따른 버튼 활성화/비활성화
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
                    clearFlagInstance.name = "ClearFlagPrefab"; // 인스턴스 이름 설정
                }
            }
            else
            {
                chapterSelectionButtons[i].interactable = false;
                chapterInfoImages[i].transform.GetChild(0).GetComponent<Image>().color = Color.gray;
            }
        }
    }

    private void UpdateChaptertButtonImage()
    {
        for (int i = 1; i < chapterButtons.Count; i++)
        {
            if (chapterClearData.chapterCleared[i - 1])
            {
                chapterButtons[i].GetComponent<Image>().color = Color.red;
            }
            else chapterButtons[i].GetComponent<Image>().color = Color.gray;
        }
    }

    private void UpdateSlimeStateImage()
    {
        for (int i = 1; i < chapterButtons.Count; i++)
        {
            if (chapterClearData.chapterCleared[i - 1])
            {
                SlimePrefabs[i].GetComponent<Image>().sprite = aliveSlimeSprite;
            }
            else
            {
                SlimePrefabs[i].GetComponent<Image>().sprite = deadSlimeSprite;
            }
        }
    }

    public void ClearChapter(int chapterIndex)
    {
        // 챕터 클리어 상태 업데이트
        chapterClearData.chapterCleared[chapterIndex] = true;
        SaveChapterClearData();

        // 챕터 선택 버튼 업데이트
        UpdateChapterInfoImage();
        UpdateChaptertButtonImage();
        UpdateSlimeStateImage();
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

