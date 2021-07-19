using UnityEngine;


public class SpawnAction : MonoBehaviour
{
    public GameObject prefab;

    public FloatVariable spawnResource;
    public float spawnCost;

    public void TrySpawnPrefab()
    {
        if(spawnResource.Value >= spawnCost)
        {
            spawnResource.AddValue(-spawnCost);
            GameObject.Instantiate(prefab);
        }
    }
}
