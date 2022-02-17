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

    private IEnumerator SetObjectsFromPoolToMatchThreePanel()
    {
        yield return new WaitForEndOfFrame();
        var tempCount = controller.ColumnCount * controller.LineCount;
        for (int i = 0; i < tempCount; i++)
        {
            controller.SetObjectToPanel(objectPool.GetObjectByType(ObjectType.MatchThreeSprite, ElementType.NoElement));
        }
        controller.HidePanel();
    }

    #endregion private functions
}
