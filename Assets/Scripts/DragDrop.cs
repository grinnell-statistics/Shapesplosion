using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//   Class: DragDrop
// Purpose: Checks if mouse drag and drop of mouse works
public class DragDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rTranform;

    private void Awake()
    {
        rTranform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Debug.Log("Found");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        rTranform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }
}
