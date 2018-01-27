using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour
{
    [SerializeField]
    CellDefinition definition;

    [SerializeField]
    GameObject clonePrefab;

    [SerializeField]
    GameObject enemyPrefab;

    #region Internal Vars

    private float enemyTimer;
    private float cloneTimer;

    // TODO
    //private PlayerInsideCellBehaviour playerBehaviour;

    #endregion

    void Awake ()
    {
        this.enabled = false;
	}
	
	void Update ()
    {
        EnemiesUpdate();
        ClonesUpdate();
	}

    void EnemiesUpdate()
    {
        var newEnemy = definition.enemiesSpawnConfig.Update(this.enemyPrefab, Time.deltaTime);
        if(newEnemy)
        {
            // Initialize enemy
        }
    }

    void ClonesUpdate()
    {
        var newClone = definition.clonesSpawnConfig.Update(this.clonePrefab, Time.deltaTime);
        if(newClone)
        {
            // Initialize clone
        }
    }

    public void PlayerEntered (Transform playerTrans, CellDefinition cell, GameObject enemy = null)
    {
        this.definition = cell;
        if (enemy) this.enemyPrefab = enemy;
        this.PlayerEntered(playerTrans);
    }

    public void PlayerEntered (Transform playerTrans)
    {
        // TODO: this.playerBehaviour = playerTrans.GetComponent<PlayerInsideCellBehaviour>();
        // this.playerBehaviour.Init();
        this.definition.enemiesSpawnConfig.Init();
        this.definition.clonesSpawnConfig.Init();
        this.enabled = true;
    }

    public CellDefinition PlayerExited ()
    {
        return this.definition.exitCell;
    }
}
