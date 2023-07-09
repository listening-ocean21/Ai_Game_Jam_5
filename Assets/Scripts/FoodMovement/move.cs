using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class move : MonoBehaviour
{
    float offset = 50;
    
    void Update()
    {
        float width = GetComponent<RectTransform>().rect.size.x;
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        float z = this.transform.position.z;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            transform.DOMove(new Vector3(x - offset, y, z), 0.1f);
            if(x + width / 2 < 200)
            {
                Destroy(this);
                Debug.Log("Destroy");
            }
            Debug.Log(width.ToString());

        }
        
    }
}
