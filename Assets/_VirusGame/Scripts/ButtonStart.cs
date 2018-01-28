using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using DG.Tweening;

public class ButtonStart : MonoBehaviour {

    public VRInteractiveItem vrInteractive;

    private void Awake()
    {
        vrInteractive.OnClick += onclick;
        transform.DOScale(Vector3.one * 1.3f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    private void onclick()
    {
        VRCameraFade.Instance.FadeIn(true);
        ManagerPath.instance.iniciar();
        PlayerController.Instance.Activate();
        ManagerAudioEffect.instance.ReproducirClickButton();
        UIManager.Instance.GameStarted();
        Destroy(gameObject);
    }
}
