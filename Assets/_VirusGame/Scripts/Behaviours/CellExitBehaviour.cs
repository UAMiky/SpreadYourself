using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VRStandardAssets.Utils;

[RequireComponent(typeof(VRInteractiveItem))]
public class CellExitBehaviour : MonoBehaviour
{
    public Transform tweenPoint;

    private CellBehaviour parent;
    private VRInteractiveItem vr;
    bool activated;

    private void Awake()
    {
        vr = GetComponent<VRInteractiveItem>();
    }

    public void Activate (CellBehaviour caller)
    {
        activated = true;
        parent = caller;
        vr.OnClick += OnVRClicked;
    }

    public void Deactivate ()
    {
        if(activated)
            vr.OnClick -= OnVRClicked;
    }

    private void OnVRClicked()
    {
        parent.Exit(tweenPoint ? tweenPoint : transform);
    }
}
