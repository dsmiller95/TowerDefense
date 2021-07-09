using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    public GameObject spawnedPrefab;
    public Vector3 spawnVolume = Vector3.one;

    public float secondsPerSpawn;
    private float lastSpawnTime = 0;
    private void Update()
    {
        if (lastSpawnTime + secondsPerSpawn < Time.time)
        {
            lastSpawnTime = Time.time;
            SpawnNext();
        }
    }

    private void SpawnNext()
    {
        if(spawnedPrefab == null)
        {
            return;
        }

        var nextPosition = new Vector3(
            Random.Range(-spawnVolume.x/2f, spawnVolume.x/2f),
            Random.Range(-spawnVolume.y/2f, spawnVolume.y/2f),
            Random.Range(-spawnVolume.z/2f, spawnVolume.z/2f)) + this.transform.position;

        var spawned = Instantiate(spawnedPrefab);
        spawned.transform.position = nextPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position, spawnVolume);
    }
}
