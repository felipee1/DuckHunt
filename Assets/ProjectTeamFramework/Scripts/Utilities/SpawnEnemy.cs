using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject theEnemy;
    public int enemyCount;
    public int enemyLimit =10;
    public float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyDrop());
    }
    IEnumerator EnemyDrop()
    {
       while (enemyCount < enemyLimit)
        {
            int xPos;
            int zPos;
            int i = Random.Range(0, 4);
            if (i == 0)
            {
                xPos = Random.Range(-40, 40);
                zPos = Random.Range(20, 40);
            }
            else
            {
                if (i == 1)
                {
                    xPos = Random.Range(-40, 40);
                    zPos = Random.Range(-20, -40);
                }
                else
                {
                    if (i == 2)
                    {
                        xPos = Random.Range(20, 40);
                        zPos = Random.Range(40, -40);
                    }
                    else
                    {
                        xPos = Random.Range(-20, -40);
                        zPos = Random.Range(40, -40);
                    }
                }
            }

            Instantiate(theEnemy, new Vector3(xPos, 1, zPos), Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
            enemyCount += 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
