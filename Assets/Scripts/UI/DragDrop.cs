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
    private UnityAction<int,int> actionOnDragWithParams;
    private UnityAction actionCheckConnection;
    private int lastX = -1;
    private int lastY = -1;
    
    #endregion private variables

    #region public functions

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log($"[OnDrag] call, GO = {eventData.pointerCurrentRaycast.gameObject.name}" );
        if (eventData.pointerCurrentRaycast.gameObject.GetComponent<MatchThreeFlexibleElement>())
        {
            int x = eventData.pointerCurrentRaycast.gameObject.GetComponent<MatchThreeFlexibleElement>().X;
            int y = eventData.pointerCurrentRaycast.gameObject.GetComponent<MatchThreeFlexibleElement>().Y;
            //actionOnEndDrag?.Invoke(x,y); //second point
            //actionOnDragWithParams(x,y); // first point
            if (x != lastX || y != lastY)
            {
                actionOnEndDrag?.Invoke(x,y);
                actionCheckConnection?.Invoke();
                lastX = x;
                lastY = y;
                Debug.Log($"[OnDrag] second point X = {lastX} | Y = {lastY}");
                Debug.Log($"[OnDrag] CheckConnection Invoked");
            }
            Debug.Log($"[OnDrag] X ={x} | Y = {y}" );
        }
        //OnDragFunction(eventData);
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.GetComponent<MatchThreeFlexibleElement>())
        {
            int x = eventData.pointerCurrentRaycast.gameObject.GetComponent<MatchThreeFlexibleElement>().X;
            int y = eventData.pointerCurrentRaycast.gameObject.GetComponent<MatchThreeFlexibleElement>().Y;
            actionOnBeginDrag?.Invoke();
            if (lastX == -1 && lastY == -1)
            {
                actionOnDragWithParams(x,y);
                lastX = x;
                lastY = y;
                Debug.Log($"[OnBeginDrag] complete");
            }
            Debug.Log($"[OnBeginDrag] first point X = {lastX} | Y = {lastY}");
        }
        else
        {
            Debug.Log("wrong gameobject");
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //rectTransform.anchoredPosition = Vector2.zero;
        if (eventData.pointerCurrentRaycast.gameObject.GetComponent<MatchThreeFlexibleElement>())
        {
            int x = eventData.pointerCurrentRaycast.gameObject.GetComponent<MatchThreeFlexibleElement>().X;
            int y = eventData.pointerCurrentRaycast.gameObject.GetComponent<MatchThreeFlexibleElement>().Y;
            actionOnEndDrag?.Invoke(x,y);
            if (x != lastX || y != lastY)
            {
                actionOnEndDragWithoutParams?.Invoke();
                actionCheckConnection?.Invoke();
                lastX = x;
                lastY = y;
            }
        }
        Debug.Log($"[OnEndDrag] lastX ={lastX} | lastY = {lastY}" );
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
    
    public void SetActionOnDragWithParams(params UnityAction<int, int>[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actionOnDragWithParams += actions[i];
        }
    }

    public void SetActionCheckConnection(params UnityAction[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actionCheckConnection += actions[i];
        }
    }

    #endregion public functions

    #region private functions

    #endregion private functions
}
