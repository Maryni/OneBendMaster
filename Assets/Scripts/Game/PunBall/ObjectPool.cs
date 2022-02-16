using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum ObjectType
{
    Bullet,
    Enemy,
    MatchThreeSprite
}
public class ObjectPool : MonoBehaviour
{
    #region Inspector variables

    [Header("Prefabs for Init"),SerializeField] private List<GameObject> prefabsBulletList;
    [SerializeField] private List<GameObject> prefabsEnemyList;
    [SerializeField] private List<GameObject> prefabsSpriteList;
    [Header("Inited objects"),SerializeField] private List<GameObject> exampleBulletList;
    [SerializeField] private List<GameObject> exampleEnemyList;
    [SerializeField] private List<GameObject> exampleSpriteList;

    [Header("Transform for pools"),SerializeField] private Transform transformBulletParent;
    [SerializeField] private Transform transformEnemyParent;
    [SerializeField] private Transform transformSpriteParent;

    [Header("Count inited object for each type"),SerializeField] private int countBulletsExampleToInit;
    [SerializeField] private int countEnemyExampleToInit;
    [SerializeField] private int countSpriteExampleToInit;

    #endregion Inspector variables

    #region Unity functions

    private void Start()
    {
        Init();
    }

    #endregion Unity functions
    
    #region public functions

    public GameObject GetObjectByType(ObjectType objectType, ElementType elementType)
    {
        if (objectType == ObjectType.Bullet)
        {
           var findedObject = exampleBulletList.Where(x => x.GetComponent<BaseBullet>().BulletBase.ElementType == elementType)
               .FirstOrDefault(x => !x.activeSelf);
           if (findedObject == null)
           {
               var exampleObject =
                   exampleBulletList.FirstOrDefault(x => x.GetComponent<BaseBullet>().BulletBase.ElementType == elementType);
               var newObject = Instantiate(exampleObject, transformBulletParent);
               exampleBulletList.Add(newObject);
               return newObject;
           }
           return findedObject;
        }
        if (objectType == ObjectType.Enemy)
        {
            var findedObject = exampleEnemyList.Where(x => x.GetComponent<BaseEnemy>().EnemyBase.ElementType == elementType)
                .FirstOrDefault(x => !x.activeSelf);
            if (findedObject == null)
            {
                var exampleObject =
                    exampleEnemyList.FirstOrDefault(x => x.GetComponent<BaseEnemy>().EnemyBase.ElementType == elementType);
                var newObject = Instantiate(exampleObject, transformEnemyParent);
                exampleEnemyList.Add(newObject);
                return newObject;
            }
            return findedObject;
        }
        if (objectType == ObjectType.MatchThreeSprite)
        {
            var findedObject = exampleSpriteList.Where(x => x.GetComponent<BaseMatchThree>().MatchThreeBase.ElementType == elementType)
                .FirstOrDefault(x => !x.activeSelf);
            if (findedObject == null)
            {
                var exampleObject =
                    exampleSpriteList.FirstOrDefault(x => x.GetComponent<BaseMatchThree>().MatchThreeBase.ElementType == elementType);
                var newObject = Instantiate(exampleObject, transformSpriteParent);
                exampleSpriteList.Add(newObject);
                return newObject;
            }
            return findedObject;
        }

        Debug.LogError("Uncorrect Function GetObjectByType Work");
        return new GameObject();
    }

    #endregion public functions

    #region private functions

    private void Init()
    {
        InitDefault(prefabsBulletList,countBulletsExampleToInit,transformBulletParent,exampleBulletList);
        InitDefault(prefabsEnemyList,countEnemyExampleToInit,transformEnemyParent, exampleEnemyList);
        InitDefault(prefabsSpriteList, countSpriteExampleToInit, transformSpriteParent, exampleSpriteList);
    }

    private void InitDefault(List<GameObject> list, int countGameObjectToInit, Transform transformParent, List<GameObject> exampleList)
    {
        for (int i = 0; i < countGameObjectToInit; i++)
        {
            for (int j = 0; j < list.Count; j++)
            {
                var obj = Instantiate(list[j], transformParent);
                obj.SetActive(false);
                exampleList.Add(obj);
            }
        } 
    }

    #endregion private functions

}