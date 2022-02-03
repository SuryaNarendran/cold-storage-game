using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionIcon : MonoBehaviour
{
    [SerializeField] AnimationCurve verticalMovement;
    [SerializeField] float valueScale;
    [SerializeField] float timeScale;
    [SerializeField] SpriteRenderer spriteRenderer;

    float t = 0;
    float direction = 1;

    void Update()
    {
        t += direction * Time.deltaTime / timeScale;
        if (Mathf.Abs(t - 0.5f) > 0.48f) direction *= -1;

        spriteRenderer.transform.localPosition 
            = new Vector3(spriteRenderer.transform.localPosition.x, verticalMovement.Evaluate(t), spriteRenderer.transform.localPosition.z);

        spriteRenderer.transform.LookAt(Camera.main.transform.position, Vector3.up);
    }
}
