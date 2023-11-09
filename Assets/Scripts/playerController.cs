using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public float speed = 10f;
    public float fireDelay = 0.125f;
    public int score = 0;
    
    private float _fireTimer = 0f;
    private float _powerLevel = 1;
    
    private bulletPoint _bulletOrigin;
    private PlayerActions _playerActions;
    private Rigidbody2D _rb;
    
    private int _lives = 3;
    private bool _invis = false;
    private float _invisTimer = 0f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bulletOrigin = transform.GetChild(0).GetComponent<bulletPoint>();
    }

    void Update()
    {

        if(_lives < 0)
        {
            Debug.Log("COMO PDOES SER TAN MALO");
            Instantiate(Resources.Load("ExplosionVFX") as GameObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if(_invis && _invisTimer >= 1f)
        {
            toggleInvis();
        }

        move();
        shoot();

        _invisTimer += Time.deltaTime;
        _fireTimer += Time.deltaTime;
    }

    void move()
    {
        Vector2 direction = _playerActions.PlayerMap.movement.ReadValue<Vector2>().normalized;
        _rb.position += direction * (_playerActions.PlayerMap.focus.ReadValue<float>() > 0 ? speed/3 : speed) * Time.deltaTime;
    }

    void shoot()
    {        
        if(_playerActions.PlayerMap.fire.ReadValue<float>() > 0 && _fireTimer >= fireDelay)
        {
            _bulletOrigin.GetComponent<bulletPoint>().shoot(Resources.Load("PlayerBasicBullet") as GameObject, new Vector2(0,1), 25, 1);
            _fireTimer = 0;
        }
    }

    void toggleInvis()
    {
        if(!_invis)
        {
            transform.GetComponent<CircleCollider2D>().enabled = _invis;
            Instantiate(Resources.Load("HitVFX") as GameObject, transform);
            _invis = !_invis;
            _invisTimer = 0f;
        } else {
            transform.GetComponent<CircleCollider2D>().enabled = _invis;
            _invis = !_invis;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        _lives -= 1;
        toggleInvis();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject collidedObj = col.gameObject;
        switch(collidedObj.tag)
        {
            case "Scrap":
                score += collidedObj.GetComponent<scrapController>().value;
                Destroy(collidedObj);
                break;
            
            case "EnemyBullet":
                _lives -= 1;
                Destroy(collidedObj);
                toggleInvis();
                break;
            
            case "Enemy":
                _lives -= 1;
                toggleInvis();
                break;

            default:
                Debug.Log("TF DID U DO");
                break;
        }
        
    }

    //* Input System Init

    void Awake()
    {
        _playerActions = new PlayerActions();
    }

    void OnEnable()
    {
        _playerActions.PlayerMap.Enable();
    }

    void OnDisable()
    {
        _playerActions.PlayerMap.Disable();
    }
}
