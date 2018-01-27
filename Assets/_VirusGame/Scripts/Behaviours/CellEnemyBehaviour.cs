using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CellEnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    float tweenSpeed = 0.5f;

    [SerializeField]
    float damageTweenSpeed = 0.5f;

    [SerializeField]
    float damagePerSec = 0.5f;

    private void Awake()
    {
        this.enabled = false;
    }

    private void Update()
    {
        float dmg = damagePerSec * Time.deltaTime;
        PlayerController.Instance.ReceiveDamage(dmg);
    }

    public void Activate (Transform player, float cellRadius, float playerRadius)
    {
        var pos = player.position;
        var dir = Random.onUnitSphere;

        this.transform.position = pos + dir * cellRadius;
        this.transform.DOMove(pos + dir * playerRadius, this.tweenSpeed).SetSpeedBased().OnComplete(()=>
        {
            StartDamageBehaviour();
        });
    }

    public void Deactivate ()
    {
        this.transform.DOKill();
        this.enabled = false;
    }

    private void StartDamageBehaviour()
    {
        this.enabled = true;
        this.transform.
            DOBlendableLocalMoveBy(Vector3.back * this.transform.localScale.z * 0.5f, this.damageTweenSpeed).
            SetSpeedBased().SetLoops(-1, LoopType.Yoyo);
    }
}
