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
   
   [Header("Count Matched Objects per direction"),SerializeField] private int countLeftObjectsMatched;
   [SerializeField] private int countRightObjectsMatched;
   [SerializeField] private int countUpperObjectsMatched;
   [SerializeField] private int countDownObjectsMatched;

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

   public void CheckDragDropComponent(int x, int y)
   {
      xFirst = x;
      yFirst = y;

      CheckCombinations();
   }

   public void ClearCountMatched()
   {
      countUpperObjectsMatched = 0;
      countRightObjectsMatched = 0;
      countLeftObjectsMatched = 0;
      countDownObjectsMatched = 0;
   }
   
   #endregion public functions

   #region private functions

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

   private void SetRandomElementsToCells()
   {
      for (int i = 0; i < lineCount; i++)
      {
         for (int j = 0; j < columnCount; j++)
         {
            var tempRandomElementType = (ElementType) Random.Range(1,System.Enum.GetValues(typeof(ElementType)).Length);
            //Debug.Log($"current elementType ={arrayObjectsInCell[i,j].ElementType} | before");
            arrayObjectsInCell[i,j].SetElementType(tempRandomElementType);
            
            switch (tempRandomElementType)
            {
               case ElementType.Fire: arrayObjectsInCell[i,j].SetSprite(spriteFire); break;
               case ElementType.Water: arrayObjectsInCell[i,j].SetSprite(spriteWater); break;
               case ElementType.Nature: arrayObjectsInCell[i,j].SetSprite(spriteNature); break;
               case ElementType.Energy: arrayObjectsInCell[i,j].SetSprite(spriteEnergy); break;
               case ElementType.Magic: arrayObjectsInCell[i,j].SetSprite(spriteMagic); break;
               default: arrayObjectsInCell[i,j].SetSprite(spriteClear);
                  break;
            }
            //Debug.Log($"current elementType ={arrayObjectsInCell[i,j].ElementType} | after");
         }
      }
      //Debug.Log($"current elementType ={arrayObjectsInCell[1,1].ElementType} | after 2");
      
   }

   private void CheckIndexesAndSetRandomElement()
   {
      CheckPanelAndAddAllToList();
      SetRandomElementsToCells();
   }

   private void CheckCombinations()
   {
      Debug.Log($"xFirst = {xFirst}, yFirst ={yFirst} | xSecond = {xSecond}, ySecond = {ySecond}");

      CheckUpper();
      CheckDown();
      CheckLeft();
      CheckRight();
      
      IsHaveCombination();
   }
   // var countTryInDirectionPositive = (columnCount - 1) - yFirst;
   // var countTryInDirectionNegative = yFirst;
   private void CheckLeft()
   {
      if (countDownObjectsMatched == 0 && countUpperObjectsMatched == 0)
      {
         for (int i = 1; i <= yFirst; i++)
         {
            if (arrayObjectsInCell[xFirst, yFirst].ElementType == arrayObjectsInCell[xFirst,yFirst + i].ElementType)
            {
               countLeftObjectsMatched++;
            }
         }
      }
      Debug.Log($"[Left] tempObj1 ElementType = {arrayObjectsInCell[xFirst,yFirst].ElementType}");
   }
   
   private void CheckRight()
   {
      var tempObject1 = arrayObjectsInCell[xFirst, yFirst];
      var tempObject2 = arrayObjectsInCell[xFirst, yFirst++];
      if (yFirst > 0 && yFirst < columnCount - 2 && countDownObjectsMatched == 0 && countUpperObjectsMatched == 0)
      {
         while (tempObject1.ElementType ==
                tempObject2.ElementType && yFirst < columnCount - 1)
         {
            countRightObjectsMatched++;
         }
      }

      if (yFirst == 0)
      {
         while (tempObject1.ElementType ==
                tempObject2.ElementType && yFirst < columnCount - 1)
         {
            countRightObjectsMatched++;
         }
      }
      Debug.Log($"[Left] tempObj1 ElementType = {arrayObjectsInCell[xFirst,yFirst].ElementType}");
   }
   
   private void CheckUpper()
   {
      var tempObject1 = arrayObjectsInCell[xFirst, yFirst];
      var tempObject2 = arrayObjectsInCell[xFirst--, yFirst];
      if (xFirst > 0 && xFirst < lineCount - 2 && countRightObjectsMatched == 0 && countLeftObjectsMatched == 0)
      {
         while (tempObject1.ElementType ==
                tempObject2.ElementType && xFirst > 0)
         {
            countUpperObjectsMatched++;
         }
      }
      if (xFirst == lineCount - 1)
      {
         while (tempObject1.ElementType ==
                tempObject2.ElementType && xFirst > 0)
         {
            countUpperObjectsMatched++;
         }
      }
      Debug.Log($"[Left] tempObj1 ElementType = {arrayObjectsInCell[xFirst,yFirst].ElementType}");
   }
   
   private void CheckDown()
   {
      var tempObject1 = arrayObjectsInCell[xFirst, yFirst];
      var tempObject2 = arrayObjectsInCell[xFirst++, yFirst];
      if (xFirst > 0 && xFirst < lineCount - 2 && countRightObjectsMatched == 0 && countLeftObjectsMatched == 0)
      {
         while (tempObject1.ElementType ==
                tempObject2.ElementType && xFirst < lineCount - 1)
         {
            countDownObjectsMatched++;
         }
      }
      if (xFirst == 0)
      {
         while (tempObject1.ElementType ==
                tempObject2.ElementType && xFirst < lineCount - 1)
         {
            countDownObjectsMatched++;
         }
      }
      Debug.Log($"[Left] tempObj1 ElementType = {arrayObjectsInCell[xFirst,yFirst].ElementType}");
   }

   private bool IsHaveCombination()
   {
      if ((countDownObjectsMatched + countLeftObjectsMatched + countRightObjectsMatched + countUpperObjectsMatched) >=
          2)
      {
         Debug.Log($"combinations confirm");
         return true;
      }
      Debug.Log($"no combinations");
      return false;
   }

   #endregion private functions

}
