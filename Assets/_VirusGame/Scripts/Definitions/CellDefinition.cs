using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnConfig
{
    [SerializeField]
    [Tooltip("Time to wait for the first spawn")]
    private float firstSpawnTime;

    [SerializeField]
    [Tooltip("Time to wait for the second and subsequent spawns")]
    private float nextSpawnTime;

    [SerializeField]
    [Tooltip("Total number of items to spawn")]
    private int maxSpawnNumber;

    private int itemsLeft;
    private float timer;

    // Starts this spawner
    public void Init ()
    {
        this.timer = firstSpawnTime;
        this.itemsLeft = maxSpawnNumber;
    }

    // Spawns item
    public GameObject Update (GameObject prefab, float deltaTime)
    {
        if (this.itemsLeft <= 0)
            return null;

        if (this.timer < 0)
            this.timer += nextSpawnTime;
        this.timer -= deltaTime;

        if(this.timer < 0)
        {
            this.itemsLeft--;
            return InstanceManager.Instance.InstanceGet(prefab);
        }

        return null;
    }
}

[CreateAssetMenu]
public class CellDefinition : ScriptableObject
{
    public SpawnConfig enemiesSpawnConfig;
    public SpawnConfig clonesSpawnConfig;
    public CellDefinition exitCell;
}
