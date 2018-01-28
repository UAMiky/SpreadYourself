using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.YouWin();
        Debug.Log("Ganaste");
    }
}
