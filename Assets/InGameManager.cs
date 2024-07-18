using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class InGameManager : MonoBehaviour
{
    public GameObject RestartPopup;
    public GameObject SelectStagePopup;
    public TextMeshProUGUI LifeText;
    public TextMeshProUGUI TimeText;
    private float timeRemaining = 600f;


    private void Start()
    {
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

        // 시간이 다 되면 수행할 작업을 여기에 추가
    }

    private void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        TimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
