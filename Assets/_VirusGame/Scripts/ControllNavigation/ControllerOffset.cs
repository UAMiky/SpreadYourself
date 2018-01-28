using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ControllerOffset : MonoBehaviour {

    public float fuerzaSalto=2f;
    public float moveSpeed = 2f;
    Vector3 target;
    Rigidbody rgbd;
    public Transform cam;
    
    
    private void Awake()
    {
        rgbd = GetComponent<Rigidbody>();
        cam = Camera.main.GetComponent<Transform>();
        target = Vector3.zero;
    }
    private void Start()
    {        

    }    

    void Update()
    {     
       // if (transform.localPosition == Vector3.zero)
        {

            var dist = target - transform.localPosition;
            if (dist.sqrMagnitude > 0.01)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * moveSpeed);
            }
            else
            {
                transform.localPosition = target;
                target = Vector3.zero;
            }

            if (Input.GetButtonDown("Fire1"))
            {

                var positonlocal = transform.position;                

                transform.position = transform.parent.position + cam.forward * -fuerzaSalto;

                target = transform.localPosition;

                transform.position = positonlocal;

                //transform.DOComplete();
               // transform.DOLocalMove(Vector3.zero - dir * 2f, 2f).OnComplete(() => transform.DOLocalMove(Vector3.zero, 1f));
            }

            
        }
    }

    public void Deactivate()
    {
        this.enabled = false;
        this.transform.localPosition = Vector3.zero;
        this.target = Vector3.zero;
    }

    public void Activate ()
    {
        this.enabled = true;
    }
}
