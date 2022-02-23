using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private int maxBulletCount;
    [SerializeField] private int maxHp;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject activeBullet;
    [SerializeField] private bool canShoot = false;

    #endregion Inspector variables

    #region private variables

    private int currentHp;
    private int currentBulletsCount;
    private UnityAction actionOnShoot;
    private UnityAction actionAfterShootingWhenBulletsZero;
    [SerializeField]private Camera cam;

    #endregion private variables

    #region properties

    public int MaxBulletsCount => maxBulletCount;
    public int CurrentBulletsCount => currentBulletsCount;
    public int CurrentHp => currentHp;
    public int MaxHp => maxHp;

    #endregion properties

    #region Unity functions

    private void Start()
    {
        SetCurrentBulletsFromMaxBulletsCount();
        SetCurrentHp();
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
    }

    #endregion Unity functions
    
    #region public functions

    public void AddMaxBulletCount()
    {
        maxBulletCount++;
    }

    public void Shoot()
    {
        if (canShoot)
        {
            currentBulletsCount--;
            ShootActiveBullet();
            if (currentBulletsCount == 0)
            {
                actionAfterShootingWhenBulletsZero?.Invoke();
            }
        }
    }

    public void SetActionsOnShoot(params UnityAction[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actionOnShoot += actions[i];
        }
    }
    
    public void SetActionAfterShootingWhenBulletsZero(params UnityAction[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actionAfterShootingWhenBulletsZero += actions[i];
        }
    }

    public void SetCurrentBulletsFromMaxBulletsCount()
    {
        currentBulletsCount = maxBulletCount;
    }
    
    public void SetCurrentHp()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int value)
    {
        currentHp -= value;
    }

    public void SetActiveBullet(GameObject bullet)
    {
        if (bullet.GetComponent<BaseBullet>().ElementType != ElementType.NoElement)
        {
            activeBullet = bullet;   
        }
        else
        {
            Debug.LogError($"ActiveBullet ElementType = NoElement");
        }

    }

    public void ChangeCanShootState()
    {
        canShoot = !canShoot;
    }
    
    #endregion public functions

    #region private functions

    private void ShootActiveBullet()
    {
        if (activeBullet != null)
        {
            activeBullet.transform.position = transform.position;
            Vector3 endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = endPoint - transform.position;
            direction.y = 1.5f;
            activeBullet.transform.LookAt(direction);
            var rig = activeBullet.GetComponent<Rigidbody>();
            rig.velocity = (direction * bulletSpeed);
            activeBullet = null;
        }
        else
        {
            Debug.LogError($"Active bullet are null");
            actionOnShoot?.Invoke();
            Debug.LogWarning($"But bullet was loaded");
            ShootActiveBullet();
        }
    }

    #endregion private functions
}
