using System.Collections.Generic;
using UnityEngine;

public class WallGrid : MonoBehaviour
{
    [Header("Layout")]
    [SerializeField] private GameObject wallBlockPrefab;
    [SerializeField] private int gridSize = 16;
    [SerializeField] private float blockSize = 1f;

    [Header("Durezza")]
    [SerializeField] private int defaultHealth = 1;
    [SerializeField] private int hardHealth    = 3;
    [SerializeField] [Range(0f, 1f)] 
    private float hardBlockChance = 0.15f;

    private HashSet<Vector3> _spawnedPositions = new();
    
    private void Start() => GenerateWalls();

    private void GenerateWalls()
    {
        float half = (gridSize - 1) * blockSize * 0.5f;

        for (int i = 0; i < gridSize; i++)
        {
            float t = i * blockSize - half;

            SpawnBlock(new Vector3(t,     0f, -half)); // basso
            SpawnBlock(new Vector3(t,     0f,  half)); // alto
            SpawnBlock(new Vector3(-half, 0f,  t));    // sinistra
            SpawnBlock(new Vector3( half, 0f,  t));    // destra
        }
    }

    

    private void SpawnBlock(Vector3 pos)
    {
        if (!_spawnedPositions.Add(pos)) return; // già spawned, skip

        var go = Instantiate(
            wallBlockPrefab, pos,
            Quaternion.identity, transform);

        go.transform.localScale = Vector3.one * blockSize;

        bool isHard = Random.value < hardBlockChance;
        go.GetComponent<WallBlock>()
            .Init(isHard ? hardHealth : defaultHealth);
    }
}