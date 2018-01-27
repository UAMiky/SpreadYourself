using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnConfig
{
    [Tooltip("Time to wait for the first spawn")]
    public float firstSpawnTime;

    [Tooltip("Time to wait for the second and subsequent spawns")]
    public float nextSpawnTime;

    [Tooltip("Total number of items to spawn")]
    public int maxSpawnNumber;
}

[CreateAssetMenu]
public class CellDefinition : ScriptableObject
{
    public SpawnConfig enemiesSpawnConfig;
    public SpawnConfig clonesSpawnConfig;
}
