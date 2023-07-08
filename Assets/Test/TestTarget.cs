using AnttiStarterKit.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTarget : MonoBehaviour
{
    [SerializeField] private Camera cam;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cam.ScreenToWorldPoint(Input.mousePosition).WhereZ(0);
    }
}
