using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VirusCloneBehaviour : MonoBehaviour
{
    [SerializeField]
    private float tweenTime;

    Transform localParent;
    Transform player;

    private void Awake()
    {
        localParent = new GameObject("AuxCloneRotation").transform;
        localParent.SetParent(this.transform);
        localParent.localPosition = Vector3.zero;
    }

    public void Activate (Transform player)
    {
        this.player = player;

        Vector3 pos = player.position;
        var dir = player.position - transform.position;
        pos -= dir.normalized * 2f;

        this.transform.DOLookAt(player.position, 0.25f);
        this.transform.DOMove(pos, tweenTime).OnComplete(() =>
        {
            localParent.SetParent(null);
            localParent.position = player.position;
            this.transform.SetParent(localParent);
            localParent.DOBlendableRotateBy(Vector3.up * 30, 1f).OnComplete(LocalParentRotate);
        });
    }

    void LocalParentRotate()
    {
        localParent.DOBlendableRotateBy(Vector3.up * 30, 1f).OnComplete(LocalParentRotate);
        this.transform.DOLookAt(player.position, 0.25f);
    }

    public void Deactivate()
    {
        this.transform.DOKill();
        localParent.DOKill();
        this.transform.SetParent(null);
        localParent.SetParent(this.transform);
    }
}
