using System.Collections;
using System.Collections.Generic;

namespace EasyFramework
{
    public interface IEventHandle
    {
        void ProcessEvent(ushort id,params object[] objs);
    }
}
