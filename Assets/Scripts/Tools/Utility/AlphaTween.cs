using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AlphaTween : MonoBehaviour
{
    private void Start()
    {
        Image image = GetComponent<Image>();
        image.DOColor(new Color(0, 0, 0, 0), 2f)
            .SetEase(Ease.InCubic)
            .OnComplete(() => gameObject.SetActive(false));
    }
}
