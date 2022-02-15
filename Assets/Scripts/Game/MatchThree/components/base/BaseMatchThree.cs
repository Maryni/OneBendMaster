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

    #endregion Inspector variables
    
    #region properties

    public MatchThree MatchThreeBase => matchThree;

    #endregion properties

    #region Unity functions

    private void Awake()
    {
        SetMatchThree(elementType);
    }

    #endregion Unity functions
    
    #region public functions

    protected virtual void SetMatchThree(ElementType elementType)
    {
        matchThree = new MatchThree(elementType);
    }

    #endregion public functions 
}

public struct MatchThree
{
    #region private variables
    
    private ElementType elementType;

    #endregion private variables
    
    #region properties
    
    public ElementType ElementType => elementType;

    #endregion properties

    public MatchThree (ElementType elementType)
    {
        this.elementType = elementType;
    }
    
}
