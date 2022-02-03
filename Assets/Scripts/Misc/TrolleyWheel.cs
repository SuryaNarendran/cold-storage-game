using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyWheel : MonoBehaviour
{
    [SerializeField] Rigidbody parentRb;
    [SerializeField] float circumference;

    private void Update()
    {
        float speed = parentRb.velocity.magnitude;

        float rotationFactor = (speed * Time.deltaTime) / circumference;

        transform.Rotate(Vector3.right, rotationFactor * 360);
    }
}
