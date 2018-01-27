using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class ButtonStart : MonoBehaviour {

    public VRInteractiveItem vrInteractive;

    private void Awake()
    {
        vrInteractive.OnClick += onclick;
    }

    private void onclick()
    {
        VRCameraFade.Instance.FadeIn(true);
        ManagerPath.instance.iniciar();
        Destroy(gameObject);
    }
}
