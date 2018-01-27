using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VirusCloneBehaviour : MonoBehaviour
{
    [SerializeField]
    private float tweenTime;

    public void Activate (Transform player)
    {
        Vector3 pos = player.position;
        pos += Random.onUnitSphere * 3f;

        this.transform.DOMove(pos, tweenTime);
    }

    public void Deactivate()
    {
        this.transform.DOComplete();
    }
}
