using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFramework
{
    public class FrameworkEntry : MonoBehaviour
    {

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            ManagerCenter.Instance.InitManager();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                CoreManager.Instance.EventCtl.SendEvent(0);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                CoreManager.Instance.EventCtl.RegisterEvent(0, new TestHandle());
                Debug.Log("success");
            }
        }
    }
}
