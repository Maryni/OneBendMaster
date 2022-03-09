using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MatchThreeController : MonoBehaviour
{
   #region Inspector variables

   [SerializeField] private float valueLocalScaleConnectedElement;
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
   [SerializeField] private List<MatchThreeFlexibleElement> arrayObjectsConnected;
   private int xFirst = -1, xSecond = -1;
   private int yFirst = -1, ySecond = -1;

   #endregion private variables

   #region properties

   public int ColumnCount => columnCount;
   public int LineCount => lineCount;
   public ElementType ElementTypeLastConnections => elementTypeLastConnections;
   //public int CountConnectedCellsLastConnection => countConnectedCells + 1;

   #endregion properties

   #region Unity functions

   private void Awake()
   {
      arrayObjectsInCell = new MatchThreeFlexibleElement[columnCount, lineCount];
      arrayObjectsConnected = new List<MatchThreeFlexibleElement>();
   }

   #endregion Unity functions

   #region public functions

   public string GetCountConnectedCellsLastConnection()
   {
      int count = 0;
      switch (countConnectedCells + 1)
      {
         case 2: count = 1; break;
         case 3: count = 3; break;
         case 4: count = 5; break;
         case 5: count = 7; break;
         case 6: count = 9; break;
         default: count = countConnectedCells; break;
      }
      return count.ToString();
   }

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
      //Debug.Log($"xFirst = {xFirst} | yFirst = {yFirst} | xSecond = {xSecond} | ySecond = {ySecond}");
      if (arrayObjectsInCell[xSecond, ySecond].ElementType == arrayObjectsInCell[xFirst, yFirst].ElementType)
      {
         if (CheckConnectionBetweenPoints(xSecond, ySecond, xFirst, yFirst))
         {
            SetElementToLastConnectionList(xSecond, ySecond);
            SetLastConnectedElementScale(valueLocalScaleConnectedElement);
            SetElementToLastConnectionList(xFirst, yFirst);
            SetLastConnectedElementScale(valueLocalScaleConnectedElement);

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
   
   /// <summary>
   /// Better to write more optimize function
   /// </summary>
   public void MoveCellsDown()
   {
      for (int i = 0; i < arrayObjectsConnected.Count; i++)
      {
            int count = 0;
            
            do
            {
               arrayObjectsConnected[i].gameObject.transform.localScale = Vector3.one;
               var currentX = arrayObjectsConnected[i].X + (count--);
               var nextX = arrayObjectsConnected[i].X + count;
               
               Debug.Log($"[AAA] i = {i} | currentX = {currentX} | nextX = {nextX}");
               if (nextX >= 0 && nextX < lineCount)
               {
                  var tempCurrentElement = arrayObjectsInCell[currentX, arrayObjectsConnected[i].Y];
                  var tempNextElement = arrayObjectsInCell[nextX, arrayObjectsConnected[i].Y];
                  
                  if (tempNextElement.X > 0)
                  {
                     tempNextElement.SetX(tempNextElement.X - 1);
                     continue;
                  }

                  if (tempNextElement.X == 0 && arrayObjectsConnected[i].Y + 1 < lineCount -1)
                  {
                     arrayObjectsInCell[currentX, arrayObjectsConnected[i].Y].SetElementType(arrayObjectsInCell[currentX, arrayObjectsConnected[i].Y + 1].ElementType);
                     arrayObjectsInCell[currentX, arrayObjectsConnected[i].Y].SetSprite(arrayObjectsInCell[currentX, arrayObjectsConnected[i].Y + 1].Sprite);
                     continue;
                  }
                  else if (tempNextElement.X == 0 && arrayObjectsConnected[i].Y + 1 > lineCount - 1)
                  {
                     arrayObjectsInCell[currentX, arrayObjectsConnected[i].Y].SetElementType(arrayObjectsInCell[currentX, arrayObjectsConnected[i].Y - 1].ElementType);
                     arrayObjectsInCell[currentX, arrayObjectsConnected[i].Y].SetSprite(arrayObjectsInCell[currentX, arrayObjectsConnected[i].Y - 1].Sprite);
                     continue;
                  }
                  arrayObjectsInCell[tempCurrentElement.X, tempCurrentElement.Y].SetElementType(arrayObjectsInCell[tempNextElement.X, tempNextElement.Y].ElementType);
                  arrayObjectsInCell[tempCurrentElement.X, tempCurrentElement.Y].SetSprite(arrayObjectsInCell[tempNextElement.X, tempNextElement.Y].Sprite);
               }
               
               //better to remove and set new info in cell
               if (nextX < 0 && (arrayObjectsConnected[i].Y >=0  && arrayObjectsConnected[i].Y < lineCount - 2))
               {
                  arrayObjectsInCell[currentX, arrayObjectsConnected[i].Y].SetElementType(arrayObjectsInCell[currentX, arrayObjectsConnected[i].Y + 1].ElementType);
                  arrayObjectsInCell[currentX, arrayObjectsConnected[i].Y].SetSprite(arrayObjectsInCell[currentX, arrayObjectsConnected[i].Y + 1].Sprite);
               }
               
            } while (arrayObjectsConnected[i].X + count - 1 > 0);

      }
      arrayObjectsConnected.Clear();
      elementTypeLastConnections = ElementType.NoElement;
   }

   #endregion public functions

   #region private functions

   private void SetLastConnectedElementScale(float value)
   {
      arrayObjectsConnected[arrayObjectsConnected.Count - 1].gameObject.transform.localScale =
         new Vector3(value, value, value);
   }
   
   private void SetElementToLastConnectionList(int xValue, int yValue)
   {
      var tempObject = arrayObjectsInCell[xValue, yValue];
      if (!arrayObjectsConnected.Contains(tempObject))
      {
         arrayObjectsConnected.Add(tempObject);
         Debug.Log($"[LastElementAdded] arrayObjectsConnected[{arrayObjectsConnected.Count - 1}].X = {arrayObjectsConnected[arrayObjectsConnected.Count - 1].X} | arrayObjectsConnected[0].Y = {arrayObjectsConnected[arrayObjectsConnected.Count - 1].Y} ");
      }
      else
      {
         Debug.Log($"[LastElementAdded] arrayObjectsConnected have object");
      }
   }

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
