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
    public ElementType ElementType => elementType;

    #endregion properties

    #region Unity functions

    private void Awake()
    {
        SetMatchThree(sprite);
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
    
    protected virtual void SetMatchThree(Sprite sprite)
    {
        matchThree = new MatchThree( sprite);
    }

    #endregion public functions 
}

public struct MatchThree
{
    #region private variables
    
    private Sprite sprite;

    #endregion private variables
    
    #region properties
    
    private Sprite Sprite => sprite;

    #endregion properties

    #region public functions

    public void SetElementType(ElementType elementType)
    {
       // this.elementType = elementType;
    }

    #endregion public functions
    
    public MatchThree (Sprite sprite)
    {
        this.sprite = sprite;
    }
    
}
