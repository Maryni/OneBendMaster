using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveLoadController : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private DefaultLevelData defaultLevelData;
    [SerializeField] private List<LevelData> levelDatas;
    [SerializeField] private int lastCompleteLevel;

    #endregion Inspector variables

    #region Unity functions

    private void Start()
    {
        SetLastCompleteLevel();
    }

    #endregion Unity functions
    
    #region public functions

    public LevelData GetFullLevelData(int index)
    {
        return levelDatas[index];
    }

    public Data GetWaveData(int indexWave, int indexLevel)
    {
        return levelDatas[indexLevel].GetDataWaveByIndex(indexWave);
    }

    public Data GetDefaultData()
    {
        return defaultLevelData.GetDefaultData();
    }

    #endregion public functions

    #region private functions

    private void SetLastCompleteLevel()
    {
       lastCompleteLevel = levelDatas.FirstOrDefault(x => !x.IsLevelComplete)!.IndexLevel;
    }

    #endregion private functions
    
    
}
