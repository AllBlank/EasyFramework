using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFramework
{
    /// <summary>
    /// 进行框架需要的初始化操作
    /// </summary>
    public class FrameworkEntry : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            ManagerCenter.Instance.InitManager();
        }
    }
}
