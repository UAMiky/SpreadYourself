using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviourExtensions
{
    public static T FindWithTag<T> (this MonoBehaviour behaviour, string tag) where T : class
    {
        var go = GameObject.FindGameObjectWithTag(tag);
        return go ? go.GetComponent<T>() : null;
    }
}
