using System;
using UnityEngine;

namespace AnttiStarterKit.Utils
{
    // �ӳٽ���
    public class DisableAfter : MonoBehaviour
    {
        [SerializeField] private float delay = 0.1f;

        private void Start()
        {
            Invoke(nameof(Disable), delay);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}