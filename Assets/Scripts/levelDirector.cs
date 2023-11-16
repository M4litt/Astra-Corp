using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector : MonoBehaviour
{

    delegate IEnumerator waveSpawnMethod();

    private Queue<string> waves = new Queue<string>();

    // Start is called before the first frame update
    void Start()
    {
        waves.Enqueue("firstWave");
        waves.Enqueue("firstWave");
        waves.Enqueue("firstWave");
    }

    // Update is called once per frame
    bool spawned = false;
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && waves.Count != 0 && spawned != true)
        {
            Invoke(waves.Dequeue(), 2f);
            spawned = true;
        }
        spawned = false;
    }

    private void firstWave()
    {
        StartCoroutine(spawnShooter(2.5f, 5f));
        StartCoroutine(spawnShooter(-2.5f, 5f));

        StartCoroutine(helixSegment(1));
        StartCoroutine(helixSegment(1.5f));
        StartCoroutine(helixSegment(2));
    }

    private IEnumerator spawnShooter(float posX, float posY, float timeout = 0)
    {
        yield return new WaitForSeconds(timeout);
        Instantiate(Resources.Load("ShooterEnemy") as GameObject, new Vector2(posX, posY), Quaternion.identity);
    }

    private IEnumerator helixSegment(float timeout = 0)
    {
        yield return new WaitForSeconds(timeout);
        int aux = 1;
            for(int i = 0; i < 2; i++)
            {
                GameObject spawnedEnemy = Instantiate(Resources.Load("RusherEnemy") as GameObject, new Vector2(0,5f), Quaternion.identity);
                //spawnedEnemy.GetComponent<enemyController>().fireDelay = 0.25f;

                Func<float, float> genSalt = aux > 0 ? step => -Mathf.Cos(step*5)*5: step => Mathf.Cos(step*5)*5;

                spawnedEnemy.GetComponent<pusherEnemyController>().setSalt(genSalt);
                aux = aux * -1;
            }
    }

}
