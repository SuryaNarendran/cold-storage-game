using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trolley : MonoBehaviour, IClickable, IDraggable
{

    [SerializeField] bool isHorizontal = false;
    [SerializeField] Vector2 baseDimensions;
    [SerializeField] Rigidbody rb;
    [SerializeField] float speedConstant;
    [SerializeField] float maxSpeed;
    [SerializeField] float maxForce;
    [SerializeField] float minForce;
    [SerializeField] UnityEvent onDrag;

    Vector3 previousFloorPoint;
    bool isDragging;

    Vector3 mouseToTrolleyOffset = new Vector3(-0.8f, 0, -0.9f);

    //public System.Action<Vector3> onMoved;

    private void Awake()
    {
        if (isHorizontal)
            rb.constraints = rb.constraints | RigidbodyConstraints.FreezePositionZ;
        else
            rb.constraints = rb.constraints | RigidbodyConstraints.FreezePositionX;
    }

    public void OnClick(Vector3 mousehitPosition)
    {
        //if (!isDragging) isDragging = true;
    }

    public void OnDrag(Vector3 floorMousePosition)
    {

        if (!isDragging)
        {
            isDragging = true;
            previousFloorPoint = floorMousePosition;
            return;
        }

        if (rb.velocity.magnitude > maxSpeed)
        {
            return;
        }
        Vector3 deltaFloorMousePosition = (floorMousePosition - previousFloorPoint);

        Vector3 newDeltaPosition;
        if (isHorizontal) newDeltaPosition = new Vector3(deltaFloorMousePosition.x, 0, 0);
        else newDeltaPosition = new Vector3(0, 0, deltaFloorMousePosition.z);

        Vector3 force = newDeltaPosition * speedConstant;
        if (force.magnitude > maxForce)
        {
            force.Normalize();
            force.Scale(Vector3.one * maxForce);
        }
        if (force.magnitude < minForce)
        {
            force.Normalize();
            force.Scale(Vector3.one * minForce);
        }

        //if((force.normalized + rb.velocity.normalized).magnitude < 1f)
        //{
        //    force *= rb.velocity.magnitude * 0.05f;
        //}

        rb.AddForce(force, ForceMode.Acceleration);
        onDrag?.Invoke();

        previousFloorPoint = floorMousePosition;
    }

    //alternate method of dragging - needs work
    //public void OnDrag(Vector3 floorMousePosition)
    //{
    //    if (!isDragging) return;

    //    Vector3 deltaFloorMousePosition = (floorMousePosition - transform.position);

    //    Vector3 newDeltaPosition;
    //    if (isHorizontal) newDeltaPosition = new Vector3(deltaFloorMousePosition.x, 0, 0);
    //    else newDeltaPosition = new Vector3(0, 0, deltaFloorMousePosition.z);

    //    if(newDeltaPosition.sqrMagnitude > 4)
    //    {
    //        isDragging = false;
    //        return;
    //    }

    //    Vector3 force = newDeltaPosition * speedConstant;
    //    if (force.magnitude > maxForce)
    //    {
    //        force.Normalize();
    //        force.Scale(Vector3.one * maxForce);
    //    }
    //    if (force.magnitude < minForce)
    //    {
    //        force.Normalize();
    //        force.Scale(Vector3.one * minForce);
    //    }

    //    if ( rb.velocity.sqrMagnitude > 0.5f && (rb.velocity.normalized + force.normalized).sqrMagnitude < 0.9f)
    //    {
    //        force = Vector3.zero;
    //        if (isHorizontal) transform.position += new Vector3(deltaFloorMousePosition.x, 0, 0);
    //        else transform.position += new Vector3(0, 0, deltaFloorMousePosition.z);
    //    }


    //    rb.AddForce(force, ForceMode.Acceleration);
    //    onDrag?.Invoke();

    //}

    public void OnRelease()
    {
        isDragging = false;
    }

}
