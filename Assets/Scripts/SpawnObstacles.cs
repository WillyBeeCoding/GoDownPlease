using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public List<GameObject> obstaclePrefabs;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public float timeBetweenSpawn;
    private float spawnTime;
    public bool canSpawn = true;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CompareState(GameState.Gameplay) & canSpawn)
        {
            if (Time.time > spawnTime)
            {
                Spawn();
                spawnTime = Time.time + timeBetweenSpawn;
            }
        }
    }

    public void Spawn()
    {

        SpawnObstacle();

    }

    private void SpawnObstacle()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Vector2 spawnPos = transform.position + new Vector3(randomX, randomY, 0);
        Quaternion spawnRot = Quaternion.Euler(0, 0, Random.Range(0, 360));
        GameObject newObstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count-1)], spawnPos, spawnRot);
    }

    public void ResetGame() {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject o in obstacles)
            { Destroy(o); }
    }
}
