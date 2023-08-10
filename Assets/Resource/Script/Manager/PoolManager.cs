using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject monsterPb;

    public List<GameObject> pool;

    private void Awake()
    {
        pool = new List<GameObject>();
    }

    public GameObject GetMonsterPb()
    {
        GameObject monster = null;

        foreach(GameObject found in pool)
        {
            if(!found.activeSelf)
            {
                monster = found;
                monster.SetActive(true);
                break;
            }
        }

        if (monster == null)
        {
            monster = Instantiate(monsterPb, transform);
            pool.Add(monster);
        }

        return monster;
    }
}
