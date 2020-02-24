using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedPoolingObject : MonoBehaviour
{
    public static SharedPoolingObject instance;

    [Header("Tank")]
    [SerializeField] private GameObject tankBulletPrefabs;
    [SerializeField] private Transform tankBulletParentPool;
    [SerializeField] private int tankBulletsPooled;

    [Header("Turret")]
    [SerializeField] private GameObject turretBulletPrefabs;
    [SerializeField] private Transform turretBulletParentPool;
    [SerializeField] private int turretBulletsPooled;

    private int maxPools;
    private List<GameObject> tankBullets;
    private List<GameObject> turretBullets;


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        instance = this;
        GameObject objectInstance = null;
        
        tankBullets = new List<GameObject>();
        turretBullets = new List<GameObject>();

        maxPools = Mathf.Max(tankBulletsPooled, turretBulletsPooled);
        
        for (int i = 0; i < maxPools; i++)
        {
            if (i < tankBulletsPooled)
            {
                TankBulletsInstance(objectInstance);
            }

            if (i < turretBulletsPooled)
            {
                TurretBulletsInstance(objectInstance);
            }  
        }
    }

    private void TankBulletsInstance(GameObject objectInstance)
    {
        objectInstance = Instantiate(tankBulletPrefabs, tankBulletParentPool);
        tankBullets.Add(objectInstance);
        objectInstance.GetComponent<Bullet>().Initialize();
        objectInstance.SetActive(false);
        objectInstance = null;
    }

    private void TurretBulletsInstance(GameObject objectInstance)
    {
        objectInstance = Instantiate(turretBulletPrefabs, turretBulletParentPool);
        turretBullets.Add(objectInstance);
        objectInstance.GetComponent<Bullet>().Initialize();
        objectInstance.SetActive(false);
        objectInstance = null;
    }

    public GameObject GetObject(string command)
    {
        switch (command.ToLower())
        {
            case "tank-bullet" :
                return GetTankBullet();
            case "turret-bullet":
                return GetTurretBullet();
        }

        return null;
    }

    public void DeactiveObject(GameObject bullet, string command)
    {
        switch (command.ToLower())
        {
            case "tank":
                bullet.transform.parent = tankBulletParentPool;
                bullet.SetActive(false);
                break;
            case "turret":
                bullet.transform.parent = turretBulletParentPool;
                bullet.SetActive(false);
                break;
        }
    }

    private GameObject GetTankBullet()
    {
        
        foreach (GameObject bullet in tankBullets)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }

        return null;
    }

    private GameObject GetTurretBullet()
    {

        foreach (GameObject bullet in turretBullets)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }

        return null;
    }
}
