using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Image energyBar;
    public Text clonesText;

    public void EnergyUpdated (float current, float total)
    {
        energyBar.fillAmount = current / total;
    }

    public void ClonesUpdated (int nClones)
    {
        clonesText.text = nClones.ToString();
    }
}
