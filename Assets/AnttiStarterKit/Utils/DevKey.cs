using UnityEngine;

namespace AnttiStarterKit.Utils
{
    // 开发用的快捷键
    public static class DevKey
    {
        public static bool Down(KeyCode key)
        {
            return Application.isEditor && Input.GetKeyDown(key);
        }
        
        public static bool Held(KeyCode key)
        {
            return Application.isEditor && Input.GetKey(key);
        }
        
        public static bool Up(KeyCode key)
        {
            return Application.isEditor && Input.GetKeyUp(key);
        }
    }
}