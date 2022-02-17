using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    #region private variables

    private UnityAction actionOnBeginDrag;
    private UnityAction<int,int> actionOnEndDrag;
    private UnityAction actionOnEndDragWithoutParams;
    private UnityAction<int,int> actionOnBeginDragWithParams;
    private RectTransform rectTransform;
    private Vector2 defaultVector2Position;
    
    #endregion private variables

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    #region public functions

    public void OnDrag(PointerEventData eventData)
    {
        OnDragFunction(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        int x = eventData.pointerCurrentRaycast.gameObject.GetComponent<MatchThreeFlexibleElement>().X;
        int y = eventData.pointerCurrentRaycast.gameObject.GetComponent<MatchThreeFlexibleElement>().Y;
        actionOnBeginDrag?.Invoke();
        actionOnBeginDragWithParams(x,y);
        Debug.Log($"[OnBeginDrag] X ={x} | Y = {y}" );
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = Vector2.zero;
        int x = eventData.pointerCurrentRaycast.gameObject.GetComponent<MatchThreeFlexibleElement>().X;
        int y = eventData.pointerCurrentRaycast.gameObject.GetComponent<MatchThreeFlexibleElement>().Y;
        actionOnEndDrag?.Invoke(x,y);
        actionOnEndDragWithoutParams?.Invoke();
        
        Debug.Log($"[OnEndDrag] X ={eventData.pointerCurrentRaycast.gameObject.GetComponent<MatchThreeFlexibleElement>().X} | Y = {eventData.pointerCurrentRaycast.gameObject.GetComponent<MatchThreeFlexibleElement>().Y}" );
    }

    public void SetActionOnBeginDrag(params UnityAction[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actionOnBeginDrag += actions[i];
        }
    }
    
    public void SetActionOnEndDrag(params UnityAction<int,int>[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actionOnEndDrag += actions[i];
        }
    }
    public void SetActionOnEndDragWithoutParams(params UnityAction[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actionOnEndDragWithoutParams += actions[i];
        }
    }
    
    public void SetActionOnBeginDragWithParams(params UnityAction<int, int>[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actionOnBeginDragWithParams += actions[i];
        }
    }
    
    public void OnDragFunction(PointerEventData eventData)
    {
        if (defaultVector2Position == Vector2.zero)
        {
            defaultVector2Position = rectTransform.position;
        }
        rectTransform.anchoredPosition += eventData.delta;
        //endTransform = rectTransform.anchoredPosition;
    }

    #endregion public functions
}
