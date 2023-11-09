using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pusherEnemyController : MonoBehaviour
{

    public int speed = 15;
    public Vector2 dir = new Vector2(0,1);

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
        _rb.position += (new Vector2(0,-1).normalized * 2.5f + (new Vector2(1, 0) * Mathf.Cos(_aliveTime*5)*10)) * Time.deltaTime;
        _aliveTime += Time.deltaTime;
    }

    void move()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag != "Bullet") return;

        Instantiate(Resources.Load("Scrap") as GameObject, transform.position, Quaternion.identity);
        Instantiate(Resources.Load("ExplosionVFX") as GameObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
