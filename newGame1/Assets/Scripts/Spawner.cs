using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject botPrefab;

    public int enemyCounter;
    private int enemySpawnedCounter;
    private int maxEnemyCounter;
    private Coroutine Spawn;

    IEnumerator SpawnEnemy(int count)
    {
        while (enemySpawnedCounter < 10)
        {
            yield return new WaitForSeconds(Random.Range(1, 3));
            CreateEnemy();
            enemyCounter++;
            enemySpawnedCounter++;
        }

        if (Spawn != null)
        {
            StopCoroutine(Spawn);
            Spawn = null;
        }
    }

    void Awake()
    {
        botPrefab = Resources.Load<GameObject>("Prefabs/SoldierEnemy");
        enemyCounter = 0;
        maxEnemyCounter = 10;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Spawn == null)
        {
            Spawn = StartCoroutine(SpawnEnemy(maxEnemyCounter));
        }
    }

    void CreateEnemy()
    {
        float x = Random.Range(gameObject.transform.position.x - 5, gameObject.transform.position.x + 5);
        float z = Random.Range(gameObject.transform.position.z - 5, gameObject.transform.position.z + 5);
        Vector3 pos = new Vector3(x, 5f, z);
        GameObject temp = Instantiate(botPrefab, pos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCounter >= maxEnemyCounter)
        {
            CancelInvoke("CreateEnemy");
        }
    }
}
