using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Platinio
{

    public static class ServiceLocator
    {
        private static Dictionary<string, MonoBehaviour> serviceContainer = new Dictionary<string, MonoBehaviour>();

        public static void RegisterService(MonoBehaviour service)
        {
            RegisterService( service.GetType().Name, service );
        }

        public static void RegisterService(string id, MonoBehaviour service)
        {
            VerifyId( id );
            VerifyService( service );

            serviceContainer[id] = service;
        }

        private static void VerifyId(string id)
        {
            if (id == null)
            {
                throw new System.ArgumentNullException( "Service ID can no be null" );
            }

            if (serviceContainer.ContainsKey( id ))
            {
                throw new System.ArgumentException( "Service ID already exist" );
            }
        }

        private static void VerifyService(MonoBehaviour service)
        {
            if (service == null)
            {
                throw new System.ArgumentNullException( "Service can no be null" );
            }

            if (serviceContainer.ContainsValue( service ))
            {
                Debug.LogWarning( "You have a duplicate service" );
            }
        }

        public static void UnregisterService<T>() where T : MonoBehaviour
        {
            UnregisterService( typeof( T ).Name );
        }

        public static void UnregisterService(MonoBehaviour service)
        {
            if (serviceContainer.ContainsValue( service ))
            {
              
                foreach (string key in serviceContainer.Keys.ToList())
                {
                    if (serviceContainer[key] == service)
                    {
                        serviceContainer.Remove( key );
                    }
                }
            }
        }

        public static void UnregisterService(string id)
        {
            if (serviceContainer.ContainsKey( id ))
            {
                serviceContainer.Remove( id );
            }
        }

        public static T GetService<T>(bool createIfNotFound = false) where T : MonoBehaviour
        {
            return GetService<T>( typeof( T ).Name, createIfNotFound );
        }

        public static T GetService<T>(string id) where T : MonoBehaviour
        {
            return GetService<T>( id, false );
        }

        public static T GetService<T>(string id, bool createIfNotFound = false) where T : MonoBehaviour
        {

            Object service = GetServiceByIdAndDeleteIfNull( id );

            if (service != null)
            {
                return (T) service;
            }

            service = FindAndRegisterService<T>( id );

            if (service != null)
            {
                return (T) service;
            }

            if (service == null && createIfNotFound)
            {
                return CreateAndRegisterService<T>( id );
            }

            throw new System.NotImplementedException( "No service found for id " + id );

        }

        private static Object GetServiceByIdAndDeleteIfNull(string id)
        {
            if (!serviceContainer.ContainsKey( id ))
                return null;

            Object service = serviceContainer[id];

            if (serviceContainer.ContainsKey( id ) && service == null)
            {
                serviceContainer.Remove( id );
            }

            return service;
        }

        private static T FindAndRegisterService<T>(string id) where T : MonoBehaviour
        {
            T service = GameObject.FindObjectOfType<T>();

            if (service != null)
            {
                RegisterService( id, service );
            }

            return service;
        }

        private static T CreateAndRegisterService<T>(string id) where T : MonoBehaviour
        {
            GameObject go = new GameObject( typeof( T ).Name );
            return go.AddComponent<T>();
        }


    }
}

