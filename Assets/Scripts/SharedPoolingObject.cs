using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedPoolingObject : MonoBehaviour
{
    public static SharedPoolingObject instance;

    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform parentPool;
    [SerializeField] private int objectsPooled;

    private List<GameObject> bullets;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        instance = this;
        GameObject objectInstance;
        
        bullets = new List<GameObject>();
        
        for (int i = 0; i < objectsPooled; i++)
        {
            objectInstance = Instantiate(bulletPrefabs, parentPool);
            bullets.Add(objectInstance);
            objectInstance.GetComponent<Bullet>().Initialize();
            objectInstance.SetActive(false);
        }
    }

    public GameObject GetObject(string command)
    {
        switch (command.ToLower())
        {
            case "bullet" :
                return GetBullet();
        }

        return null;
    }

    public void DeactiveObject(GameObject bullet)
    {
        bullet.transform.parent = parentPool;
        bullet.SetActive(false);
    }

    private GameObject GetBullet()
    {
        
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }

        return null;
    }
}
