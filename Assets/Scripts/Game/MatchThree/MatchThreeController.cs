using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MatchThreeController : MonoBehaviour
{
   #region Inspector variables
   
   [SerializeField] private GameObject gamePanelGameObject;
   [SerializeField] private GameObject panelForHideObjects;

   [Header("Sprites in cells"),SerializeField] private Sprite spriteFire;
   [SerializeField] private Sprite spriteWater;
   [SerializeField] private Sprite spriteEnergy;
   [SerializeField] private Sprite spriteNature;
   [SerializeField] private Sprite spriteMagic;
   [SerializeField] private Sprite spriteClear;

   [SerializeField] private int countConnectedCells;
   [SerializeField] private ElementType elementTypeLastConnections = ElementType.NoElement;
   #endregion Inspector variables

   #region private variables

   private int columnCount = 6;
   private int lineCount = 6;
   private MatchThreeFlexibleElement[,] arrayObjectsInCell;
   private int xFirst = -1, xSecond = -1;
   private int yFirst = -1, ySecond = -1;

   #endregion private variables

   #region properties

   public int ColumnCount => columnCount;
   public int LineCount => lineCount;
   public ElementType ElementTypeLastConnections => elementTypeLastConnections;
   public int CountConnectedCellsLastConnection => countConnectedCells + 1;

   #endregion properties

   #region Unity functions

   private void Awake()
   {
      arrayObjectsInCell = new MatchThreeFlexibleElement[columnCount, lineCount];
   }

   #endregion Unity functions

   #region public functions

   public void SetObjectToPanel(params GameObject[] gameObjects)
   {
      for (int i = 0; i < gameObjects.Length; i++)
      {
         gameObjects[i].transform.SetParent(gamePanelGameObject.transform);
      }
      CheckIndexesAndSetRandomElement();
   }

   public void HidePanel()
   {
      this.gameObject.SetActive(false);
   }

   /// <summary>
   /// point what we will check with first point (it's second point)
   /// </summary>
   /// <param name="x"></param>
   /// <param name="y"></param>
   public void SetFirstsXY(int x, int y)
   {
      xFirst = x;
      yFirst = y;
   }

   /// <summary>
   /// point from we checking
   /// </summary>
   /// <param name="x"></param>
   /// <param name="y"></param>
   public void SetValuesFromBeginDragPoint(int x, int y)
   {
      //x,ySecond = point OnDrag
      xSecond = x;
      ySecond = y;
   }

   /// <summary>
   /// From beginDrag to EndDrag (from xSecond to xFirst, and then xFirst to xSecond)
   /// </summary>
   public void ChangeElementsInArray()
   {
      ElementType tempType = arrayObjectsInCell[xSecond, ySecond].ElementType;
      Sprite tempSprite = arrayObjectsInCell[xSecond,ySecond].Sprite;
      arrayObjectsInCell[xSecond,ySecond].SetElementType(arrayObjectsInCell[xFirst,yFirst].ElementType);
      arrayObjectsInCell[xFirst,yFirst].SetElementType(tempType);
      arrayObjectsInCell[xSecond,ySecond].SetSprite(arrayObjectsInCell[xFirst,yFirst].Sprite);
      arrayObjectsInCell[xFirst,yFirst].SetSprite(tempSprite);
   }
   
   public void CheckSlideConnectionBetweenOnBeginDragAndOnEndDrag()
   {
      Debug.Log($"xFirst = {xFirst} | yFirst = {yFirst} | xSecond = {xSecond} | ySecond = {ySecond}");
      if (arrayObjectsInCell[xSecond, ySecond].ElementType == arrayObjectsInCell[xFirst, yFirst].ElementType)
      {
         if (CheckConnectionBetweenPoints(xSecond, ySecond, xFirst, yFirst))
         {
            countConnectedCells++;
            xSecond = xFirst;
            ySecond = yFirst;
            if (elementTypeLastConnections != arrayObjectsInCell[xFirst, yFirst].ElementType)
            {
               elementTypeLastConnections = arrayObjectsInCell[xFirst, yFirst].ElementType;
            }
            Debug.Log($"connection YES | countConnectedCells = [{countConnectedCells}] ");
         }
         else
         {
            Debug.Log($"connection NO | countConnectedCells = [{countConnectedCells}] ");
         }
      }
      else
      {
         Debug.Log("ElementType not the same");
      }
   }

   public void ClearCountConnected()
   {
      countConnectedCells = 0;
   }

   #endregion public functions

   #region private functions

   
   
   private bool CheckConnectionBetweenPoints(int x1, int y1, int x2, int y2)
   {
      if (x1 + 1 == x2)
      {
         if (y1 + 1 == y2)
         {
            return true;
         }
         else if (y1 - 1 == y2)
         {
            return true;
         }
         else if (y1 == y2)
         {
            return true;
         }
         else
         {
            return false;
         }
      }

      if (x1 == x2)
      {
         if (y1 + 1 == y2)
         {
            return true;
         }
         else if (y1 - 1 == y2)
         {
            return true;
         }
         else
         {
            return false;
         }
      }
      
      if (x1 - 1 == x2)
      {
         if (y1 + 1 == y2)
         {
            return true;
         }
         else if (y1 - 1 == y2)
         {
            return true;
         }
         else if (y1 == y2)
         {
            return true;
         }
         else
         {
            return false;
         }
      }
      return false;
   }
   
   private void CheckPanelAndAddAllToList()
   {
      MatchThreeFlexibleElement[,] tempObjects = new MatchThreeFlexibleElement[lineCount,columnCount];
      for (int i = 0; i < lineCount; i++)
      {
         for (int j = 0; j < columnCount; j++)
         {
            tempObjects[i,j] = gamePanelGameObject.transform.GetChild((lineCount*i) + j).GetChild(0).GetComponent<MatchThreeFlexibleElement>();  
         }
      }

      for (int i = 0; i < lineCount; i++)
      {
         for (int j = 0; j < columnCount; j++)
         {
            arrayObjectsInCell[i, j] = tempObjects[i,j];
            arrayObjectsInCell[i, j].SetX(i);
            arrayObjectsInCell[i, j].SetY(j);
         }
      }
   }

   private void SetRandomElementsToCells(bool needToChangeAllCells = true)
   {
      for (int i = 0; i < lineCount; i++)
      {
         for (int j = 0; j < columnCount; j++)
         {
            var tempRandomElementType = GetRandomElementType();
            if (needToChangeAllCells)
            {
               arrayObjectsInCell[i,j].SetElementType(tempRandomElementType);
            }
            else
            {
               if (arrayObjectsInCell[i, j].ElementType == ElementType.NoElement)
               {
                  tempRandomElementType = arrayObjectsInCell[i, j].ElementType;
                  arrayObjectsInCell[i,j].SetElementType(tempRandomElementType);
               }
            }
            switch (tempRandomElementType)
            {
               case ElementType.Fire: arrayObjectsInCell[i,j].SetSprite(spriteFire); break;
               case ElementType.Water: arrayObjectsInCell[i,j].SetSprite(spriteWater); break;
               case ElementType.Nature: arrayObjectsInCell[i,j].SetSprite(spriteNature); break;
               case ElementType.Energy: arrayObjectsInCell[i,j].SetSprite(spriteEnergy); break;
               case ElementType.Magic: arrayObjectsInCell[i,j].SetSprite(spriteMagic); break;
               default: arrayObjectsInCell[i,j].SetSprite(spriteClear); break;
            }
         }
      }
   }

   private void CheckIndexesAndSetRandomElement()
   {
      CheckPanelAndAddAllToList();
      SetRandomElementsToCells();
   }

   private ElementType GetRandomElementType()
   {
      return (ElementType) Random.Range(1, System.Enum.GetValues(typeof(ElementType)).Length);
   }
   
   #endregion private functions

}
