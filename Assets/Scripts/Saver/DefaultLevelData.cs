using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Level Data",menuName = "LevelData/Create Default Level Data",order = 53)]
public class DefaultLevelData : ScriptableObject
{
    #region Inspector variables

    [SerializeField] private Data data;
    private Dictionary<ElementType, List<float>> dictionaryEnemyStats;

    #endregion Inspector variables

    #region public functions

    public Data GetDefaultData()
    {
        return data;
    }

    public List<float> GetListStats(ElementType elementType)
    {
        SetDictionary();
        return dictionaryEnemyStats[elementType];
    }
    
    #endregion public functions

    #region private functions

    private void SetDictionary()
    {
        List<float> tempListBaseHpEnemies = data.GetListBaseHpEnemies();
        List<float> tempListBaseModHpEnemies = data.GetListModHpEnemies();
        List<float> tempListBaseDamageEnemies = data.GetListBaseDamageEnemies();
        List<float> tempListBaseModDamageEnemies = data.GetListModDamageEnemies();
        
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
