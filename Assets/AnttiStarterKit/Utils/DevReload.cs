using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AnttiStarterKit.Utils
{
    // 开发用，按R重载场景
    public class DevReload : MonoBehaviour
    {
        private void Update()
        {
            if (!Application.isEditor) return;

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneChanger.Instance.ChangeScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}