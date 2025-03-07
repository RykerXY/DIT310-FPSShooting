using UnityEngine;

public class SpawnPrefabs : MonoBehaviour
{
    public GameObject prefab;
    public float spawnTime = 1f;
    public float spawnDelay = 1f;

    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }

    private void Spawn()
    {
        if (boxCollider == null) return;

        Vector3 spawnPosition = new Vector3(
            Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x),
            Random.Range(boxCollider.bounds.min.y, boxCollider.bounds.max.y),
            Random.Range(boxCollider.bounds.min.z, boxCollider.bounds.max.z)
        );

        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
