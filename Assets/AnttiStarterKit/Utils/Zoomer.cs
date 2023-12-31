using System;
using Cinemachine;
using UnityEngine;

namespace AnttiStarterKit.Utils
{
    // 在编辑器中按Z键可以放大摄像机
    public class Zoomer : MonoBehaviour
    {
        [SerializeField] private float zoomLevel = 10f;

        private CinemachineVirtualCamera cam;
        private float originalZoom;

        private void Start()
        {
            cam = GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            if (Application.isEditor && Input.GetKeyDown(KeyCode.Z))
            {
                originalZoom = cam.m_Lens.OrthographicSize;
                cam.m_Lens.OrthographicSize = zoomLevel;
            }
            
            if (Application.isEditor && Input.GetKeyUp(KeyCode.Z))
            {
                cam.m_Lens.OrthographicSize = originalZoom;
            }
        }
    }
}