using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Level Data",menuName = "LevelData/Create Default Level Data",order = 53)]
public class DefaultLevelData : ScriptableObject
{
    [SerializeField] private Data data;
    //definately I wanna write something more

    public Data GetDefaultData()
    {
        return data;
    }
}
