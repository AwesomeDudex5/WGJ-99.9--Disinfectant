using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameObject antibodyPrefab;
    [SerializeField] private float spawnInterval;
    [SerializeField] private int amountPerSpawn;
    [SerializeField] private int spawnRange;

    private bool canSpawnCells;

    // Start is called before the first frame update
    void Start()
    {
        canSpawnCells = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawnCells)
        {
            StartCoroutine(spawnCells());
        }

    }

    IEnumerator spawnCells()
    {
        canSpawnCells = false;
        float randomX, randomY;
        for (int i = 0; i < amountPerSpawn; i++)
        {
            randomX = Random.Range(-spawnRange, spawnRange);
            randomY = Random.Range(-spawnRange, spawnRange);
            Instantiate(cellPrefab, new Vector2(randomX, randomY), Quaternion.identity);
        }

        yield return new WaitForSeconds(spawnInterval);
        canSpawnCells = true;
    }
}
