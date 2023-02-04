using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public List<GameObject> obstacles;
    public List<Sprite> obstacleSprites;
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
        if(GameManager.Instance.CompareState(GameState.Gameplay) & canSpawn){
            if (Time.time > spawnTime) {
                Spawn();
                spawnTime = Time.time + timeBetweenSpawn;
            }
        }
    }

    public void Spawn() {

        SpawnObstacle();

    }

    private void SpawnObstacle()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Vector2 spawnPos = transform.position + new Vector3(randomX, randomY, 0);
        Quaternion spawnRot = Quaternion.Euler(0, 0, Random.Range(0, 360));
        GameObject newObstacle = Instantiate(obstacles[Random.Range(0,obstacles.Count)], spawnPos , spawnRot);
    }
}
