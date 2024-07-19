using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class InGameManager : MonoBehaviour
{
    public GameObject RestartPopup;
    public GameObject SelectStagePopup;
    public TextMeshProUGUI LifeText;
    public TextMeshProUGUI TimeText;
    private float timeRemaining = 600f;
    private string chapterClearFilePath;
    private ChapterClearData chapterClearData;

    private void Start()
    {
        chapterClearFilePath = Path.Combine(Application.persistentDataPath, "chapterClearData.json");
        LoadChapterClearData();
        StartCoroutine(UpdateTimer());
    }

    public void OnRestartButtonClicked()
    {
        Time.timeScale = 0f;
        RestartPopup.SetActive(true);
    }

    public void OnSelectButtonClicked()
    {
        Time.timeScale = 0f;
        SelectStagePopup.SetActive(true);
    }

    public void OnRestarPopupExit()
    {
        Time.timeScale = 1f;
        RestartPopup.SetActive(false);
    }

    public void OnSelectStagePopupExit()
    {
        Time.timeScale = 1f;
        SelectStagePopup.SetActive(false);
    }

    public void OnRestarPopupEnter()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void OnSelectStagePopupEnter()
    {
        SceneManager.LoadScene(2);
    }

    private IEnumerator UpdateTimer()
    {
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText(timeRemaining);
            yield return null;
        }
    }

    private void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        TimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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

    private void SaveChapterClearData()
    {
        string json = JsonUtility.ToJson(chapterClearData);
        File.WriteAllText(chapterClearFilePath, json);
    }
}
