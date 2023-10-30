using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPoint : MonoBehaviour
{
    void Start()
    {
        // Unused
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shoot(GameObject bullet, Vector2 dir, int speed, int damage)
    {
        GameObject spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        //* Prevent self collition
        switch (transform.parent.tag)
        {
            case "Player":
                Physics2D.IgnoreCollision(transform.parent.GetComponent<CircleCollider2D>(), spawnedBullet.GetComponent<CircleCollider2D>());
                break;

            default:
                Physics2D.IgnoreCollision(transform.parent.GetComponent<BoxCollider2D>(), spawnedBullet.GetComponent<CircleCollider2D>());
                break;
        }
        
        spawnedBullet.GetComponent<bulletController>().damage = damage;
        spawnedBullet.GetComponent<bulletController>().speed = speed;
        spawnedBullet.GetComponent<bulletController>().dir = dir;
        return;
    }
}
