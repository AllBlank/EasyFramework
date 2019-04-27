using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EasyFramework
{
    public abstract class Singleton<T> where T : Singleton<T>, new()
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
                            instance = new T();
                        }
                       
                    }
                }
                return instance;
            }
        }


    }
}
