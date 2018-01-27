using UnityEngine;
using System.Collections;

public class InstanceManager : Singleton<InstanceManager, SingletonAutoCreatePolicy>
{
   private Hashtable p_table = new Hashtable ();

   public GameObject InstanceGet ( GameObject i_original,
                                   Vector3    i_position,
                                   Quaternion i_rotation
                                 )
   {
      ArrayList  a;
      GameObject rv;

      if(p_table.ContainsKey(i_original) == false)
      {
         p_table.Add(i_original, new ArrayList() );
      } //endif

      a = p_table[i_original] as ArrayList;
      if(a.Count > 0)
      {
         rv = a[a.Count - 1] as GameObject;
         a.RemoveAt(a.Count - 1);
         rv.SetActive(true);
         rv.transform.position = i_position;
         rv.transform.eulerAngles = i_rotation.eulerAngles;
         rv.BroadcastMessage("Start", SendMessageOptions.DontRequireReceiver);
      }
      else
      {
         rv = Instantiate(i_original, i_position, i_rotation) as GameObject;
         InstanceCopyOf copy = rv.AddComponent<InstanceCopyOf>();
         copy._original = i_original;
      } //endif

      return rv;
   }

   public GameObject InstanceGet ( GameObject i_original)
   {
      return InstanceGet(i_original,
                         i_original.transform.position,
                         i_original.transform.rotation);
   }

   public void InstanceReturn ( GameObject i_obj)
   {
      InstanceCopyOf copy = i_obj.GetComponent<InstanceCopyOf>();

      if(copy != null)
      {
         InstanceReturn(i_obj, copy);
      } //endif
   }

   public void InstanceReturn ( GameObject     i_obj,
                                InstanceCopyOf i_copy
                              )
   {
      GameObject obj;
      ArrayList  a;

      obj = i_copy._original;
      if(p_table.ContainsKey(obj) == true)
      {
         a = p_table[obj] as ArrayList;
         a.Add(i_obj);
         i_obj.SetActive(false);
      } //endif
   }

   public void WasDestroyed ( InstanceCopyOf i_copy)
   {
      GameObject obj;
      ArrayList  a;

      obj = i_copy._original;
      if(p_table.ContainsKey(obj) == true)
      {
         a = p_table[obj] as ArrayList;
         a.Remove(i_copy.gameObject);
      } //endif
   }

}
