using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWater : MonoBehaviour
{
    public List<GameObject> waterPrefabs;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public float timeBetweenSpawn = 10f;
    public float spawnTime;
    public bool canSpawn = true;
    // Update is called once per frame

    private void Start()
    {
        spawnTime = timeBetweenSpawn;
    }
    void Update()
    {
        if(GameManager.Instance.CompareState(GameState.Gameplay) & canSpawn){
            if (Time.time > spawnTime) {
                Spawn();
                spawnTime = Time.time + (timeBetweenSpawn - 8*Resources.Instance.GetParchedAmount());
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
        Quaternion spawnRot = Quaternion.Euler(0, 0, 0); //dont rotate it, maybe, but keep this for compatibility
        GameObject newObstacle = Instantiate(waterPrefabs[Random.Range(0,waterPrefabs.Count-1)], spawnPos , spawnRot);
    }
}
