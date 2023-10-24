using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrapController : MonoBehaviour
{

    public float gravity = 7.5f;
    
    private int _currentValue = 500;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.position += new Vector2(0, -(gravity * Time.deltaTime));
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public int getValue()
    {
        return _currentValue;
    }
}
