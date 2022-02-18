using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data",menuName = "LevelData/Create New Level Data",order = 52)]
public class LevelData : ScriptableObject
{
    #region Inspector variables

    [SerializeField] private int indexLevel;
    [Header("1 element = 1 wave"),SerializeField] public List<Data> listDatas = new List<Data>();
    [SerializeField] private bool isLevelComplete;

    #endregion Inspector variables

    #region properties

    public bool IsLevelComplete => isLevelComplete;

    #endregion properties
    
    #region public functions

    public void SetLevelComplete()
    {
        isLevelComplete = true;
    }

    public Data GetDataWaveByIndex(int index)
    {
        if (index >= 0 && index < listDatas.Count)
        {
            return listDatas[index];
        }
        Debug.LogError("index not in index range (>= 0 && < count)");
        return null;
    }

    #endregion public functions
}

[Serializable]
public class Data
{
   #region Inspector variables

   [Header("5 in [0] countEnemies = 5 enemies of elemType [0]"),Header("Each elementType index = countEnemies index"),SerializeField] private ElementType[] elementType;
   [SerializeField] private int[] countEnemies;
   [Header("0 = default, if more - enemyHP = baseHp + (baseHp * baseModHp)"),SerializeField] private List<float> baseHpEnemies;
   [SerializeField] private List<float> baseModHpEnemies;
   [SerializeField] private List<float> baseDamageEnemies;
   [SerializeField] private List<float> baseModDamageEnemies;
   [Space] [Header("Boss settings"), SerializeField] private bool isWaveHaveBoss;
   [SerializeField] private ElementType bossElementType;
   [SerializeField] private float baseHpBoss;
   [SerializeField] private float baseModHpBoss;
   [SerializeField] private float baseDamageBoss;
   [SerializeField] private float baseModDamageBoss;

   #endregion Inspector variables

   #region properties

   public ElementType[] ElementType => elementType;
   public int[] CountEnemies => countEnemies;
   public ElementType LastElementType => elementType[elementType.Length - 1];
   public int LastCountEnemies => countEnemies[countEnemies.Length - 1];
   public bool IsWaveHaveBoss => isWaveHaveBoss;

   #endregion properties

   #region public functions

   public int GetCountEnemiesByIndex(int index)
   {
       return countEnemies[index];
   }

   public ElementType GetElementTypeByIndex(int index)
   {
       return elementType[index];
   }

   public bool IsHpAreZeroByIndex(int index)
   {
       if (baseHpEnemies.Count == 0)
       {
           return true;
       }
       return baseHpEnemies[index] == 0;
   }

   #endregion public functions
}
