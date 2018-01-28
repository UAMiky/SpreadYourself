using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public Renderer shell;

    [Header("Energy")]

    [SerializeField]
    float totalEnergy = 20;

    [SerializeField]
    float energyRecoverPerSecond = 1;

    float _currentEnergy;
    float currentEnergy
    {
        get { return _currentEnergy; }
        set
        {
            _currentEnergy = Mathf.Clamp(value, 0, totalEnergy);
            UIManager.Instance.EnergyUpdated(_currentEnergy, totalEnergy);
        }
    }

    [Header("Clones")]

    [SerializeField]
    float bodyRecoveryTime = 20;

    int _nClones;
    int nClones
    {
        get { return _nClones; }
        set
        {
            _nClones = Mathf.Max(value, 0);
            UIManager.Instance.ClonesUpdated(_nClones);
        }
    }

    protected override void OnSetAsSingletonInstance()
    {
        this.enabled = false;
    }

    private void Update()
    {
        if (currentEnergy < totalEnergy)
            currentEnergy += Time.deltaTime * energyRecoverPerSecond;

        if (currentEnergy >= totalEnergy)
            this.enabled = false;
    }

    public void Activate()
    {
        this.nClones = 0;
        this.currentEnergy = totalEnergy;
        this.shell.enabled = true;

        InvokeRepeating("CloneKilled", bodyRecoveryTime, bodyRecoveryTime);
    }

    public void ReceiveDamage(float dmg)
    {
        if (currentEnergy > 0)
        {
            currentEnergy -= dmg;
            if (currentEnergy <= 0)
                PlayerDied();
        }
    }

    public void CellEntered ()
    {
        GetComponentInChildren<ControllerOffset>().Deactivate();
        this.enabled = false;
        CancelInvoke("CloneKilled");
    }

    public void CellExited ()
    {
        InvokeRepeating("CloneKilled", bodyRecoveryTime, bodyRecoveryTime);
        this.enabled = true;
        GetComponentInChildren<ControllerOffset>().Activate();
    }

    public void CloneCreated ()
    {
        nClones++;
    }

    public void CloneKilled ()
    {
        nClones--;
    }

    private void PlayerDied()
    {
        // TODO Use last clone ?
        Debug.Log("DIEE!!!");
    }
}
