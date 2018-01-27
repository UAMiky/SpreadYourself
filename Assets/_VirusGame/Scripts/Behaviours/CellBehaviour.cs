using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CellBehaviour : MonoBehaviour
{
    [SerializeField]
    CellDefinition definition;

    [SerializeField]
    GameObject clonePrefab;

    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    Transform cloneSpawn;

    [SerializeField]
    float exitTweenTime = 2;

    #region Internal Vars

    private Transform player;
    private List<CellExitBehaviour> Exits = new List<CellExitBehaviour>();
    private List<GameObject> Enemies = new List<GameObject>();
    private List<GameObject> Clones = new List<GameObject>();

    #endregion

    void Awake ()
    {
        this.enabled = false;
        this.GetComponentsInChildren<CellExitBehaviour>(this.Exits);
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
            this.Enemies.Add(newEnemy);

            // Initialize enemy
        }
    }

    void ClonesUpdate()
    {
        var newClone = definition.clonesSpawnConfig.Update(this.clonePrefab, Time.deltaTime, this.cloneSpawn);
        if(newClone)
        {
            this.Clones.Add(newClone);

            // Initialize clone
            newClone.GetComponent<VirusCloneBehaviour>().Activate(this.player);

            // Add 1 to score
        }
    }

    public void Exit (Transform tweenDestination)
    {
        this.enabled = false;

        // Kill all enemies
        InstanceManager.Instance.ReturnAll(this.Enemies);

        // Deactivate all exits
        foreach (var exit in Exits)
            exit.Deactivate();

        // Move player to exit
        this.player.DOMove(tweenDestination.position, this.exitTweenTime).OnComplete(() => 
        {
            // Kill all clones
            foreach(var clone in this.Clones)
            {
                clone.GetComponent<VirusCloneBehaviour>().Deactivate();
                InstanceManager.Instance.InstanceReturn(clone);
            }
            InstanceManager.Instance.ReturnAll(this.Clones);

            var nextCell = this.PlayerExited();
            if(nextCell)
            {
                this.PlayerEntered(this.player, nextCell);
            }
            else
            {
                // TODO: Volver al carril
            }
        });
    }

    public void PlayerEntered (Transform playerTrans, CellDefinition cell, GameObject enemy = null)
    {
        this.definition = cell;
        if (enemy) this.enemyPrefab = enemy;
        this.PlayerEntered(playerTrans);
    }

    public void PlayerEntered (Transform playerTrans)
    {
        this.Exits.Shuffle();
        int n = Mathf.Min(this.Exits.Count, this.definition.maxExits);
        for (int i = 0; i < n; i++)
            this.Exits[i].Activate(this);

        this.player = playerTrans;
        this.player.position = this.transform.position;
        this.definition.enemiesSpawnConfig.Init();
        this.definition.clonesSpawnConfig.Init();
        this.enabled = true;
    }

    public CellDefinition PlayerExited ()
    {
        return this.definition.exitCell;
    }
}
