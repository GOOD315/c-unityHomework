using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject botPrefab;

    private int enemyCounter;
    private int maxEnemyCounter;

    void Awake()
    {
        botPrefab = Resources.Load<GameObject>("Prefabs/SoldierEnemy");
        enemyCounter = 0;
        maxEnemyCounter = 10;
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateEnemy", 3, 2);
    }

    void CreateEnemy()
    {
        float x = Random.Range(gameObject.transform.position.x - 5, gameObject.transform.position.x + 5);
        float z = Random.Range(gameObject.transform.position.z - 5, gameObject.transform.position.z + 5);
        Vector3 pos = new Vector3(x, 5f, z);
        GameObject temp = Instantiate(botPrefab, pos, Quaternion.identity);
        enemyCounter++;
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
