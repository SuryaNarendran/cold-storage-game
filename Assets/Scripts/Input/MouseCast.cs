using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCast : Singleton<MouseCast>
{

    [SerializeField] float floorPlaneY;

    GameObject currentlyClicked;
    GameObject currentlyHovered;
    GameObject currentlyDragged;


    void Update()
    {
        IHoverable hoverableComponent = null;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                var clickableComponent = hit.collider.GetComponent<IClickable>();
                if (clickableComponent != null)
                {
                    currentlyClicked = clickableComponent.gameObject;
                }
                if (currentlyClicked != null)
                {
                    currentlyClicked.SendMessage("OnClick", hit.point);
                }

                var draggableComponent = hit.collider.GetComponent<IDraggable>();
                if (draggableComponent != null)
                {
                    currentlyDragged = draggableComponent.gameObject;
                }
                if (currentlyDragged != null)
                {
                    //change to ondrag start later
                    currentlyDragged.SendMessage("OnClick", hit.point);
                }
            }

            hoverableComponent = hit.collider.GetComponent<IHoverable>();

        }

        if(hoverableComponent != null)
        {
            if(currentlyHovered != hoverableComponent.gameObject)
            {
                if(currentlyHovered != null) currentlyHovered.SendMessage("OnHoverLost");
                currentlyHovered = hoverableComponent.gameObject;
                currentlyHovered.SendMessage("OnHover", hit.point);
            }
        }
        else if (currentlyHovered != null)
        {
            currentlyHovered.SendMessage("OnHoverLost");
            currentlyHovered = null;
        }

        //if (currentlyHovered == null)
        //{
        //    if (hoverableComponent != null)
        //    {
        //        currentlyHovered = hoverableComponent.gameObject;
        //        currentlyHovered.SendMessage("OnHover", hit.point);
        //    }
        //}
        //else
        //{
        //    if (hoverableComponent.gameObject != currentlyHovered)
        //    {
        //        currentlyHovered.SendMessage("OnHoverLost");
        //        currentlyHovered = hoverableComponent.gameObject;

        //        if (currentlyHovered != null)
        //            currentlyHovered.SendMessage("OnHover", hit.point);
        //    }
        //}

        if (currentlyHovered == null)
        {
            UI.CursorIndicator(false);
        }
        else
        {
            UI.CursorIndicator(true);
            UI.SetIndicatorPosition(hit.point);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (currentlyDragged != null)
                currentlyDragged.SendMessage("OnRelease");
            currentlyDragged = null;
        }

        //else if (currentlyClicked is IDraggable && hit.collider != null)
        //{
        //    IDraggable draggable = hit.collider.GetComponent<IDraggable>();
        //    if (draggable == currentlyClicked)
        //    {
        //        Plane plane = new Plane(Vector3.up, Vector3.up * floorPlaneY);
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        float distance;
        //        if (plane.Raycast(ray, out distance))
        //        {
        //            Vector3 hitPoint = ray.GetPoint(distance);
        //            (currentlyClicked as IDraggable).OnDrag(hitPoint);
        //        }
        //    }
        //}

        else if (currentlyDragged != null)
        {
            Vector3 position;
            if(MousePositionOnFloor(out position))
            {
                currentlyDragged.SendMessage("OnDrag", position);
            }
        }
    }

    public static bool MousePositionOnFloor(out Vector3 position)
    {
        Plane plane = new Plane(Vector3.up, Vector3.up * Instance.floorPlaneY);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            position = hitPoint;
            return true;
        }

        else
        {
            position = Vector3.zero;
            return false;
        }
    }

    public static void ClearSelections()
    {
        Instance.currentlyClicked = null;
        Instance.currentlyDragged = null;
        Instance.currentlyHovered = null;
    }
}
