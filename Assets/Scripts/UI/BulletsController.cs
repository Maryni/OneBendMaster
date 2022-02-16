using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsController : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private List<GameObject> spriteBulletsList;

    #endregion Inspector variables

    #region private variables

    private GameObject poolBullets;

    #endregion private variables

    #region Unity functions

    private void Start()
    {
        if (poolBullets == null)
        {
            poolBullets = transform.GetChild(0).gameObject;
        }
    }

    #endregion Unity functions

    #region public functions

    

    #endregion public functions

    #region private functions

    

    #endregion private functions
}
