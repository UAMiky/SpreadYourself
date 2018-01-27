using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluacionDireccion : MonoBehaviour {


    public Transform[] direccionPosition;

    private float distancia;

    private void OnTriggerEnter (Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            float menor = 1000f;
            int indexMenor=0;
            for(int i =0; i <direccionPosition.Length;i++)
            {
                distancia = Vector3.Distance(other.transform.position, direccionPosition[i].position);
                if (menor > distancia)
                {
                    menor = distancia;
                    indexMenor = i;
                }
            }            
            Debug.Log("el index es "+indexMenor);
            //metodo para seleccionar la direccion
        }
       
    }
}
