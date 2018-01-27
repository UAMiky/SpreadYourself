using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ManagerPath : MonoBehaviour {


    public Transform contentCamera;
    public Transform[] pointsMove;

    public List<Carril> carrilesCompletos;

    private void Start()
    {
        Vector3[] vectorMove = new Vector3[pointsMove.Length];
        for (int i =0; i < pointsMove.Length; i++)
        {
            vectorMove[i]= pointsMove[i].position;
        }
        contentCamera.DOPath(vectorMove, 10f).SetEase(Ease.InOutSine);   
    }

    public void  NextDirection(int [] indexCarriles)
    {
       for(int i=0; i < carrilesCompletos.Count; i++)
        {
            for (int x = 0; x < indexCarriles.Length; x++)
            {
              
            }
        }
    }

}

[System.Serializable]
public class Carril
{
    public List<Puntos> carriles;
}

[System.Serializable]
public class Puntos
{
    public Transform point;
}