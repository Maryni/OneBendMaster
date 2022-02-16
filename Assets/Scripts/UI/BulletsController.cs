using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BulletsController : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private List<GameObject> spriteBulletsList;
    [SerializeField] private GameObject exampleBulletSprite;
    [SerializeField] private Transform parentBulletSpritePanel;
    [Header("Elemental Sprites"),SerializeField] private Sprite colorFireElemental;
    [SerializeField] private Sprite colorWaterElemental;
    [SerializeField] private Sprite colorEnergyElemental;
    [SerializeField] private Sprite colorNatureElemental;
    [SerializeField] private Sprite colorMagicElemental;
    [SerializeField] private Sprite colorDefaultElemental;

    #endregion Inspector variables

    #region private variables

    private GameObject poolBullets;

    #endregion private variables

    #region properties

    public int CountCurrentBullet => spriteBulletsList.Count;

    #endregion properties

    #region Unity functions

    private void Awake()
    {
        if (poolBullets == null)
        {
            poolBullets = transform.GetChild(0).gameObject;
        }

        AddAllBulletsToList();
        gameObject.SetActive(false); 
    }

    #endregion Unity functions

    #region public functions

    public void SetLastBulletColorByType(ElementType elementType)
    {
        SetBulletColorByType(elementType,spriteBulletsList.Count-1);
    }

    public void SetBulletColorByTypeAndIndex(ElementType elementType, int index)
    {
        SetBulletColorByType(elementType, index);
    }

    public void SetBulletTextByIndex(int index, string value)
    {
        SetTextByIndex(index, value);
    }

    public void SetBulletTextForLastBullet(string value)
    {
        SetTextByIndex(spriteBulletsList.Count-1, value);
    }

    /// <summary>
    /// for add default bullet dont write #1 parameter in function
    /// </summary>
    /// <param name="elementType"></param>
    /// <param name="value"></param>
    public void AddBulletByType(ElementType elementType = ElementType.NoElement, string value = "1")
    {
        var tempObject = Instantiate(exampleBulletSprite, parentBulletSpritePanel);
        spriteBulletsList.Add(tempObject);
        SetLastBulletColorByType(elementType);
        SetTextByIndex(spriteBulletsList.Count-1,value);
        
    }

    #endregion public functions

    #region private functions

    private void AddAllBulletsToList()
    {
        
        for (int i = 0; i < parentBulletSpritePanel.childCount; i++)
        {
            var tempObject = parentBulletSpritePanel.GetChild(i).gameObject;
            if (!spriteBulletsList.Contains(tempObject))
            {
                spriteBulletsList.Add(tempObject);  
            }
        }
    }

    private void SetBulletColorByType(ElementType elementType, int index)
    {
        switch (elementType)
        {
            case ElementType.Fire:  spriteBulletsList[index].GetComponent<BulletSprite>().SetImageSprite(colorFireElemental); break;
            case ElementType.Water:  spriteBulletsList[index].GetComponent<BulletSprite>().SetImageSprite(colorWaterElemental); break;
            case ElementType.Energy:  spriteBulletsList[index].GetComponent<BulletSprite>().SetImageSprite(colorEnergyElemental); break;
            case ElementType.Nature:  spriteBulletsList[index].GetComponent<BulletSprite>().SetImageSprite(colorNatureElemental); break;
            case ElementType.Magic:  spriteBulletsList[index].GetComponent<BulletSprite>().SetImageSprite(colorMagicElemental); break;
            default: spriteBulletsList[index].GetComponent<BulletSprite>().SetImageSprite(colorDefaultElemental); break;
        }
    }

    private void SetTextByIndex(int index, string value)
    {
        spriteBulletsList[index].GetComponent<BulletSprite>().SetText(value);
    }

    #endregion private functions
}
