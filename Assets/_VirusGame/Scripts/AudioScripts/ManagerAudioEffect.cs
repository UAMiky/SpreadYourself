using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ManagerAudioEffect : MonoBehaviour {

    public static ManagerAudioEffect instance;

    public AudioClip clipAbrirMembrana;
    public AudioClip clipCerrarMembrana;
    public AudioClip clipRiegoSan;
    public AudioClip clipClick;

    public AudioSource asMembrana;
    public AudioSource asRiegoSanguineo;
    public AudioSource asEffectSounds;


    private void Awake()
    {
        instance = this;
    }

    public void ReproducirAbrir()
    {
        asMembrana.Stop();
        asMembrana.clip = clipAbrirMembrana;
        asMembrana.Play();
    }

    public void ReproducirCerrar()
    {
        asMembrana.Stop();
        asMembrana.clip = clipCerrarMembrana;
        asMembrana.Play();
    }

    public void ReproducirRiegoSanguineo(bool isPlay)
    {
        if (isPlay)
        {
            asRiegoSanguineo.clip = clipRiegoSan;
            asRiegoSanguineo.loop = true;
            asRiegoSanguineo.Play();
        }else
        {
            asRiegoSanguineo.DOFade(0.5f,1f).SetEase(Ease.OutSine);
        }
    }
    public void ReproducirClickButton()
    {
        asEffectSounds.Stop();
        asEffectSounds.clip = clipClick;
        asEffectSounds.Play();
    }

}
