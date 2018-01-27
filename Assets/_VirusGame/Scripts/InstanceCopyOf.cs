using UnityEngine;
using System.Collections;

public class InstanceCopyOf : MonoBehaviour
{
   public GameObject _original;

   public void Start ()
   {
      this.enabled = false;
   }

   public void InstanceReturn ( Object i_origin)
   {
      InstanceManager.Instance.InstanceReturn(this.gameObject, this);
   }

}

