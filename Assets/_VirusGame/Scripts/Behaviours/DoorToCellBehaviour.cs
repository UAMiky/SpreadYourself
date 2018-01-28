using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using VRStandardAssets.Utils;

[RequireComponent(typeof(VRInteractiveItem))]
public class DoorToCellBehaviour : MonoBehaviour
{
    public CellDefinition celula;
    public Carril carrilSalida;

    public bool triggerEnabled = false;
    public bool clickEnabled = true;

    private Transform player;


    void Start()
    {
        if(clickEnabled)
            GetComponent<VRInteractiveItem>().OnClick += OnVRClick;

        player = this.FindWithTag<Transform>("Player").transform;
    }

    private void OnTriggerEnter (Collider other)
    {
        if (!this.enabled || !triggerEnabled)
            return;

        if (clickEnabled)
            GetComponent<VRInteractiveItem>().OnClick -= OnVRClick;

        DoEnterToCell();
    }

    private void OnVRClick()
    {
        if (!this.enabled)
            return;

        GetComponent<VRInteractiveItem>().OnClick -= OnVRClick;
        DoEnterToCell();
    }

    private void DoEnterToCell()
    {
        this.enabled = false;

        player.DOMove(this.transform.position, 0.5f).OnComplete(()=>
        {
            CellBehaviour.Instance.PlayerEntered(player, celula);
            ManagerAudioEffect.instance.ReproducirAbrir();
        });

    }

    public void ExitFromCell()
    {
        if (carrilSalida.carrill.Count > 0)
            ManagerPath.instance.NextCarril(carrilSalida);
    }
}
