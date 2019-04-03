using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static Dictionary<string , MonoBehaviour> m_serviceContainer = new Dictionary<string, MonoBehaviour>();

    public static void RegisterService(MonoBehaviour service)
    {
        RegisterService(service.GetType().Name, service);
    }

    public static void RegisterService(string id, MonoBehaviour service)
    {
        VerifyId(id);
        VerifyService(service);

        m_serviceContainer[id] = service;
    }

    private static void VerifyId(string id)
    {
        if (id == null)
        {
            throw new System.ArgumentNullException("Service ID can no be null");
        }

        if (m_serviceContainer.ContainsKey(id))
        {
            throw new System.ArgumentException("Service ID already exist");
        }
    }    
    
    private static void VerifyService(MonoBehaviour service)
    {
        if (service == null)
        {
            throw new System.ArgumentNullException("Service can no be null");
        }

        if (m_serviceContainer.ContainsValue(service))
        {
            Debug.LogWarning("You have a duplicate service" );
        }
    }

    public static T GetService<T>(string id = null , bool createIfNotFound = false) where T : MonoBehaviour
    {
        //if id is null set it as the type of the service
        if (id == null)
        {
            id = typeof(T).Name;
        }

        Object service = GetServiceByIdAndDeleteIfNull(id);

        if (service != null)
        {
            return (T) service;
        }

        service = FindAndRegisterService<T>(id);

        if(service == null && createIfNotFound)
        {
            return CreateAndRegisterService<T>( id );
        }

        throw new System.NotImplementedException("No service found for id " + id);

    }
    
    private static Object GetServiceByIdAndDeleteIfNull(string id)
    {
        Object service = m_serviceContainer[id];

        if (m_serviceContainer.ContainsKey(id) && service == null)
        {
            m_serviceContainer.Remove(id);
        }
        
        return service;
    }

    private static T FindAndRegisterService <T>(string id) where T : MonoBehaviour
    {
        T service = GameObject.FindObjectOfType<T>();

        if (service != null)
        {
            RegisterService(id , service);
        }

        return service;
    }

    private static T CreateAndRegisterService<T>(string id) where T : MonoBehaviour
    {
        GameObject go = new GameObject(typeof( T ).Name);
        return go.AddComponent<T>();        
    }
    
	
}
