using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private Player player;
    [SerializeField] private BulletsController bulletsController;
    [SerializeField] private float defaultBulletDamage = 5f; //set this variables to ScriptableObject in future
    
    #endregion Inspector variables

    #region Unity functions

    private void Start()
    {
        SetVariables();
        SetEnoughtBulletsSprite();
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

    #endregion private functions
}
