using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManagerScript : MonoBehaviour
{
    public static BulletPoolManagerScript Instance;

    private List<GameObject> m_Bullets = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetBullet(GameObject bulletPrefab, Transform spawnTransform)
    {
        for (int i = 0; i < m_Bullets.Count; i++)
        {
            GameObject bullet = m_Bullets[i];
            if (bullet == null)
            {
                m_Bullets.RemoveAt(i);
                continue;
            }

            if (!bullet.activeSelf && bullet.name == bulletPrefab.name)
            {
                bullet.transform.position = spawnTransform.position;
                bullet.transform.rotation = spawnTransform.rotation;
                bullet.SetActive(true);
                return bullet;
            }
        }

        GameObject newBullet = Instantiate(bulletPrefab, spawnTransform.position, spawnTransform.rotation);
        newBullet.transform.parent = transform;
        newBullet.name = bulletPrefab.name;
        
        m_Bullets.Add(newBullet);
        return newBullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        if (bullet != null)
        {
            bullet.SetActive(false);
        }
    }
}