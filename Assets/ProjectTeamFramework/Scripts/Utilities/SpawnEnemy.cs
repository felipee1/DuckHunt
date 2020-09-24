using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject theEnemy;
    public float xPos;
    public float spawnRadius = 7;
    public float zPos;
    public int enemyCount;
    public float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyDrop());
    }
    IEnumerator EnemyDrop()
    {
       while (enemyCount < 10)
        {
            //TODO FAZER A PORRA DO ENEMY APARECER NA FRENTE
            Vector3 spawnPos = GameObject.Find("Player").transform.forward*20;
            xPos = Random.Range(-10, 10);           
            Instantiate(theEnemy, new Vector3(xPos,1, spawnPos.z), Quaternion.identity);
            yield return new WaitForSeconds(0.9f);
            enemyCount += 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
