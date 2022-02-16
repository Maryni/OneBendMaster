using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private Player player;

    #endregion Inspector variables

    #region Unity functions

    private void Start()
    {
        
    }

    #endregion Unity functions

    #region private functions

    private void SetVariables()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }

    #endregion private functions
}
