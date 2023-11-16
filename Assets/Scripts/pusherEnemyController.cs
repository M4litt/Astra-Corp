using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pusherEnemyController : MonoBehaviour
{

    public Vector2 dir = new Vector2(0,1);
    
    private Func<float, float> salt;
    private Rigidbody2D _rb;
    private float _aliveTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        fixRotation();
        _aliveTime += Time.deltaTime;
    }

    void move()
    {
        _rb.position += (new Vector2(0,-1).normalized * 2.5f + (new Vector2(1, 0) * salt(_aliveTime))) * Time.deltaTime;
    }

    void fixRotation()
    {
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, new Vector2(-salt(_aliveTime), 1) );
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 999f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag != "Bullet") return;

        Instantiate(Resources.Load("Scrap") as GameObject, transform.position, Quaternion.identity);
        Instantiate(Resources.Load("ExplosionVFX") as GameObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void setSalt(Func<float, float> aux)
    {
        this.salt = aux;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
