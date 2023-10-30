using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{

    public Vector2 dir;
    public int speed;
    public int damage;
    
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rb.position += dir.normalized * speed * Time.deltaTime;
        if(dir != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 999f);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
