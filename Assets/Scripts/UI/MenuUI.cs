using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class MenuUI : MonoBehaviour
{
    [SerializeField] float riseY, dropY, transitionTime;
    [SerializeField] TMP_Text playButtonText;
    [SerializeField] public GameObject playButton;
    [SerializeField] public GameObject menuButton;
    [SerializeField] GameObject thanksText;

    bool isDropped = false;

    private void Start()
    {
        Timer.Register(2f, DropMenu);
    }

    public void DropMenu()
    {
        if (isDropped) return;
        isDropped = true;

        gameObject.SetActive(true);
        menuButton.SetActive(false);

        if (LevelController.playingGame)
            playButtonText.text = "Continue";
        else playButtonText.text = "Play";

        GetComponent<RectTransform>().DOAnchorPosY(dropY, transitionTime).SetEase(Ease.OutCubic);
    }

    public void RaiseMenu()
    {
        if (isDropped == false) return;
        isDropped = false;

        GetComponent<RectTransform>().DOAnchorPosY(riseY, transitionTime)
            .SetEase(Ease.InCubic)
            .OnComplete(SetRaisedMenu);
    }

    private void SetRaisedMenu()
    {
        menuButton.SetActive(true);
        gameObject.SetActive(false);
    }

    public void PlayButton()
    {
        if (LevelController.playingGame == false)
        {
            Timer.Register(2f, () => LevelController.Setup());
        }
        RaiseMenu();
    }

    public void EndOfGame()
    {
        playButton.SetActive(false);
        thanksText.SetActive(true);
    }
}
