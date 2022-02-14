using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PatronElementType
{
    Fire,
    Water,
    Energy,
    Nature,
    Magic
}

public abstract class BaseBullet 
{
    #region private variables

    private Bullet bullet;
    protected float baseDamage = 1f;
    protected UnityAction actionOnShoot;

    #endregion private variables

    #region Inspector variables

    [SerializeField] protected float damage;

    #endregion Inspector variables
    
    #region properties

    public Bullet BulletBase => bullet;

    #endregion properties

    #region public functions

    protected virtual void SetBullet(float damage, PatronElementType patronElementType)
    {
        bullet = new Bullet(baseDamage, patronElementType);
    }

    protected virtual void Shoot()
    {
        actionOnShoot?.Invoke();
    }

    #endregion public functions


}

public struct Bullet
{
    #region private variables

    private float damage;
    private PatronElementType patronElementType;

    #endregion private variables
    
    #region properties

    public float Damage => damage;
    public PatronElementType PatronElementType => patronElementType;

    #endregion properties

    #region public functions

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    #endregion public functions
    
    public Bullet (float damage, PatronElementType patronElementType)
    {
        this.damage = damage;
        this.patronElementType = patronElementType;
    }



}
