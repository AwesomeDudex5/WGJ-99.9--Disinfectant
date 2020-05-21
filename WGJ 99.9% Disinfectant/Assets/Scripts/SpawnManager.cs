using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameObject[] antibodyPrefab;
    [SerializeField] private float spawnRange;
    [SerializeField] private int antibodySpawnRange;
    private Transform playerTransform;

    [Header("Cells Stats")]
    [SerializeField] private float cellSpawnInterval;
    [SerializeField] private int cellAmountPerSpawn;
    [SerializeField] private float cellScaleMin;
    [Header("In relation to player's scale")]
    [SerializeField] private float cellScaleMax;

    [Header("Antibody Stats")]
    [SerializeField] private float antibodySpawnInterval;
    [SerializeField] private int antibodyAmountPerSpawn;
    [SerializeField] private float antibodyScaleMin, antibodyScaleMax;

    private bool canSpawnCells;
    public bool initializeAntibodies;
    private bool canSpawnAntibodies;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        canSpawnCells = true;
        canSpawnAntibodies = true;
    }

    // Update is called once per frame
    void Update()
    {
    	spawnRange = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x - playerTransform.position.x;
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

    }

    IEnumerator spawnCells()
    {
        canSpawnCells = false;
        float randomX, randomY, randomScale;
        for (int i = 0; i < cellAmountPerSpawn; i++)
        {
            
            randomX = Random.Range(playerTransform.position.x - spawnRange, playerTransform.position.x + spawnRange);
            randomY = Random.Range(playerTransform.position.y - spawnRange, playerTransform.position.y + spawnRange);

            randomScale = Random.Range(playerTransform.localScale.x - cellScaleMin, cellScaleMax + playerTransform.localScale.x);
            if (playerTransform.localScale.x < 1)
            {
                randomScale = Random.Range(1.5f - cellScaleMin, cellScaleMax + 1.5f);
            }


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
        int randomIndex;
        for (int i = 0; i < antibodyAmountPerSpawn; i++)
        {
            randomIndex = Random.Range(0, antibodyPrefab.Length);

            randomX = Random.Range(playerTransform.position.x - antibodySpawnRange, playerTransform.position.x + antibodySpawnRange);
            randomY = Random.Range(playerTransform.position.y - antibodySpawnRange, playerTransform.position.y + antibodySpawnRange);
            randomScale = Random.Range(playerTransform.localScale.x - antibodyScaleMin, antibodyScaleMax + playerTransform.localScale.x);

            GameObject antibodyObject = Instantiate(antibodyPrefab[randomIndex], new Vector2(randomX, randomY), Quaternion.identity);
            antibodyObject.transform.localScale = new Vector3(randomScale, randomScale, 1);
        }

        yield return new WaitForSeconds(antibodySpawnInterval);

        antibodyAmountPerSpawn++;

        canSpawnAntibodies = true;

    }
}
