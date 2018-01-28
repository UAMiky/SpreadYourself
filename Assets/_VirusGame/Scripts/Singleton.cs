using UnityEngine;
using System.Collections;

/// A generic singleton base class for Unity 3D MonoBehaviour objects.
///
/// Developed for Drakhar Studio by Miguel Company (aka UAMike)
/// 
/// Unity MonoBehaviour objects are special because they should be added to
/// a game object (either in the editor or by calling AddComponent). This
/// means that we cannot implement the usual singleton pattern, by creating
/// a readonly static variable which is directly assigned to a newly created
/// instance.
/// 
/// This class deals with these issues and other related with the singleton 
/// pattern and Unity MonoBehaviour
/// 
/// Basic usage:
/// 
///    1. Inherit from Singleton<YourClass> instead of from MonoBehaviour
///          public class MyClass : Singleton<MyClass>
///          {
///             // ...
///          }
///
///    2. If you need actions on Awake, code like this:
///          public override void Awake ()
///          {
///             base.Awake();
///             // Your awake code
///          }
/// 
///    3. If you need actions on OnDestroy, code like this:
///          public override void OnDestroy ()
///          {
///             // Your OnDestroy code
///             base.OnDestroy();
///          }
/// 
/// Handling unexpected conditions:
/// 
///    Q. What if I added a Singleton more than once in the same scene?
///    A. Don't worry. Override OnDoubleInstanceDetected to perform
///       the actions you like on the second and following instances.
/// 
///    Q. What if I forgot to add a Singleton I need to have?
///    A. You will detect it because YourClass.Instance will return null,
///       and a NullReferenceException will most likely be thrown.
///
///    Q. I want to have a Singleton automatically created, and I want
///       the same instance to be present through all the scenes of my
///       game. Is that possible?
///    A. Yes. It can be done with a policy as a second generic parameter.
///       You may code it like this:
///          public class MyPersistentSingleton : 
///             Singleton<MyPersistentSingleton, SingletonPersistentPolicy>
///          {
///             // ...
///          }
///       There are 3 available policies:
///          SingletonStandardPolicy:   Warn that object was not found
///          SingletonAutoCreatePolicy: A GameObject is created and the singleton
///                                     is attached to it
///          SingletonPersistentPolicy: A GameObject is created and the singleton
///                                     is attached to it. The created game object
///                                     is marked as non-destroyable
/// 
///    Q. When in play mode, I cannot find the singleton instance in the
///       hierarchy. Where is it?
///    A. Automatically created singletons are added to an automatically
///       created object, which depends on the policy used. One and only one
///       object is created for each policy. The name of the objects are
///       "Singletons (auto-created)" for SingletonAutoCreatePolicy and
///       "Singletons (persistent)" for SingletonPersistentPolicy.
/// 
///    Q. I have a class hierarchy, and multiple inheritance is not allowed
///       in .NET languages. How may I implement the Singleton pattern in this
///       case?
///    A. Code like in the following example:
///          BaseA.cs
///             public class BaseA : MonoBehaviour
///             {
///             }
/// 
///          A1.cs
///             using SingletonA1 = Singleton<A1, SingletonPersistentPolicy>;
/// 
///             public class A1 : BaseA
///             {
///                static public A1 Instance
///                {
///                   get { return SingletonA1.Instance; }
///                }
/// 
///                public void Awake ()
///                {
///                   if(SingletonA1.InstanceSet(this) == false)
///                   {
///                      // Double instance detected code
///                   }
///                }
/// 
///                public void OnDestroy ()
///                {
///                   if(SingletonA1.InstanceDestroyed(this) == true)
///                   {
///                      // I was the singleton and I was destroyed
///                   }
///                }
///             }
/// 
public class Singleton<T, P> : MonoBehaviour 
   where T : MonoBehaviour
   where P : SingletonPolicy, new()
{
   /// The singleton instance
   static protected T sp_instance = null;

   /// A way of knowing if the application is quitting (no Application.isQuitting flag yet!)
   static private bool sp_applicationQuitting = false;
 
   /// A convenient readonly property. Use T.Instance to access the singleton
   static public T Instance
   {
      get
      {
         // Check if instance has been assigned
         if(sp_instance == null )
         {
            // Try to find an active instance of type T
            T instance = (T) FindObjectOfType( typeof(T) );
            if(instance == null )
            {
               // Apply not found policy
               instance = OnSingletonInstanceNotFound();
            } //endif
            
            if(instance != null)
            {
               // Set instance to the discovered / created one
               InstanceSet(instance);
            } //endif
         } //endif
   
         // Return singleton instance (may be null if not found)
         return sp_instance;
      }
   }
   
   // Specify the current instance. Returns false if singleton has already
   // been assigned to a different instance
   static public bool InstanceSet( T i_instance)
   {
      bool retVal = true;
      
      // If we are the first to start, keep our reference for singleton
      if(sp_instance == null)
      {
         sp_instance = i_instance;
         new P().OnSingletonInstanceSet<T>(i_instance);
      }
      else
      {
         // If the singleton instance is not me, indicate by returning false
         if(sp_instance != i_instance)
         {
            retVal = false;
         } //endif
      } //endif
      
      return retVal;
   }
   
   // Inform of a destroyed instance. Returns true if the specified instance
   // was the singleton one
   static public bool InstanceDestroyed( T i_instance)
   {
      bool retVal = false;
      
      if( (sp_instance != null) &&
          (sp_instance == i_instance) )
      {
         sp_instance = null;
         new P().OnSingletonInstanceDestroyed<T>(i_instance);
         retVal = true;
      } //endif
      
      return retVal;
   }
   
   // A default Awake implementation. It will set the singleton instance
   public virtual void Awake ()
   {
      if(this is T)
      {
         // Try to set the singleton instance
         if(InstanceSet(this as T) == false)
         {
            // Another instance is present. Call overridable function
            OnDoubleInstanceDetected();
         }
         else
         {
            OnSetAsSingletonInstance();
         } //endif
      }
      else
      {
         // A very strange case where someone does something like
         //    public class A : Singleton<B>
         OnWrongTypeInstance();
      } //endif
   }
   
   // A default OnDestroy implementation. It will unset the singleton instance
   public virtual void OnDestroy ()
   {
      if(this is T)
      {
         if(InstanceDestroyed(this as T) == true)
         {
            OnSingletonInstanceDestroyed();
         } //endif
      } //endif
   }

   // A default OnApplicationQuit implementation. It will set the quitting flag
   public virtual void OnApplicationQuit ()
   {
      sp_applicationQuitting = true;
   }
   
   // Overridable function called when the current singleton instance is
   // destroyed
   protected virtual void OnSingletonInstanceDestroyed()
   {
   }

   // Overridable function called when set as the current singleton instance
   protected virtual void OnSetAsSingletonInstance()
   {
   }

   // Overridable function called when a second instance awakes
   protected virtual void OnDoubleInstanceDetected()
   {
      Debug.LogWarning("Second singleton of " + typeof(T).Name + "detected!\n   ", this);
   }
   
   // Overridable function called when someone does something strange like
   //    public class A : Singleton<B>
   protected virtual void OnWrongTypeInstance()
   {
      Debug.LogWarning("Singleton wrong type: '" + this.GetType().Name +
                       "' is not a '" + typeof(T).Name + "'");
   }

   // This would be virtual, but should be static as it is called when no
   // instance is set. It will apply the not-found policy
   static protected T OnSingletonInstanceNotFound ()
   {
      // Avoid creating objects if application is quitting
      if(sp_applicationQuitting == true)
         return null;

      return new P().OnSingletonInstanceNotFound<T>();
   }
   
}

