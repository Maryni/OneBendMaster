using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletSprite : MonoBehaviour
{
    #region Inspector variables

    private Image image;
    private Text textOnBullet;

    #endregion Inspector variables

    #region Unity functions

    private void OnEnable()
    {
        SetDefaultVariables();
    }

    #endregion Unity functions

    #region public functions

    public void SetImageSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetText(string value)
    {
        textOnBullet.text = value;
    }

    #endregion public functions

    #region private functions

    private void SetDefaultVariables()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        if (textOnBullet == null)
        {
            textOnBullet = GetComponentInChildren<Text>();
        } 
    }

    #endregion private functions
}
