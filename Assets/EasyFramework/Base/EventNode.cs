namespace EasyFramework
{
    /// <summary>
    /// 事件链表
    /// </summary>
    public class EventNode
    {

        private IEventHandle handle;

        public IEventHandle Handle
        {
            get
            {
                return handle;
            }
        }

        public EventNode next = null;

        public EventNode(IEventHandle handle)
        {
            this.handle = handle;
        }


        
    }
}
