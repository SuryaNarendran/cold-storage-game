using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoPageUI : PopUpValue
{
    [SerializeField] public TMP_Text targetTempText;
    [SerializeField] public TMP_Text sourceTempText;
    [SerializeField] public TMP_Text statusText;
    [SerializeField] public TMP_Text statusLabel;
    [SerializeField] public Button openCloseButton;
    [SerializeField] public TMP_Text openCloseButtonText;
    [SerializeField] public Image arrowImage;
    [SerializeField] public Image lockedImage;
    [SerializeField] public Sprite unlockedImageSprite;
    [SerializeField] public Sprite lockedImageSprite;
    [SerializeField] public Color lockedColor;
    [SerializeField] public Color unlockedColor;

    public void SetTargetTemp(float value)
    {
        targetTempText.text = value.ToString("0") + "°";
        targetTempText.color = TemperatureMethods.TemperatureColor(value*3, true);
    }

    public void SetSourceTemp(float value)
    {
        sourceTempText.text = value.ToString("0") + "°";
        sourceTempText.color = TemperatureMethods.TemperatureColor(value * 3, true);
    }

    public void SetOpenCloseButtonActive(bool state)
    {
        openCloseButton.gameObject.SetActive(state);
    }

    public void SetOpenCloseButtonText(string value)
    {
        openCloseButtonText.text = value;
    }

    public void SetStatusText(string value, Color color)
    {
        statusText.text = value;
        statusText.color = color;
    }
    public void SetStatusLabelActive(bool state)
    {
        statusLabel.gameObject.SetActive(state);
        statusText.gameObject.SetActive(state);
    }

    public void SetArrowDirection (bool up)
    {
        float angle = 0;
        if (up) angle = 180f;

        if(arrowImage != null)
            arrowImage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void Locked(bool state)
    {
        if (state)
        {
            lockedImage.sprite = lockedImageSprite;
            lockedImage.color = lockedColor;
        }
        else
        {
            lockedImage.sprite = unlockedImageSprite;
            lockedImage.color = unlockedColor;
        }
    }

    public void OpenClose()
    {
        UI.selectedObject.gameObject.SendMessage("OpenClose");
    }
}
