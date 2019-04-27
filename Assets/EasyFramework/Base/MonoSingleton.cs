using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFramework
{
    public abstract  class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {

        private static T instance = null;
        static object locker = new object();

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {

                            GameObject go = FindObjectOfType<FrameworkEntry>().gameObject;
                            instance = go.AddComponent<T>();
                        }

                    }
                }
                return instance;
            }
        }

        private Dictionary<ushort, EventNode> nodetree = new Dictionary<ushort, EventNode>();

        private EventController eventController;



        private void InitEventController(ushort managerId)
        {
            eventController = new EventController(nodetree, managerId);
        }

        public EventController EventCtl
        {
            get
            {
                return eventController;
            }
        }



        protected Dictionary<ushort, EventNode> NodeTree
        {
            get
            {
                return nodetree;
            }
        }

    }
}
