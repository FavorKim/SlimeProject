using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SelectChapterManager : MonoBehaviour
{
    public static SelectChapterManager Instance { get; private set; }
    public List<GameObject> chapterButtons;
    public List<GameObject> chapterInfoImages;
    public List<Button> chapterSelectionButtons;
    public List<GameObject> SlimePrefabs;

    private ChapterClearData chapterClearData;
    public GameObject ClearFlagPrefab;
    public Sprite aliveSlimeSprite;
    public Sprite deadSlimeSprite;
    public Sprite unlockedChapterSprite;
    public Button ExitButtonPrefab;
    public GameObject ExitNoticePrefab;
    public int SelectChapter;

    private string saveFilePath;

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
        saveFilePath = Path.Combine(Application.persistentDataPath, "chapterClearData.json");
        LoadChapterClearData();

        SelectChapter = SelectChapterManager.Instance.SelectChapter;
        foreach (var infoImage in chapterInfoImages)
        {
            infoImage.SetActive(false);
        }
        foreach (var SlimePrefab in SlimePrefabs)
        {
            SlimePrefab.SetActive(false);
        }

        if (chapterInfoImages.Count > 0 && SlimePrefabs.Count > 0)
        {
            chapterInfoImages[SelectChapter].SetActive(true);
            SlimePrefabs[SelectChapter].SetActive(true);
        }

        for (int i = 0; i < chapterButtons.Count; i++)
        {
            int chapterIndex = i;
            Button buttonComponent = chapterButtons[i].GetComponent<Button>();
            Button buttonSelectComponent = chapterSelectionButtons[i].GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => OnChapterButtonClicked(chapterIndex));
                buttonSelectComponent.onClick.AddListener(() => OnChapterSelectionButtonClicked(chapterIndex));
            }
        }

        UpdateChapterInfoImage();
        UpdateChaptertButtonImage();
        UpdateSlimeStateImage();
    }

    private void OnChapterButtonClicked(int chapterIndex)
    {
        foreach (var infoImage in chapterInfoImages)
        {
            infoImage.SetActive(false);
        }
        foreach (var SlimePrefab in SlimePrefabs)
        {
            SlimePrefab.SetActive(false);
        }

        chapterInfoImages[chapterIndex].SetActive(true);
        SlimePrefabs[chapterIndex].SetActive(true);
        SelectChapter = chapterIndex;
        SaveChapterClearData();
    }

    private void OnChapterSelectionButtonClicked(int chapterIndex)
    {
        SelectChapter = chapterIndex;
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
        for (int i = 1; i < chapterSelectionButtons.Count; i++)
        {
            if (chapterClearData.chapterCleared[i - 1])
            {
                chapterSelectionButtons[i].interactable = true;
                Transform imageTransform = chapterInfoImages[i].transform.GetChild(0);
                Image imageComponent = imageTransform.GetComponent<Image>();
                imageComponent.sprite = unlockedChapterSprite;
                if (imageTransform.Find("ClearFlagPrefab") == null)
                {
                    RectTransform clearFlagPos = imageTransform.GetComponent<RectTransform>();
                    GameObject clearFlagInstance = Instantiate(ClearFlagPrefab, clearFlagPos);
                    clearFlagInstance.name = "ClearFlagPrefab";
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
        chapterClearData.chapterCleared[chapterIndex] = true;
        SaveChapterClearData();

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

