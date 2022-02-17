using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class BaseMatchThree : MonoBehaviour
{
    #region private variables

    private MatchThree matchThree;

    #endregion private variables

    #region Inspector variables
    
    [SerializeField] private ElementType elementType;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Image image;

    #endregion Inspector variables
    
    #region properties

    public MatchThree MatchThreeBase => matchThree;

    #endregion properties

    #region Unity functions

    private void Awake()
    {
        SetMatchThree(elementType, sprite);
    }

    #endregion Unity functions
    
    #region public functions

    public void SetElementType(ElementType elementType)
    {
        this.elementType = elementType;
        MatchThreeBase.SetElementType(elementType);
    }

    public void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
        image.sprite = sprite;
    }
    
    protected virtual void SetMatchThree(ElementType elementType, Sprite sprite)
    {
        matchThree = new MatchThree(elementType, sprite);
    }

    #endregion public functions 
}

public struct MatchThree
{
    #region private variables
    
    private ElementType elementType;
    private Sprite sprite;

    #endregion private variables
    
    #region properties
    
    public ElementType ElementType => elementType;
    private Sprite Sprite => sprite;

    #endregion properties

    #region public functions

    public void SetElementType(ElementType elementType)
    {
        this.elementType = elementType;
    }

    #endregion public functions
    
    public MatchThree (ElementType elementType, Sprite sprite)
    {
        this.sprite = sprite;
        this.elementType = elementType;
    }
    
}
