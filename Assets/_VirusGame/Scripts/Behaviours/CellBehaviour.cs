using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CellBehaviour : Singleton<CellBehaviour>
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
    float cellRadius = 10;

    [SerializeField]
    float playerRadius = 0.5f;

    [SerializeField]
    float exitTweenTime = 2;

    #region Internal Vars

    private Transform player;
    private Vector3 playerOriginalPosition;
    private Quaternion playerOriginalRotation;
    private List<CellExitBehaviour> Exits = new List<CellExitBehaviour>();
    private List<GameObject> Enemies = new List<GameObject>();
    private List<GameObject> Clones = new List<GameObject>();

    #endregion

    protected override void OnSetAsSingletonInstance()
    {
        this.enabled = false;
        this.gameObject.SetActive(false);
        this.GetComponentsInChildren<CellExitBehaviour>(this.Exits);
    }

    void Update()
    {
        EnemiesUpdate();
        ClonesUpdate();
    }

    void EnemiesUpdate()
    {
        var newEnemy = definition.enemiesSpawnConfig.Update(this.enemyPrefab, Time.deltaTime);
        if (newEnemy)
        {
            this.Enemies.Add(newEnemy);

            // Initialize enemy
            newEnemy.GetComponent<CellEnemyBehaviour>().Activate(this.player, this.cellRadius, this.playerRadius);
            newEnemy.transform.DOLookAt(Camera.main.transform.position, 0.5f);
        }
    }

    void ClonesUpdate()
    {
        var newClone = definition.clonesSpawnConfig.Update(this.clonePrefab, Time.deltaTime, this.cloneSpawn);
        if (newClone)
        {
            this.Clones.Add(newClone);

            // Initialize clone
            newClone.GetComponent<VirusCloneBehaviour>().Activate(this.player);

            // Add 1 to score
            PlayerController.Instance.CloneCreated();
        }
    }

    public void Exit(Transform tweenDestination)
    {
        // Kill all enemies
        foreach (var enemy in Enemies)
        {
            enemy.GetComponent<CellEnemyBehaviour>().Deactivate();
            InstanceManager.Instance.InstanceReturn(enemy);
        }

        // Deactivate all exits
        foreach (var exit in Exits)
            exit.Deactivate();

        // Move player to exit
        this.player.DOMove(tweenDestination.position, this.exitTweenTime).OnComplete(() =>
        {
            //play audio close
            ManagerAudioEffect.instance.ReproducirCerrar();

            // Kill all clones
            foreach (var clone in this.Clones)
            {
                clone.GetComponent<VirusCloneBehaviour>().Deactivate();
                InstanceManager.Instance.InstanceReturn(clone);
            }

            var nextCell = this.PlayerExited();
            if (nextCell)
            {
                this.PlayerEntered(this.player, nextCell);
            }
            else
            {
                // Volver al carril
                
                this.gameObject.SetActive(false);
                this.player.position = this.playerOriginalPosition;
                this.player.rotation = this.playerOriginalRotation;
                this.player.DOTogglePause();
                PlayerController.Instance.CellExited();
                this.enabled = false;
            }
        });
    }

    public void PlayerEntered(Transform playerTrans, CellDefinition cell, GameObject enemy = null)
    {
        this.definition = cell;
        if (enemy) this.enemyPrefab = enemy;
        this.PlayerEntered(playerTrans);
    }

    public void PlayerEntered(Transform playerTrans)
    {
        this.Exits.Shuffle();
        int n = Mathf.Min(this.Exits.Count, this.definition.maxExits);
        for (int i = 0; i < n; i++)
            this.Exits[i].Activate(this);

        this.player = playerTrans;
        if (this.enabled == false)
        {
            this.player.DOTogglePause();
            this.playerOriginalPosition = this.player.position;
            this.playerOriginalRotation = this.player.rotation;
        }

        this.gameObject.SetActive(true);
        this.player.position = this.transform.position;
        this.player.rotation = this.transform.rotation;
        this.definition.enemiesSpawnConfig.Init();
        this.definition.clonesSpawnConfig.Init();
        this.enabled = true;
    }

    public CellDefinition PlayerExited()
    {
        return this.definition.exitCell;
    }
}
