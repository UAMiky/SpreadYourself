using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ManagerAudioEffect : MonoBehaviour {

    public static ManagerAudioEffect instance;

    public AudioClip clipAbrirMembrana;
    public AudioClip clipCerrarMembrana;
    public AudioClip riegoSan;

    public AudioSource asMembrana;
    public AudioSource asRiegoSanguineo;


    private void Awake()
    {
        instance = this;
    }

    public void ReproducirAbrir()
    {
        asMembrana.Stop();
     //   asMembrana.clip = 
    }

}
