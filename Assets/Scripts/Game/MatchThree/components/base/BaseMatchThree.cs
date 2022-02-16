using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseMatchThree : MonoBehaviour
{
    #region private variables

    private MatchThree matchThree;

    #endregion private variables

    #region Inspector variables
    
    [SerializeField] private ElementType elementType;
    [SerializeField] private Sprite sprite;

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

    public MatchThree (ElementType elementType, Sprite sprite)
    {
        this.sprite = sprite;
        this.elementType = elementType;
    }
    
}
