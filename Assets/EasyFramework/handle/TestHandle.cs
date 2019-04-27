using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFramework
{
    public class TestHandle : IEventHandle
    {
        public void ProcessEvent()
        {
            Debug.Log("hello framework");
        }
    }
}
