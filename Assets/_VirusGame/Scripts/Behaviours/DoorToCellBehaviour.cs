using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

[RequireComponent(typeof(VRInteractiveItem))]
public class DoorToCellBehaviour : MonoBehaviour
{
    public CellDefinition celula;

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
        if (!this.enabled)
            return;

        if(triggerEnabled)
            CellBehaviour.Instance.PlayerEntered(player, celula);

        if(clickEnabled)
            GetComponent<VRInteractiveItem>().OnClick -= OnVRClick;

        this.enabled = false;
    }

    private void OnVRClick()
    {
        if (!this.enabled)
            return;

        GetComponent<VRInteractiveItem>().OnClick -= OnVRClick;
        CellBehaviour.Instance.PlayerEntered(player, celula);
        this.enabled = false;
    }
}
