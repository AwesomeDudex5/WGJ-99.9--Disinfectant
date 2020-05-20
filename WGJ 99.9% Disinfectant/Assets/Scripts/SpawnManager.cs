using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameObject antibodyPrefab;
    [SerializeField] private int spawnRange;

    [Header("Cells Stats")]
    [SerializeField] private float cellSpawnInterval;
    [SerializeField] private int cellAmountPerSpawn;
    [SerializeField] private float cellScaleMin, cellScaleMax;

    [Header("Antibody Stats")]
    [SerializeField] private float antibodySpawnInterval;
    [SerializeField] private int antibodyAmountPerSpawn;
    [SerializeField] private float antibodyScaleMin, antibodyScaleMax;

    private bool canSpawnCells;
    public bool initializeAntibodies;
    private bool canSpawnAntibodies;


    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        canSpawnCells = true;
        canSpawnAntibodies = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawnCells)
        {
            StartCoroutine(spawnCells());
        }
        if (initializeAntibodies)
        {
            if (canSpawnAntibodies)
            {
                StartCoroutine(spawnAntibodies());
            }

        }

        // Max scale depends on the scale of Player
        cellScaleMax = player.transform.localScale.x * 1.5f;
        antibodyScaleMax = player.transform.localScale.x * 1.5f;
    }

    IEnumerator spawnCells()
    {
        canSpawnCells = false;
        float randomX, randomY, randomScale;
        for (int i = 0; i < cellAmountPerSpawn; i++)
        {
            randomX = Random.Range(player.transform.position.x - spawnRange, player.transform.position.x + spawnRange);
            randomY = Random.Range(player.transform.position.y - spawnRange, player.transform.position.y + spawnRange);
            randomScale = Random.Range(cellScaleMin, cellScaleMax);

            GameObject cellObject = Instantiate(cellPrefab, new Vector2(randomX, randomY), Quaternion.identity);
            cellObject.transform.localScale = new Vector3(randomScale, randomScale, 1);
        }

        yield return new WaitForSeconds(cellSpawnInterval);
        canSpawnCells = true;
    }

    IEnumerator spawnAntibodies()
    {
        canSpawnAntibodies = false;

        float randomX, randomY, randomScale;
        for (int i = 0; i < antibodyAmountPerSpawn; i++)
        {
            randomX = Random.Range(-spawnRange, spawnRange);
            randomY = Random.Range(-spawnRange, spawnRange);
            randomScale = Random.Range(antibodyScaleMin, antibodyScaleMax);

            GameObject antibodyObject = Instantiate(antibodyPrefab, new Vector2(randomX, randomY), Quaternion.identity);
            antibodyObject.transform.localScale = new Vector3(randomScale, randomScale, 1);
        }

        yield return new WaitForSeconds(antibodySpawnInterval);
        canSpawnAntibodies = true;

    }
}
