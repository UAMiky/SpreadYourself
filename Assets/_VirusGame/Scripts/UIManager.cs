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
    public GameObject youWinObject;
    public GameObject notEnoughObject;

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

    internal void YouWin()
    {
        int nClones = PlayerController.Instance.nClones;
        var obj = (nClones >= 30) ? youWinObject : notEnoughObject;
        if (obj)
            obj.SetActive(true);

        VRCameraFade.Instance.FadeOut(true);
        PlayerController.Instance.Deactivate();
        CellBehaviour.Instance.PlayerExited();
        ButtonReload.Instance.gameObject.SetActive(true);
    }
}
