using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;
    
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

        // rightButton ���� �� ����
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

        // Image ������Ʈ�� �ִ��� Ȯ��
        if (buttonImage != null)
        {
            // Image�� ������ ������
            Color color = buttonImage.color;

            // alpha ���� ���� (0 ~ 1 ������ ��ȯ)
            color.a = alpha / 255f;

            // ����� ������ �ٽ� Image�� ����
            buttonImage.color = color;
        }
    }
}
