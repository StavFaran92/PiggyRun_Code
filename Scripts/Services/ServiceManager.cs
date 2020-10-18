using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Services
{
    class ServiceManager
    {

        Dictionary<string, List<Action>> mServiceRequests;
        Dictionary<string, IService> mServices;

        public ServiceManager()
        {

            mServiceRequests = new Dictionary<string, List<Action>>();
            mServices = new Dictionary<string, IService>();
        }

        /// <summary>
        /// This method is used for service dependency reason, if a service is not yet
        /// available you can give it a request to perform when initialized,
        /// if the service is already live it will perform immidiatly
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public void CallActionWhenServiceIsLive(string name, Action action)
        {
            //--If the service was already registered, simply call the action
            if (mServices.ContainsKey(name))
            {
                action.Invoke();
            }
            else
            {
                //--if the service was not yet initialized add it to his list of requests
                if (!mServiceRequests.ContainsKey(name))
                {
                    mServiceRequests.Add(name, new List<Action>());
                }

                mServiceRequests.TryGetValue(name, out List<Action> actions);
                
                actions.Add(action);
            }
        }

        //public void CallActionWhenServiceIsLive<T>(string name, Action<T> action)
        //{
        //    T service;
        //    //--If the service was already registered, simply call the action
        //    if (mServices.ContainsKey(name))
        //    {
        //        mServices.TryGetValue(name, out var a);
        //        service = (T)a;
        //        action.Invoke(service);
        //    }
        //    else
        //    {
        //        //--if the service was not yet initialized add it to his list of requests
        //        if (!mServiceRequests.ContainsKey(name))
        //        {
        //            mServiceRequests.Add(name, new List<Action>());
        //        }

        //        mServiceRequests.TryGetValue(name, out List<Action> actions);

        //        actions.Add(action);
        //    }
        //}

        ///// <summary>
        ///// This method registers the service in the cache
        ///// and invokes any waiting requests for it.
        ///// </summary>
        ///// <param name="name"></param>
        ///// <param name="service"></param>
        //public void RegisterService<T>(string name, IService service)
        //{
        //    //--register the service
        //    mServices.Add(name, service);

        //    //--execute his waiting requests
        //    mServiceRequests.TryGetValue(name, out List<Action> actions);
        //    if (actions == null)
        //    {
        //        return;
        //    }
        //    foreach (Action action in actions) 
        //    {
        //        action.Invoke(service);
        //    }
        //}

        /// <summary>
        /// This method registers the service in the cache, It should always be called on awake
        /// </summary>
        /// <param name="name"></param>
        /// <param name="service"></param>
        public void RegisterService(string name, IService service)
        {
            //--register the service
            mServices.Add(name, service);

            //--execute his waiting requests
            mServiceRequests.TryGetValue(name, out List<Action> actions);
            if(actions == null)
            {
                return;
            }
            foreach(Action action in actions)
            {
                action.Invoke();
            }

            actions.Clear();
        }

        /// <summary>
        /// This method returns the service by name,
        /// *Important* it should not be called on Awake, this will result in unwanted behaviour
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetService<T>(string name) where T : IService
        {
            mServices.TryGetValue(name, out IService service);
            if(service.GetType() == typeof(T))
            {
                return (T)service;
            }

            return default(T);
        }

        internal void UnRegisterService(string name, IService service)
        {
            if(mServices.TryGetValue(name, out IService serviceToRemove))
            {
                if(serviceToRemove == service)
                {
                    mServices.Remove(name);
                }
            }
        }
    }
}
