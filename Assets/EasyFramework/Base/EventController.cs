using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFramework
{
    public class EventController
    {
        private Dictionary<ushort, EventNode> nodetree;
        private ushort managerId;


        public EventController(Dictionary<ushort, EventNode> nodetree, ushort managerId)
        {
            this.nodetree = nodetree;
            this.managerId = managerId;
        }

        public void SendEvent(ushort id)
        {
            if (GetManager(id) == managerId)
            {
                if (!nodetree.ContainsKey(id))
                {
                    //TODO:id未注册
                    return;
                }
                EventNode tmpnode = nodetree[id];
                while (tmpnode != null)
                {
                    tmpnode.Handle.ProcessEvent();
                    tmpnode = tmpnode.next;
                }
            }
            else
            {
                ManagerCenter.Instance.SendEvent(id);
            }
        }

        public void RegisterEvent(ushort id, IEventHandle handle)
        {
            if(GetManager(id) != managerId)
            {
                //TODO:Event注册在错误的Manager上
                return;
            }
            if (!nodetree.ContainsKey(id))
            {
                nodetree.Add(id, new EventNode(handle));
            }
            else
            {
                EventNode tmpnode = nodetree[id];
                while (tmpnode.next != null)
                {
                    tmpnode = tmpnode.next;
                }
                tmpnode.next = new EventNode(handle);
            }
        }


        public void UnregisterEvent(ushort id, IEventHandle handle)
        {
            if (!nodetree.ContainsKey(id))
            {
                //TODO:解除注册失败
                return;
            }
            EventNode tmpNode = nodetree[id];
            if (handle == tmpNode.Handle)
            {
                if (tmpNode.next == null)
                {
                    nodetree.Remove(id);
                }
                else
                {
                    nodetree[id] = tmpNode.next;
                }
            }
            else
            {
                while (tmpNode.next.Handle != handle)
                {
                    tmpNode = tmpNode.next;
                }
                tmpNode.next = tmpNode.next.next;
            }
        }

        public static ushort GetManager(ushort msgId)
        {
            int count = msgId / GlobalConfig.Instance.StepofManagerId;
            return (ushort)count;
        }
    }
}
