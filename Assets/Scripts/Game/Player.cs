using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private float shootRate;
    [SerializeField] private List<int> maxBulletTypeCount;
    [SerializeField] private List<ElementType> maxBulletTypeElementType;
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
    private UnityAction actionAfterShootAllBullets;
    private Camera cam;
    private Coroutine shootCoroutine;
    
    #endregion private variables

    #region properties

    public List<int> MaxBulletTypeCount => maxBulletTypeCount;
    public int CurrentBulletsCount => currentBulletsCount;
    public int CurrentHp => currentHp;
    public int MaxHp => maxHp;

    #endregion properties

    #region Unity functions

    private void Start()
    {
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

    public void SetActionAfterShootAllBullets(UnityAction action)
    {
        actionAfterShootAllBullets += action;
    }
    
    public void SetCurrentBulletsForFirstBullet(string value)
    {
        for (int i = 0; i < maxBulletTypeCount.Count; i++)
        {
            if (maxBulletTypeCount[i] == 0)
            {
                maxBulletTypeCount[i] = int.Parse(value);
                break;
            }
        }
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

    public void SetCountCurrentBullets()
    {
        currentBulletsCount = maxBulletTypeCount.FirstOrDefault(x => x > 0);
    }

    public void ChangeCanShootState()
    {
        canShoot = !canShoot;
    }
    
    #endregion public functions

    #region private functions

    private void Shoot()
    {
        SetCountCurrentBullets();
        if (canShoot && currentBulletsCount > 0)
        {
            if (currentBulletsCount >= 1)
            {
                for (int i = 0; i < maxBulletTypeCount.Count; i++)
                {
                    if (maxBulletTypeCount[i] > 0)
                    {
                        maxBulletTypeCount[i] = 0;
                        break;
                    }
                } 
            }

            Debug.Log("[Shoot]");
            ShootActiveBullet(currentBulletsCount);
            
            
            if (!IsHaveNotAvalibleBullets())
            {
                for (int i = 0; i < maxBulletTypeCount.Count; i++)
                {
                    if (maxBulletTypeCount[i] > 0)
                    {
                        currentBulletsCount = maxBulletTypeCount[i];
                        break;
                    }
                }
            }
            else if (IsHaveNotAvalibleBullets())
            {
                actionAfterShootingWhenBulletsZero?.Invoke();
            }
        }
    }
    
    private bool IsHaveNotAvalibleBullets()
    {
        var allElementsZero = maxBulletTypeCount.All(x => x == 0);
        return allElementsZero;
    }

    private void ShootActiveBullet(int countBullet)
    {
        shootCoroutine = StartCoroutine(ShootActiveBulletCoroutine(countBullet));
    }
    
    private IEnumerator ShootActiveBulletCoroutine(int countCycles)
    {
        for(int i=0; i< countCycles; i++)
        {
            if (activeBullet == null)
            {
                actionOnShoot?.Invoke();
            }
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(shootRate);
                
            activeBullet.transform.position = transform.position;
            activeBullet.transform.position = new Vector3( activeBullet.transform.position.x, activeBullet.transform.position.y,activeBullet.transform.position.z + 2f);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Vector3 endPoint = hit.point;
                Vector3 direction = endPoint - transform.position;
                activeBullet.transform.LookAt(direction);
                var rig = activeBullet.GetComponent<Rigidbody>();
                rig.velocity = (direction * bulletSpeed);
                activeBullet = null;
            }
            else
            {
                Debug.LogWarning($"No hit point");
            }
        }
        StopCoroutine(ShootActiveBulletCoroutine(countCycles));
        shootCoroutine = null;
        actionAfterShootAllBullets?.Invoke();
    }

    #endregion private functions
}
