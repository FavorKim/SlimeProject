using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class SelectChapterManager : MonoBehaviour
{
    public List<GameObject> chapterButtons; // é�� ��ư��
    public List<GameObject> chapterInfoImages; // é�� ���� �̹�����
    public List<Button> chapterSelectionButtons;
    public List<GameObject> SlimePrefabs;// �������� ���� ��ư��

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

        // ��� é�� ���� �̹����� �������� ���� ��ư ��Ȱ��ȭ
        foreach (var infoImage in chapterInfoImages)
        {
            infoImage.SetActive(false); 
        }
        foreach (var SlimePrefab in SlimePrefabs)
        {
            SlimePrefab.SetActive(false);
        }
        //ù��° é�� �����̹��� Ȱ��ȭ
        if (chapterInfoImages.Count > 0 || SlimePrefabs.Count > 0)
        {
            chapterInfoImages[0].SetActive(true);
            SlimePrefabs[0].SetActive(true);
        }
        // é�� ��ư Ŭ�� �̺�Ʈ ����
        for (int i = 0; i < chapterButtons.Count; i++)
        {
            int chapterIndex = i; // ���� ������ ĸó
            Button buttonComponent = chapterButtons[i].GetComponent<Button>();
            Button buttonSelectComponent = chapterSelectionButtons[i].GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => OnChapterButtonClicked(chapterIndex));
                buttonSelectComponent.onClick.AddListener(() => OnChapterSelectionButtonClicked(chapterIndex));
            }
        }

        // é�� Ŭ���� ���¿� ���� �������� ���� ��ư Ȱ��ȭ/��Ȱ��ȭ
        UpdateChapterInfoImage();
        UpdateChaptertButtonImage();
        UpdateSlimeStateImage();
    }

    private void OnChapterButtonClicked(int chapterIndex)
    {
        // ��� é�� ���� �̹��� ��Ȱ��ȭ
        foreach (var infoImage in chapterInfoImages)
        {
            infoImage.SetActive(false);
        }
        foreach (var SlimePrefab in SlimePrefabs)
        {
            SlimePrefab.SetActive(false);
        }

        // ������ é���� ���� �̹��� Ȱ��ȭ
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
        // é�� Ŭ���� ���� ������Ʈ
        chapterClearData.chapterCleared[chapterIndex] = true;
        SaveChapterClearData();

        // é�� ���� ��ư ������Ʈ
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