/// A convenient alias when using standard policy
public class Singleton<T> : Singleton<T, SingletonStandardPolicy>
   where T : MonoBehaviour
{
}

public class SingletonPolicy
{
   public virtual T OnSingletonInstanceNotFound<T> () where T : MonoBehaviour
   {
      return null;
   }

   public virtual void OnSingletonInstanceSet<T> (T i_instance) where T : MonoBehaviour
   {
   }

   public virtual void OnSingletonInstanceDestroyed<T> (T i_instance) where T : MonoBehaviour
   {
   }
}

/// Standard policy: 
///    Just warn that object was not found
public class SingletonStandardPolicy : SingletonPolicy
{
   public override T OnSingletonInstanceNotFound<T> ()
   {
      Debug.LogWarning( typeof(T).Name + " object not found!" );
      return null;
   }
}

/// AutoCreate policy: 
///    Creates a GameObject and adds it the singleton
public class SingletonAutoCreatePolicy : SingletonPolicy
{
   static private GameObject sp_object = null;
   
   public override T OnSingletonInstanceNotFound<T> ()
   {
      if(sp_object == null)
      {
         sp_object = new GameObject("Singletons (auto-created)");
      } //endif
      
      return sp_object.AddComponent<T>();
   }
}

/// Persistent policy: 
///    Creates a GameObject, makes it nondestroyable, and adds it the singleton
public class SingletonPersistentPolicy : SingletonPolicy
{
   static private GameObject sp_object = null;

   public override T OnSingletonInstanceNotFound<T> ()
   {
      if(sp_object == null)
      {
         sp_object = new GameObject("Singletons (persistent)");
         GameObject.DontDestroyOnLoad(sp_object);
      } //endif
      
      return sp_object.AddComponent<T>();
   }

   public override void OnSingletonInstanceSet<T> (T i_instance)
   {
      GameObject.DontDestroyOnLoad(i_instance.gameObject);
   }

}
