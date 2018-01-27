using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

[RequireComponent(typeof(VRInteractiveItem))]
public class DoorToCellBehaviour : MonoBehaviour
{
    public CellDefinition celula;

    private Transform player;


    void Start()
    {
        GetComponent<VRInteractiveItem>().OnClick += OnVRClick;
        player = this.FindWithTag<Transform>("Player").transform;
    }

    private void OnVRClick()
    {
        GetComponent<VRInteractiveItem>().OnClick -= OnVRClick;
        CellBehaviour.Instance.PlayerEntered(player, celula);
        ManagerAudioEffect.instance.ReproducirAbrir();
    }
}
