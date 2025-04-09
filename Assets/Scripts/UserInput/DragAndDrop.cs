using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = new Vector2(eventData.delta.x / canvas.scaleFactor, eventData.delta.y / canvas.scaleFactor);
        rectTransform.anchoredPosition += delta;
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        GameObject tempObject = GameObject.Find("Canvas");
        if (tempObject != null)
        {
            canvas = tempObject.GetComponent<Canvas>();
        }
    }
}
