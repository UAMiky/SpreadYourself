using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluacionDireccion : MonoBehaviour {


     Vector3[] direccionPosition;

    public Carril[] carriles;
    private float distancia;


    private void Start()
    {
        

    }

    private void OnTriggerEnter (Collider other)
    {
        
        {
            float menor = 1000f;
            int indexMenor=0;
            for(int i =0; i < carriles.Length;i++)
            {
                distancia = Vector3.Distance(other.transform.position, carriles[i].carrill[0].point.position);
                if (menor > distancia)
                {
                    menor = distancia;
                    indexMenor = i;
                }

            }            
            Debug.Log("el index es "+indexMenor);
            direccionPosition = new Vector3[carriles[indexMenor].carrill.Count];
            for(int i = 0; i < direccionPosition.Length; i++)
            {
                direccionPosition[i] = carriles[indexMenor].carrill[i].point.position;
            }

            ManagerPath.instance.NextCarril(direccionPosition);
        }       
    }


}

[System.Serializable]
public class Carril
{
    public string carril = "Carril";
    public List<Punto> carrill;
}

[System.Serializable]
public class Punto
{
    public Transform point;
}
