using System.Collections;
using System.Collections.Generic;

namespace EasyFramework
{
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
