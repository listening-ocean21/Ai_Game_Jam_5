using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FoodType
{

}
/// <summary>
/// 测试用的Food类, 如果报错可删除
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