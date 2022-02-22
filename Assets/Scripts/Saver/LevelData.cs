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
    [Header("0 element = first wave"),SerializeField] private List<Data> listDatas = new List<Data>();
    [SerializeField] private bool isLevelComplete;

    #endregion Inspector variables

    #region properties

    public bool IsLevelComplete => isLevelComplete;
    public int IndexLevel => indexLevel;

    #endregion properties
    
    #region public functions

    public void SetLevelComplete()
    {
        isLevelComplete = true;
        Debug.Log($"Level {indexLevel} is complete");
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

   [Header("5 in [0] countEnemies = 5 enemies of elemType [0]")]
   [Header("Each elementType index = countEnemies index")]
   [SerializeField] private ElementType[] elementTypes = new ElementType[5];
   [SerializeField] private int[] countEnemies = new int[5];
   [Header("0 = default, if more - enemyHP = baseHp + (baseHp * baseModHp)")]
   [SerializeField] private List<float> baseHpEnemies;
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

   #region private variables

   private Dictionary<ElementType, List<float>> dictionaryEnemyStats;

   #endregion private variables
   
   
   #region properties

   public ElementType[] ElementTypes => elementTypes;
   public int[] CountEnemies => countEnemies;
   public ElementType LastElementType => elementTypes[elementTypes.Length - 1];
   public int LastCountEnemies => countEnemies[countEnemies.Length - 1];
   public bool IsWaveHaveBoss => isWaveHaveBoss;

   #endregion properties

   #region public functions

   public List<float> GetListStats(ElementType type)
   {
       SetDictionary();
       return dictionaryEnemyStats[type];
   }
   
   public int GetCountEnemiesByIndex(ElementType elementType)
   {
       return countEnemies[(int)elementType - 1];
   }

   public List<float> GetListBaseHpEnemies()
   {
       return baseHpEnemies;
   }
   
   public List<float> GetListModHpEnemies()
   {
       return baseModHpEnemies;
   }
   
   public List<float> GetListBaseDamageEnemies()
   {
       return baseDamageEnemies;
   }
   
   public List<float> GetListModDamageEnemies()
   {
       return baseModDamageEnemies;
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

   #region private functions

   private void SetDictionary()
   {
       dictionaryEnemyStats = new Dictionary<ElementType, List<float>>();
       List<float> tempListBaseHpEnemies =GetListBaseHpEnemies();
       List<float> tempListBaseModHpEnemies = GetListModHpEnemies();
       List<float> tempListBaseDamageEnemies = GetListBaseDamageEnemies();
       List<float> tempListBaseModDamageEnemies = GetListModDamageEnemies();
       
       dictionaryEnemyStats.Add(ElementType.Fire,
           new List<float>()
           {
               tempListBaseHpEnemies[0],
               tempListBaseModHpEnemies[0],
               tempListBaseDamageEnemies[0],
               tempListBaseModDamageEnemies[0]
           });
       dictionaryEnemyStats.Add(ElementType.Water,
           new List<float>()
           {
               tempListBaseHpEnemies[1],
               tempListBaseModHpEnemies[1],
               tempListBaseDamageEnemies[1],
               tempListBaseModDamageEnemies[1]
           });
       dictionaryEnemyStats.Add(ElementType.Energy,
           new List<float>()
           {
               tempListBaseHpEnemies[2],
               tempListBaseModHpEnemies[2],
               tempListBaseDamageEnemies[2],
               tempListBaseModDamageEnemies[2]
           });
       dictionaryEnemyStats.Add(ElementType.Nature,
           new List<float>()
           {
               tempListBaseHpEnemies[3],
               tempListBaseModHpEnemies[3],
               tempListBaseDamageEnemies[3],
               tempListBaseModDamageEnemies[3]
           });
       dictionaryEnemyStats.Add(ElementType.Magic,
           new List<float>()
           {
               tempListBaseHpEnemies[4],
               tempListBaseModHpEnemies[4],
               tempListBaseDamageEnemies[4],
               tempListBaseModDamageEnemies[4]
           });
   }

   #endregion private functions
}
