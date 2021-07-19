using UnityEngine;


public class SpawnAction : MonoBehaviour
{
    public GameObject prefab;

    public void SpawnPrefab()
    {
        GameObject.Instantiate(prefab); 
    }
}
