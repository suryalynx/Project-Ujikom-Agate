using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoint;
    public float speed = 250f;

    void Start(){
        foreach(Transform spawnPoint in spawnPoint){
            SpawnEnemy(spawnPoint);
        }
    }

    void SpawnEnemy(Transform spawnPoint){
        GameObject RandomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        GameObject enemy = Instantiate(RandomEnemy, spawnPoint.position, spawnPoint.rotation);
        SetupEnemy(enemy);
    }

    void SetupEnemy(GameObject enemy){
        Rigidbody rb = enemy.GetComponent<Rigidbody>();
        if(rb != null){
            rb.velocity = transform.forward * speed; 
        }
    }

}
