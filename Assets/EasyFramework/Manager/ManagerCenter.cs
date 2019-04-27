using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace EasyFramework
{
    public class ManagerCenter
    {
        private static ManagerCenter instance;

        private static object locker = new object();

        public static ManagerCenter Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new ManagerCenter(GlobalConfig.Instance.ManagerDic);
                        }
                    }
                }

                return instance;
            }
        }

        private Dictionary<string, ushort> managetdic;

        private Dictionary<ushort, EventController> iddic;


        private ManagerCenter(Dictionary<string, ushort> managetdic)
        {
            this.managetdic = managetdic;
            iddic = new Dictionary<ushort, EventController>();
        }

        public void SendEvent(ushort id)
        {
            int rid = EventController.GetManager(id);
            foreach(var pair in iddic)
            {
                if(pair.Key == rid)
                {
                    pair.Value.SendEvent(id);
                }
            }
        }


        public void InitManager()
        {
            foreach(string name in managetdic.Keys)
            {

                Type t = Type.GetType("EasyFramework."+ name);

                Type baset = t.BaseType;

                

                object obj = baset.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).GetValue(null,null);
                baset.GetMethod("InitEventController", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Invoke(obj, new object[] { managetdic[name] });
                iddic.Add(managetdic[name], (EventController)(t.GetProperty("EventCtl").GetValue(obj)));
            }
            
        }
    }
}
