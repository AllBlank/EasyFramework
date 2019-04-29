using System.Collections.Generic;

namespace EasyFramework
{
    public class EventController
    {
        /// <summary>
        /// 事件树，所有的事件都将注册在这个字典中
        /// </summary>
        private Dictionary<ushort, EventNode> nodetree;

        /// <summary>
        /// 当前所属Manager的id
        /// </summary>
        private ushort managerId;


        public EventController(ushort managerId)
        {
            this.nodetree = new Dictionary<ushort, EventNode>();
            this.managerId = managerId;
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="id">事件id</param>
        /// <param name="objs">要发送的数据</param>
        public void SendEvent(ushort id,params object[] objs)
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
                    tmpnode.Handle.ProcessEvent(id, objs);
                    tmpnode = tmpnode.next;
                }
            }
            else
            {
                ManagerCenter.Instance.SendEvent(id);
            }
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="id">事件id</param>
        /// <param name="handle">处理事件的handle</param>
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

        /// <summary>
        /// 解除事件注册
        /// </summary>
        /// <param name="id">事件id</param>
        /// <param name="handle">处理事件的handle</param>
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

        /// <summary>
        /// 通过id获取事件所属的Manager
        /// </summary>
        /// <param name="msgId">事件id</param>
        /// <returns>事件所属的Manager</returns>
        public static ushort GetManager(ushort msgId)
        {
            int count = msgId / GlobalConfig.Instance.StepofManagerId;
            return (ushort)count;
        }
    }
}
