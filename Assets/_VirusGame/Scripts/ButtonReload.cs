using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using DG.Tweening;

public class ButtonReload : Singleton<ButtonReload>
{

    public VRInteractiveItem vrInteractive;

    protected override void OnSetAsSingletonInstance()
    {
        vrInteractive.OnClick += onclick;
        this.gameObject.SetActive(false);
        transform.DOScale(Vector3.one * 1.3f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    private void onclick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            onclick();
    }
}
