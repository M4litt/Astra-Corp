using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            GameObject spawnedEnemy = Instantiate(Resources.Load("ShooterEnemy") as GameObject, new Vector2(0,2.5f), Quaternion.identity);
            spawnedEnemy.GetComponent<enemyController>().fireDelay = 0f;
        }
    }
}
