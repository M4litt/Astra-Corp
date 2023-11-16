using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{

    public int health = 5;
    public float fireDelay = 0.25f;

    private float speed = 5f;
    private Rigidbody2D _rb;
    private bulletPoint _lShooter;
    private bulletPoint _rShooter;
    private int _currentShooter;
    private float _fireTimer = 0f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _lShooter = transform.GetChild(0).GetComponent<bulletPoint>();
        _rShooter = transform.GetChild(1).GetComponent<bulletPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Instantiate(Resources.Load("Scrap") as GameObject, transform.position, Quaternion.identity);
            Instantiate(Resources.Load("ExplosionVFX") as GameObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        _rb.position += new Vector2(0,-1) * speed * Time.deltaTime;

        if(speed == 0) shoot();

        _fireTimer += Time.deltaTime;
        speed -= speed == 0 ? 0 : 0.25f;
    }

    void shoot()
    {        
        if(_fireTimer >= fireDelay && fireDelay != 0)
        {
            (_currentShooter == 0 ? _lShooter : _rShooter).GetComponent<bulletPoint>().shoot(Resources.Load("EnemyBullet") as GameObject, new Vector2(0,-1), 15, 1);
            _currentShooter += _currentShooter == 0 ? 1 : -1;
            _fireTimer = 0;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject collidingObj = col.gameObject;
        if(collidingObj.tag != "Bullet") return;
        
        health -= collidingObj.GetComponent<bulletController>().damage;
        Destroy(collidingObj);
        
    }
}
