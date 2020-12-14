using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private static BulletManager instance;
    public static BulletManager Instance { get { return instance; } }


    public GameObject bulletPrefab;
    public int bulletAmount = 20;

    public List<GameObject> bullets;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        bullets = new List<GameObject>(bulletAmount);

        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject newBullet = Instantiate(bulletPrefab);
            newBullet.transform.SetParent(transform);
            newBullet.SetActive(false);
            bullets.Add(newBullet);
        }
    }

    public GameObject getBullet()
    {
       foreach (GameObject bullet in bullets)
        {
            if(!bullet.activeInHierarchy)
            {
               // bullet.SetActive(true);
                return bullet;
            }
        }
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.SetParent(transform);
        bullets.Add(newBullet);

        return newBullet;
    }
}
