using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{

    public Vector2 dir;
    public int speed;
    
    private int _damage;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rb.position += dir * speed * Time.deltaTime;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void setDamage(int damage)
    {
        _damage = damage;
        return;
    }

    public int getDamage()
    {
        return _damage;
    }
}
