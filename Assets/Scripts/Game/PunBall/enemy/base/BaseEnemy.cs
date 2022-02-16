using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseEnemy : MonoBehaviour
{
    #region private variables

    private Enemy enemy;
    protected UnityAction actionOnShooted;
    protected float currentHP;
    
    #endregion private variables

    #region Inspector variables
    
    [SerializeField] private ElementType elementType;
    [Header("Stats, mod 1 = 1%"),SerializeField] private float baseHp;
    [SerializeField] private float modHp;
    [SerializeField] private float baseDamage;
    [SerializeField] private float modDamage;

    #endregion Inspector variables
    
    #region properties

    public Enemy EnemyBase => enemy;
    public float CurrentHP => currentHP;

    #endregion properties

    #region Unity functions

    private void Awake()
    {
        CalculateStatsWithMods();
    }

    #endregion Unity functions
    
    #region public functions

    protected virtual void SetEnemy(ElementType elementType, float maxHp, float damage)
    {
        enemy = new Enemy(elementType, maxHp, damage);
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

    #region private functions

    private void CalculateStatsWithMods()
    {
        var maxHPCalculated = baseHp + (baseHp * modHp);
        var damageCalculated = baseDamage + (baseDamage * modDamage);
        SetEnemy(elementType,maxHPCalculated,damageCalculated);
    }

    #endregion private functions
}

public struct Enemy
{
    #region private variables
    
    private ElementType elementType;
    private float maxHp;
    private float damage;

    #endregion private variables
    
    #region properties
    
    public ElementType ElementType => elementType;
    public float MaxHp => maxHp;
    public float Damage => damage;
    
    #endregion properties

    public Enemy (ElementType elementType, float maxHp, float damage)
    {
        this.elementType = elementType;
        this.maxHp = maxHp;
        this.damage = damage;
    }
    
}
