using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VRStandardAssets.Utils;

[RequireComponent(typeof(VRInteractiveItem))]
public class CellExitBehaviour : MonoBehaviour
{
    public Transform tweenPoint;

    private CellBehaviour parent;
    private VRInteractiveItem _vr;
    bool activated;

    private VRInteractiveItem vr { get { return _vr ?? (_vr = GetComponent<VRInteractiveItem>()); } }

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
        //playAudio
        ManagerAudioEffect.instance.ReproducirAbrir();

        parent.Exit(tweenPoint ? tweenPoint : transform);
    }
}
