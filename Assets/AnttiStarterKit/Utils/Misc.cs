using UnityEngine;

namespace AnttiStarterKit.Utils
{
    public static class Misc
    {
        // 返回 1 或 -1
        public static int PlusMinusOne()
        {
            return Random.value < 0.5f ? 1 : -1;
        }
        
        // 返回与角度对应的向量 
        public static Vector3 VectorFromAngle(float deg)
        {
            var angle = deg * Mathf.Deg2Rad;
            return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
        }
    }
}