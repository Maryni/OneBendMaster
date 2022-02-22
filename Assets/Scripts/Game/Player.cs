using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private int maxBulletCount;
    [SerializeField] private int maxHp;

    #endregion Inspector variables

    #region private variables

    private int currentHp;
    private int currentBulletsCount;
    private UnityAction actionOnShoot;
    private UnityAction actionAfterShootingWhenBulletsZero;

    #endregion private variables

    #region properties

    public int MaxBulletsCount => maxBulletCount;
    public int CurrentBulletsCount => currentBulletsCount;
    public int CurrentHp => currentHp;
    public int MaxHp => maxHp;

    #endregion properties

    #region Unity functions

    private void Start()
    {
        SetCurrentBulletsFromMaxBulletsCount();
        SetCurrentHp();
    }

    #endregion Unity functions
    
    #region public functions

    public void AddMaxBulletCount()
    {
        maxBulletCount++;
    }

    public void Shoot()
    {
        currentBulletsCount--;
        actionOnShoot?.Invoke();
        if (currentBulletsCount == 0)
        {
            actionAfterShootingWhenBulletsZero?.Invoke();
        }
    }

    public void SetActionsOnShoot(params UnityAction[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actionOnShoot += actions[i];
        }
    }

    public void SetCurrentBulletsFromMaxBulletsCount()
    {
        currentBulletsCount = maxBulletCount;
    }
    
    public void SetCurrentHp()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int value)
    {
        currentHp -= value;
    }
    
    #endregion public functions
}
