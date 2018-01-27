using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ControllerOffset : MonoBehaviour {

    public float fuerzaSalto=2f;
    public float maxDistance = 2f;
    Vector3 target;

    Rigidbody rgbd;
    public Transform cam;
    
    
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
       // if (transform.localPosition == Vector3.zero)
        {

            transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime);

            if (transform.localPosition == target)
            {
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

}
