using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
   #region private variables

   private SaveLoadController saveLoadController;
   private ObjectPool objectPool;

   #endregion private variables

   #region Unity functions

   private void Start()
   {
      SetVariables();
      GetAndSetStats();
   }

   #endregion Unity functions

   #region private functions

   private void SetVariables()
   {
      if (objectPool == null)
      {
         objectPool = FindObjectOfType<ObjectPool>();
      }

      if (saveLoadController == null)
      {
         saveLoadController = GetComponent<SaveLoadController>();
      }
   }

   private void GetAndSetStats()
   {
      var tempFire = saveLoadController.GetDefaultData().GetListStats(ElementType.Fire);
      var tempWater = saveLoadController.GetDefaultData().GetListStats(ElementType.Water);
      var tempEnergy = saveLoadController.GetDefaultData().GetListStats(ElementType.Energy);
      var tempNature = saveLoadController.GetDefaultData().GetListStats(ElementType.Nature);
      var tempMagic = saveLoadController.GetDefaultData().GetListStats(ElementType.Magic);


      SetStats(tempFire,ElementType.Fire);
      SetStats(tempWater,ElementType.Water);
      SetStats(tempEnergy,ElementType.Energy);
      SetStats(tempNature,ElementType.Nature);
      SetStats(tempMagic,ElementType.Magic);
      
      objectPool.ChangeStatsLoadState();
   }

   private void SetStats(List<float> defaulListStats, ElementType elementType)
   {
      GameObject exampleEnemy = new GameObject();
      if (elementType == ElementType.Fire)
      {
        exampleEnemy = objectPool.PrefabsEnemyList[0];
      }
      
      if (elementType == ElementType.Water)
      {
         exampleEnemy = objectPool.PrefabsEnemyList[1];
      }
      
      if (elementType == ElementType.Energy)
      {
         exampleEnemy = objectPool.PrefabsEnemyList[2];
      }
      
      if (elementType == ElementType.Nature)
      {
         exampleEnemy = objectPool.PrefabsEnemyList[3];
      }
      
      if (elementType == ElementType.Magic)
      {
         exampleEnemy = objectPool.PrefabsEnemyList[4];
      }

      exampleEnemy.GetComponent<BaseEnemy>().SetStats(
         defaulListStats[0],
         defaulListStats[1],
         defaulListStats[2],
         defaulListStats[3]);
   }

   #endregion private functions
}
