using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFramework
{
    /// <summary>
    /// Manager单例模板
    /// </summary>
    /// <typeparam name="T"></typeparam>
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
                            //FrameworkEntry是框架的入口文件，必须挂上FrameworkEntry才能启动框架
                            GameObject go = FindObjectOfType<FrameworkEntry>().gameObject;
                            instance = go.AddComponent<T>();
                        }

                    }
                }
                return instance;
            }
        }

        private EventController eventController;


        /// <summary>
        /// 初始化事件控制器
        /// </summary>
        /// <param name="managerId">Manager的ID</param>
        private void InitEventController(ushort managerId)
        {
            eventController = new EventController(managerId);
        }

        public EventController EventCtl
        {
            get
            {
                return eventController;
            }
        }

    }
}
