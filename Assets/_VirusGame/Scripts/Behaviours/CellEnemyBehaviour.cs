using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CellEnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private float tweenTime = 0.5f;

    public void Activate (Transform player, float cellRadius, float playerRadius)
    {
        var pos = player.position;
        var dir = Random.onUnitSphere;

        this.transform.position = pos + dir * cellRadius;
        this.transform.DOMove(pos + dir * playerRadius, this.tweenTime);
    }

    public void Deactivate ()
    {
        this.transform.DOKill();
    }
}
