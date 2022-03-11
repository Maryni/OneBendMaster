using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private GameController gameController;
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private MatchThreeController controller;
    [SerializeField] private BulletsController bulletsController;

    #endregion Inspector variables

    #region Unity functions

    private void Start()
    {
        SceneManager.sceneLoaded += SetActionWhenSceneLoaded;
    }

    #endregion Unity functions

    #region public functions

    public void SetGameController(GameController controller)
    {
        gameController = controller;
    }

    #endregion public function
    
    #region private functions

    private void SetActionWhenSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 1)
        { 
            StartCoroutine(SetObjectsFromPoolToMatchThreePanel());
        }
    }
    
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
                () => bulletsController.SetBulletTextForFirstNonElementBullet(controller.GetCountConnectedCellsLastConnection()),
                () => bulletsController.SetBulletColorForFirstBulletWithoutColor(controller.ElementTypeLastConnections),
                () => gameController.Player.SetCurrentBulletsForFirstBullet(bulletsController.GetBulletTextWhichLastUnzero()),
                () => controller.MoveCellsDown(),
                () => controller.ClearCountConnected());
            dragDrop.SetActionCheckConnection(()=> controller.CheckSlideConnectionBetweenOnBeginDragAndOnEndDrag());
        }
        controller.SetObjectToPanel(tempArray);
        controller.HidePanel();
    }

    #endregion private functions
}
