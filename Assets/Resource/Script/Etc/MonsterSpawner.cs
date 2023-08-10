using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Transform[] spawner;

    public float maxMonsters = 5f;
    public float spawnTime =5f;
    float timer;

    private void Awake()
    {
        spawner = GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        SpawnMonster();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime)  
        {
            SpawnMonster();
            timer = 0;
        }
    }

    public void SpawnMonster()
    {
        for (float i = 0; i < maxMonsters; i++)
        {
            GameObject monster = GameManager.instance.poolManager.GetMonsterPb();
            monster.transform.position = spawner[Random.Range(1, spawner.Length)].position;
        }
    }    
}
