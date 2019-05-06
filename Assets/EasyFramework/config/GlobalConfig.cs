using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace EasyFramework
{
    public  class GlobalConfig
    {
        private static GlobalConfig instance;

        private static object locker = new object();

        public static GlobalConfig Instance
        {
            get
            {
                if(instance == null)
                {
                    lock (locker)
                    {
                        if(instance == null)
                        {
                            //读取json文件
                            Config config = JsonUtility.FromJson<Config>(File.ReadAllText(Application.streamingAssetsPath + "/frameworkconfig.json"));
                            instance = new GlobalConfig();
                            instance.stepofManagerId = config.stepofManagerId;
                            instance.managerdic = new Dictionary<string, ushort>();
                            //将配置信息中要挂载的Manager存储在字典中
                            for (int i = 0; i < config.initmanager.Length; i++)
                            {
                                instance.managerdic.Add(config.initmanager[i].managername, config.initmanager[i].managerId);
                            }
                        }
                    }
                }
                
                return instance;
            }
        }

        private ushort stepofManagerId;

        public ushort StepofManagerId
        {
            get
            {
                return stepofManagerId;
            }
        }

        private Dictionary<string, ushort> managerdic;

        public Dictionary<string, ushort> ManagerDic
        {
            get
            {
                return managerdic;
            }
        }
    }
}

