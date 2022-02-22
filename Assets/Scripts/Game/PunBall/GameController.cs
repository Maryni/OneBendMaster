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
    private SpawnController spawnController;
    private StatsController statsController;

    #endregion private variables

    #region properties

    public SaveLoadController SaveLoadController => saveLoadController;
    public PunBallPoolCells PunBallPoolCells => punBallPoolCells;
    public ObjectPool ObjectPool => objectPool;
    public int WaveIndex => waveIndex;
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

    #region public functions

    public void SetSpawnController(SpawnController spawnController)
    {
        this.spawnController = spawnController;
    }

    #endregion public functions
    
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

        if (statsController == null)
        {
           statsController = saveLoadController.GetComponent<StatsController>();
        }
    }

    private void SetEnoughtBulletsSprite()
    {
        bulletsController.SetMaxBulletCount(player.MaxBulletsCount);
        if (bulletsController.CountCurrentBullet < player.MaxBulletsCount)
        {
            var differentCount = player.MaxBulletsCount - bulletsController.CountCurrentBullet;
            for (int i = 0; i < differentCount; i++)
            {
                bulletsController.AddBulletByType();
                bulletsController.SetBulletTextForLastBullet(defaultBulletDamage.ToString());
            }
        }
        bulletsController.SetAvalibleCountBullets();
    }

    private void SetWaveData()
    {
        waveData = saveLoadController.GetWaveData(waveIndex,saveLoadController.LastCompleteLevel);
    }

    /// <summary>
    /// call only after Game scene was loaded
    /// </summary>
    private void SetActions()
    {
        spawnController.SetActionAfterSpawn(
            ()=> statsController.SetLastWaveSpawnedList(spawnController.LastWaveSpawnedList)
            );
        player.SetActionsOnShoot(CompleteWave);
    }

    private void CompleteWave()
    {
        waveIndex++;
        SetWaveData();
        spawnController.Spawn();
        statsController.SetStatsToSpawnedEnemy();
    }

    #endregion private functions
}
