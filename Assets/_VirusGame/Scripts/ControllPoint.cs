using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllPoint : MonoBehaviour {

    [SerializeField] Renderer rend;
    [SerializeField] Image filled;
    public float time = 6f;

    private void OnTriggerStay(Collider other)
    { 
    }
}
