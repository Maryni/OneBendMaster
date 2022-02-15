using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ElementType
{
    NoElement,
    Fire,
    Water,
    Energy,
    Nature,
    Magic
}

public abstract class BaseBullet : MonoBehaviour
{
    #region private variables

    private Bullet bullet;
    protected float baseDamage = 1f;
    protected UnityAction actionOnShoot;

    #endregion private variables

    #region Inspector variables

    [SerializeField] protected string description;
    [SerializeField] protected float damage;
    [SerializeField] private ElementType elementType;

    #endregion Inspector variables
    
    #region properties

    public Bullet BulletBase => bullet;

    #endregion properties

    #region Unity functions

    private void Awake()
    {
        if (damage == 0)
        {
            damage = baseDamage;
        }

        if (description == null)
        {
            Debug.LogError($"No description on Bullet {this.name}");
        }
        SetBullet(damage,elementType,description);
    }

    #endregion Unity functions
    
    #region public functions

    protected virtual void SetBullet(float damage, ElementType patronElementType, string description = "")
    {
        bullet = new Bullet(damage, patronElementType, description);
    }

    protected virtual void Shoot()
    {
        actionOnShoot?.Invoke();
    }

    public void SetActionOnShoot(params UnityAction[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actionOnShoot += actions[i];
        }
    }

    #endregion public functions


}

public struct Bullet
{
    #region private variables

    private float damage;
    private ElementType elementType;
    private string description;

    #endregion private variables
    
    #region properties

    public float Damage => damage;
    public ElementType ElementType => elementType;
    public string Description => description;

    #endregion properties

    public Bullet (float damage, ElementType elementType, string description = "")
    {
        this.damage = damage;
        this.elementType = elementType;
        this.description = description;
    }
    
}
