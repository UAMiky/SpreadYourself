using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ControllerOffset : MonoBehaviour {

    public float fuerzaSalto=2f;
    public float maxDistance = 2f;
    Rigidbody rgbd;
    public Transform cam;
    public Transform target;
    
    private void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
        cam = Camera.main.GetComponent<Transform>();
    }
    private void Start()
    {        

    }    

    void Update()
    {     
        if (transform.localPosition == Vector3.zero)
        {
            Vector3 pos = cam.transform.position;
            Vector3 dir = (target.transform.position - cam.transform.position).normalized;
            Debug.DrawLine(pos, pos + dir * 10, Color.red, 1f);

            if (Input.GetButtonDown("Fire1"))
            {
                transform.DOComplete();
                transform.DOLocalMove(Vector3.zero - dir * 2f, 2f).OnComplete(() => transform.DOLocalMove(Vector3.zero, 1f));
            }
        }
    }

}
