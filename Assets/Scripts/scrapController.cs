using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrapController : MonoBehaviour
{

    public float gravity = 7.5f;
    public int value = 0;

    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        value = updateValue();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.position += new Vector2(0, -(gravity * Time.deltaTime));
        value = updateValue();
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private int updateValue()
    {
        return (int) Camera.main.WorldToScreenPoint(transform.position).y;
    }
}
