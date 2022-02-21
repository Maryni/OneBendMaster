using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Inspector variables
    
    [SerializeField] private Data waveData;
    [SerializeField] private int waveIndex = 0;
    [SerializeField] private float defaultBulletDamage = 5f; //set this variables to ScriptableObject in future
    
    #endregion Inspector variables

    #region private variables

    private Player player;
    private BulletsController bulletsController;
    private SaveLoadController saveLoadController;
    private ObjectPool objectPool;
    private PunBallPoolCells punBallPoolCells;

    #endregion private variables

    #region properties

    public SaveLoadController SaveLoadController => saveLoadController;
    public PunBallPoolCells PunBallPoolCells => punBallPoolCells;
    public ObjectPool ObjectPool => objectPool;
    public Data WaveData => waveData;

    #endregion properties
    
    #region Unity functions

    private void Start()
    {
        SetVariables();
        SetEnoughtBulletsSprite();
        SetWaveData();
    }

    #endregion Unity functions

    #region private functions

    private void SetVariables()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

        if (bulletsController == null)
        {
            bulletsController = FindObjectOfType<BulletsController>();
        }

        if (punBallPoolCells == null)
        {
            punBallPoolCells = FindObjectOfType<PunBallPoolCells>();
        }

        if (saveLoadController == null)
        {
            saveLoadController = FindObjectOfType<SaveLoadController>();
        }

        if (objectPool == null)
        {
            objectPool = FindObjectOfType<ObjectPool>();
        }
    }

    private void SetEnoughtBulletsSprite()
    {
        if (bulletsController.CountCurrentBullet < player.MaxBulletsCount)
        {
            var differentCount = player.MaxBulletsCount - bulletsController.CountCurrentBullet;
            for (int i = 0; i < differentCount; i++)
            {
                bulletsController.AddBulletByType();
                bulletsController.SetBulletTextForLastBullet(defaultBulletDamage.ToString());
            }
        }
    }

    private void SetWaveData()
    {
        waveData = saveLoadController.GetWaveData(waveIndex,saveLoadController.LastCompleteLevel);
    }

    #endregion private functions
}
