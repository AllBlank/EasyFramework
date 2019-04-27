using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFramework
{
    public class MsgBase
    {
        private ushort msgId;

        public ushort MsgId
        {
            get
            {
                return msgId;
            }
        }

        public MsgBase(ushort msgId)
        {
            this.msgId = msgId;
        }

        public ushort GetManager()
        {
            int count = msgId / GlobalConfig.Instance.StepofManagerId;
            return (ushort)count;
        }
    }
}
