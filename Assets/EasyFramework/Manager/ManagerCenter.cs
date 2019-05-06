using System;
using System.Collections.Generic;
using System.Reflection;

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

        private Dictionary<string, ushort> managerdic;

        private Dictionary<ushort, EventController> iddic;


        private ManagerCenter(Dictionary<string, ushort> managetdic)
        {
            this.managerdic = managetdic;
            iddic = new Dictionary<ushort, EventController>();
        }

        /// <summary>
        /// 通过id向对应Manager发送消息
        /// </summary>
        /// <param name="id">事件id</param>
        public void SendEvent(ushort id)
        {
            int rid = EventController.GetManager(id);
            foreach(var pair in iddic)
            {
                if(pair.Key == rid)
                {
                    pair.Value.SendEvent(id);
                    break;
                }
            }
        }

        /// <summary>
        /// 初始化要挂载的Manager
        /// </summary>
        public void InitManager()
        {
            //通过反射建立所有Manager对象，并调用Manager的初始化方法
            foreach(string name in managerdic.Keys)
            {

                Type t = Type.GetType("EasyFramework."+ name);

                Type baset = t.BaseType;

                

                object obj = baset.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).GetValue(null,null);
                baset.GetMethod("InitEventController", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Invoke(obj, new object[] { managerdic[name] });
                iddic.Add(managerdic[name], (EventController)(t.GetProperty("EventCtl").GetValue(obj)));
            }
            
        }
    }
}
