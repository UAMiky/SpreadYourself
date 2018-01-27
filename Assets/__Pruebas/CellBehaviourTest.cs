using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviourTest : MonoBehaviour
{
    public CellBehaviour beh;
    public Transform playerTrans;

	// Use this for initialization
	void Start () {
        beh.PlayerEntered(playerTrans);
        Destroy(this);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
