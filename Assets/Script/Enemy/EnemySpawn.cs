using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn instance;
    public GameObject[] animalPrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 2.0f;
    public float checkpointDistance = 10.0f;

    private bool spawning = true;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(SpawnAnimals());
    }

    IEnumerator SpawnAnimals()
    {
        while (spawning)
        {
            SpawnAnimal();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnAnimal()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        GameObject animal = Instantiate(animalPrefabs[animalIndex], spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
        AnimalAI animalAI = animal.GetComponent<AnimalAI>();
        if (animalAI != null)
        {
            animalAI.checkpoint = CalculateCheckpointPosition(spawnPoints[spawnIndex]);
        }
    }

    Transform CalculateCheckpointPosition(Transform spawnPoint)
    {
        // Create a new GameObject to hold the checkpoint position
        GameObject checkpointObject = new GameObject("Checkpoint");
        checkpointObject.transform.position = spawnPoint.position + spawnPoint.forward * checkpointDistance;
        return checkpointObject.transform;
        
    }

    public void StopSpawning()
    {
        spawning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            Destroy(gameObject);
        }
    }
}
