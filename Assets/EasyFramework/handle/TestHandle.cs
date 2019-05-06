using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFramework
{
    public class TestHandle : IEventHandle
    {

        public void ProcessEvent(ushort id, params object[] objs)
        {
            Debug.Log("hello framework");
        }
    }
}
