using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public Player Player => player;
    public SaveLoadController SaveLoadController => saveLoadController;
    public PunBallPoolCells PunBallPoolCells => punBallPoolCells;
    public ObjectPool ObjectPool => objectPool;
    public int WaveIndex => waveIndex;
    public Data WaveData => waveData;

    #endregion properties
    
    #region Unity functions

    private void OnEnable()
    {
        SceneManager.sceneLoaded += LoadAfterGameSceneWasLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadAfterGameSceneWasLoaded;
    }

    private void Start()
    {
        SetEnoughtBulletsSprite();
        SetWaveData();
    }

    #endregion Unity functions

    #region public functions

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
           statsController.SetGameController(this);
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
        Debug.Log($"[SetWaveData] waveIndex = {waveIndex}");
        waveData = saveLoadController.GetWaveData(waveIndex,saveLoadController.LastCompleteLevel);
    }


    private void LoadAfterGameSceneWasLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene [{scene.name}] was loaded]");
        if (scene.buildIndex == 1)
        {
            if (spawnController == null)
            {
                spawnController = FindObjectOfType<SpawnController>();
            }
            SetVariables();
            SetActions();
        }
    }
    /// <summary>
    /// call only after Game scene was loaded
    /// </summary>
    private void SetActions()
    {
        spawnController.SetActionAfterSpawn(
            ()=> statsController.SetLastWaveSpawnedList(spawnController.LastWaveSpawnedList)
            );
        player.SetActionsOnShoot(() =>player.SetActiveBullet(
                ObjectPool.GetObjectByType(
                ObjectType.Bullet, bulletsController.GetLastBulletElementType()))
        );
        bulletsController.SetActionWhenAllBulletsColored(player.ChangeCanShootState);
        player.SetActionAfterShootingWhenBulletsZero(CompleteWave);
    }

    private void CompleteWave()
    {
        if (waveIndex < saveLoadController.GetFullLevelData(saveLoadController.LastCompleteLevel).GetWaveCount())
        {
            waveIndex++; 
        }
        else if (waveIndex == saveLoadController.GetFullLevelData(saveLoadController.LastCompleteLevel).GetWaveCount() -
                 1)
        {
            saveLoadController.GetFullLevelData(saveLoadController.LastCompleteLevel).SetLevelComplete();
        }
        
        SetWaveData();
        spawnController.MovePreviousEnemyForward();
        spawnController.Spawn();
        statsController.SetStatsToSpawnedEnemy();
        player.ChangeCanShootState();
    }

    #endregion private functions
}
