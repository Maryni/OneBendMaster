using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private MatchThreeController controller;

    #endregion Inspector variables

    #region Unity functions

    private void Start()
    {
        StartCoroutine(SetObjectsFromPoolToMatchThreePanel());
    }

    #endregion Unity functions

    #region private functions
    //поменять элемент с тем с кем меняемся, и картинку
    private IEnumerator SetObjectsFromPoolToMatchThreePanel()
    {
        yield return new WaitForEndOfFrame();
        var tempCount = controller.ColumnCount * controller.LineCount;
        GameObject[] tempArray = new GameObject[tempCount];
        for (int i = 0; i < tempCount; i++)
        {
            tempArray[i] = objectPool.GetObjectByType(ObjectType.MatchThreeSprite, ElementType.NoElement);
            tempArray[i].SetActive(true);
            var dragDrop = tempArray[i].GetComponentInChildren<DragDrop>();
            var image = dragDrop.gameObject.GetComponent<MatchThreeFlexibleElement>().Image;
            
            dragDrop.SetActionOnBeginDrag(() => image.raycastTarget = false);
            dragDrop.SetActionOnDragWithParams(controller.SetValuesFromBeginDragPoint);
            dragDrop.SetActionOnEndDrag(controller.SetFirstsXY);
            dragDrop.SetActionOnEndDragWithoutParams(
                () => image.raycastTarget = true,
                () => controller.ClearCountConnected());
            dragDrop.SetActionCheckConnection(()=> controller.CheckSlideConnectionBetweenOnBeginDragAndOnEndDrag());
        }
        controller.SetObjectToPanel(tempArray);
        controller.HidePanel();
    }

    #endregion private functions
}
