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
   [SerializeField] private List<MatchThreeFlexibleElement> listObjectsInCell;
   
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

   #endregion private variables

   #region properties

   public int ColumnCount => columnCount;
   public int LineCount => lineCount;

   #endregion properties

   #region Unity functions

   private void Start()
   {
      
   }

   #endregion Unity functions

   #region public functions

   public void SetObjectToPanel(params GameObject[] gameObjects)
   {
      for (int i = 0; i < gameObjects.Length; i++)
      {
         gameObjects[i].transform.SetParent(gamePanelGameObject.transform);
         gameObjects[i].SetActive(true);
      }
      CheckIndexesAndSetRandomElement();
   }

   public void HidePanel()
   {
      this.gameObject.SetActive(false);
   }

   #endregion public functions

   #region private functions

   private void CheckPanelAndAddAllToList()
   {
      if (listObjectsInCell.Count < gamePanelGameObject.transform.childCount)
      {
         for (int i = 0; i < gamePanelGameObject.transform.childCount; i++)
         {
            var tempObject = gamePanelGameObject.transform.GetChild(i).GetChild(0).GetComponent<MatchThreeFlexibleElement>();
            if (!listObjectsInCell.Contains(tempObject))
            {
               listObjectsInCell.Add(tempObject);
            }
         }
      }
   }

   private void SetIndexesInCells()
   {
      int x = 0;
      int y = 0;
      for (int i = 0; i < listObjectsInCell.Count; i++)
      {
         listObjectsInCell[i].SetX(x);
         listObjectsInCell[i].SetY(y);

         x++;
         if (x >= lineCount)
         {
            x = 0;
            y++;
         }
      }
   }

   private void SetRandomElementsToCells()
   {
      for (int i = 0; i < listObjectsInCell.Count; i++)
      {
         var tempRandomElementType = (ElementType) Random.Range(1,System.Enum.GetValues(typeof(ElementType)).Length);
         listObjectsInCell[i].SetElementType(tempRandomElementType);
         switch (tempRandomElementType)
         {
            case ElementType.Fire: listObjectsInCell[i].SetSprite(spriteFire); break;
            case ElementType.Water: listObjectsInCell[i].SetSprite(spriteWater); break;
            case ElementType.Nature: listObjectsInCell[i].SetSprite(spriteNature); break;
            case ElementType.Energy: listObjectsInCell[i].SetSprite(spriteEnergy); break;
            case ElementType.Magic: listObjectsInCell[i].SetSprite(spriteMagic); break;
            default: listObjectsInCell[i].SetSprite(spriteClear);
               break;
         }
      }
   }

   private void CheckIndexesAndSetRandomElement()
   {
      CheckPanelAndAddAllToList();
      SetIndexesInCells();
      SetRandomElementsToCells();
   }
   
   #endregion private functions

}
