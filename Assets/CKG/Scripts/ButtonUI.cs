using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;
    SelectChapterManager selectChapterManager;

    private void Update()
    {
        if (UIManager.Instance.currentStageIndex > 0)
        {
            SetButtonAlpha(leftButton, 255);
        }
        else
        {
            SetButtonAlpha(leftButton, 100);
        }

        // rightButton 알파 값 설정
        if (UIManager.Instance.currentStageIndex < UIManager.Instance.stages.Length - 1)
        {
            SetButtonAlpha(rightButton, 255);
        }
        else
        {
            SetButtonAlpha(rightButton, 100);
        }
    }
    public void OnClick_SlideLeftStage()
    {
        if (UIManager.Instance.currentStageIndex > 0)
            UIManager.Instance.MoveStageLeft();
    }

    public void OnClick_SlideRightStage()
    {
        if (UIManager.Instance.currentStageIndex < UIManager.Instance.stages.Length - 1)
            UIManager.Instance.MoveStageRight();
    }

    private void SetButtonAlpha(Button button, float alpha)
    {
        Image buttonImage = button.GetComponent<Image>();

        // Image 컴포넌트가 있는지 확인
        if (buttonImage != null)
        {
            // Image의 색상을 가져옴
            Color color = buttonImage.color;

            // alpha 값을 설정 (0 ~ 1 범위로 변환)
            color.a = alpha / 255f;

            // 변경된 색상을 다시 Image에 설정
            buttonImage.color = color;
        }
    }
}
