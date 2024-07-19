using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainTitleManager : MonoBehaviour
{
    public TextMeshProUGUI Text_ClickToStart;

    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(1);
        }

        BlinkText();
    }

    private void BlinkText()
    {
       float alphaValue = Mathf.Lerp(1f,0.3f,Mathf.PingPong(Time.time,1));
        Color color = Text_ClickToStart.color;
        color.a = alphaValue;

        Text_ClickToStart.color  = color;
    }
}
