using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FoodType
{

}
/// <summary>
/// �����õ�Food��, ��������ɾ��
/// </summary>
public class Food : MonoBehaviour
{
    public int width;
    public int height;
    public const int gridElementSize = 100;
    private void Awake()
    {
        width = Random.Range(1, 5) * gridElementSize;
        height = Random.Range(1, 5) * gridElementSize;
        RectTransform foodRectTransform = gameObject.GetComponent<RectTransform>();
        foodRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        foodRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }
}