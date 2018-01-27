using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ManagerPath : MonoBehaviour {

    public static ManagerPath instance;

    public Transform contentCamera;
    public Transform[] pointsMove;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("ya existe una clase instanciada");
        }else
        {
            instance = this;
        }
    }

    private void Start()
    {
        Vector3[] vectorMove = new Vector3[pointsMove.Length];
        for (int i =0; i < pointsMove.Length; i++)
        {
            vectorMove[i]= pointsMove[i].position;
        }
        NextCarril(vectorMove);   
    }

  public void NextCarril(Vector3[] nextArrayVector,float delay=0)
    {
        contentCamera.DOPath(nextArrayVector, 10f,PathType.CatmullRom).SetEase(Ease.Linear).SetDelay(delay);
    }

}

