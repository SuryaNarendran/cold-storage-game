using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class TextBox : MonoBehaviour
{
    [SerializeField] public TMP_Text mainContent;

    [SerializeField] [Range(1, 120)] protected float scrollTextSpeed;
    [SerializeField] [Range(0, 5)] protected float fullStopPause;
    [SerializeField] [Range(0, 10)] protected float slashPause;
    [SerializeField] [Range(1, 30)] protected float ellipsisSpeed;
    [SerializeField] [Range(0.2f, 3)] protected float scrollingVolumeFactor;
    [SerializeField] [Range(0.2f, 3)] protected float scrollingPitchFactor;
    [SerializeField] [Range(0f, 3f)] protected float initialdelay;
    [SerializeField] [Range(5, 100)] protected int maxCharactersPerLine;
    [SerializeField] public UnityEvent onLetterPrinted;

    public string textString { get; private set; }
    public bool isScrollingText { get; private set; }
    public bool scrollTextFast { get; private set; }

    private Coroutine textScrollingCoroutine;

    public virtual void DisplayText(string _textString, bool instant)
    {

        textString = _textString;

        if (instant)
        {
            ClearText();
            mainContent.text = textString;
            return;
        }

        else if (isScrollingText == false)
        {
            ClearText();
            isScrollingText = true;
            scrollTextFast = false;
            textScrollingCoroutine = StartCoroutine(ScrollText());
        }
    }

    public virtual void DisplayText(string _textString)
    {

        textString = _textString;

        if (isScrollingText == false)
        {
            ClearText();
            isScrollingText = true;
            scrollTextFast = false;
            textScrollingCoroutine = StartCoroutine(ScrollText());
        }
    }

    public virtual void SpeedUpScrolling()
    {
        scrollTextFast = true;
    }

    public virtual void ClearText()
    {
        if (isScrollingText)
        {
            if (textScrollingCoroutine != null) StopCoroutine(textScrollingCoroutine);
        }
        isScrollingText = false;
        mainContent.text = "";
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator ScrollText()
    {
        yield return new WaitForSeconds(initialdelay);

        float delay = 0f;
        mainContent.text = "";
        int charactersInLine = 0;
       
        for (int i = 0; i < textString.Length; i++)
        {
            bool skipLetter = false;
            if (scrollTextFast)
            {
                delay += 1f / (scrollTextSpeed * 2);
            }

            //parsing logic
            else if (textString[i] == '.')
            {
                //ellipsis check
                if (i > 0 && textString[i - 1] == '.' ||
                    i < textString.Length - 1 && textString[i + 1] == '.')
                {
                    delay += 1f / ellipsisSpeed;
                }
                else
                {
                    delay += fullStopPause;
                }
            }

            //parsing logic
            else if (textString[i] == '$')
            {
                delay += slashPause;
                skipLetter = true;
            }

            else delay += 1f / scrollTextSpeed;

            if (!skipLetter)
            {
                mainContent.text += textString[i];
                charactersInLine++;
                if (charactersInLine > maxCharactersPerLine && GetRemainingWordLength(textString, i) == 0)
                {
                    mainContent.text += System.Environment.NewLine;
                    charactersInLine = 0;
                }
            }

            if (delay >= Time.deltaTime)
            {
                float temp = delay;
                delay = 0f;

                //play sound
                onLetterPrinted?.Invoke();

                yield return new WaitForSeconds(temp);
            }
        }
        isScrollingText = false;
    }

    private int GetRemainingWordLength(string content, int index)
    {
        char letter = content[index];
        int count = 0;
        while(char.IsWhiteSpace(content[index+count]) == false)
        {
            count++;

            if (index + count >= content.Length) break;
        }
        return count;
    }

}
