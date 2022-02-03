using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour, IHoverable
{
    [SerializeField] float linewidth;
    [SerializeField] Color color;

    ISelectable selectableComponent;
    MeshRenderer[] meshRenderers;

    bool currentlyHighlighted = false;
    bool hovering = false;
    bool selected = false;


    private void Awake()
    {
        selectableComponent = GetComponent<ISelectable>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        if (selectableComponent != null)
        {
            selectableComponent.onSelection += OnSelected;
            selectableComponent.onDeSelection += OnDeSelected;
        }
    }

    public void OnHover(Vector3 mousePos)
    {
        hovering = true;
        if (currentlyHighlighted == false)
        {
            Set(true);
        }
    }

    public void OnHoverLost()
    {
        hovering = false;
        if (!selected && currentlyHighlighted)
        {
            Set(false);
        }
    }

    public void OnSelected()
    {
        selected = true;
        if(currentlyHighlighted == false)
        {
            Set(true);
        }
    }

    public void OnDeSelected()
    {
        selected = false;
        if(!hovering && currentlyHighlighted)
        {
            Set(false);
        }
    }

    private void Set(bool state)
    {
        if (state)
        {
            currentlyHighlighted = true;
            foreach (var meshRenderer in meshRenderers)
            {
                Material mat = meshRenderer.material;
                if(mat.HasProperty("_FirstOutlineWidth") && mat.HasProperty("_FirstOutlineColor"))
                {
                    mat.SetFloat("_FirstOutlineWidth", linewidth);
                    mat.SetColor("_FirstOutlineColor", color);
                }
            }
        }
        else
        {
            currentlyHighlighted = false;
            foreach (var meshRenderer in meshRenderers)
            {
                Material mat = meshRenderer.material;
                if (mat.HasProperty("_FirstOutlineWidth"))
                {
                    mat.SetFloat("_FirstOutlineWidth", 0);
                }
            }
        }

    }
}
