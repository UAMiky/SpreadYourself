using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class UIManager : Singleton<UIManager>
{
    public Image energyBar;
    public Text clonesText;
    public GameObject startObject;
    public GameObject youLooseObject;

    public void GameStarted()
    {
        startObject.SetActive(false);
    }

    public void EnergyUpdated (float current, float total)
    {
        energyBar.fillAmount = current / total;
    }

    public void ClonesUpdated (int nClones)
    {
        clonesText.text = nClones.ToString();
    }

    internal void GameOver()
    {
        VRCameraFade.Instance.FadeOut(true);
        PlayerController.Instance.Deactivate();

        if (youLooseObject)
            youLooseObject.SetActive(true);

        CellBehaviour.Instance.PlayerExited();
        ButtonReload.Instance.gameObject.SetActive(true);
    }
}
