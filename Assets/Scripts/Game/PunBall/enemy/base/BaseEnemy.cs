using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseEnemy : MonoBehaviour
{
    #region private variables

    private Enemy enemy;
    protected UnityAction actionOnShooted;

    #endregion private variables

    #region Inspector variables
    
    [SerializeField] private ElementType elementType;

    #endregion Inspector variables
    
    #region properties

    public Enemy EnemyBase => enemy;

    #endregion properties

    #region Unity functions

    private void Awake()
    {
        SetEnemy(elementType);
    }

    #endregion Unity functions
    
    #region public functions

    protected virtual void SetEnemy(ElementType elementType)
    {
        enemy = new Enemy(elementType);
    }

    protected virtual void Shooted()
    {
        actionOnShooted?.Invoke();
    }

    public void SetActionOnShooted(params UnityAction[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actionOnShooted += actions[i];
        }
    }

    #endregion public functions 
}

public struct Enemy
{
    #region private variables
    
    private ElementType elementType;

    #endregion private variables
    
    #region properties
    
    public ElementType ElementType => elementType;

    #endregion properties

    public Enemy (ElementType elementType)
    {
        this.elementType = elementType;
    }
    
}
