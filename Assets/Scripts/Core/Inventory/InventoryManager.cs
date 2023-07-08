using AnttiStarterKit.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum FoodType
{

}

public class Food : MonoBehaviour
{

}

public class InventoryManager : Manager<InventoryManager>
{
    
    public int width;
    public int height;
    public FoodType[] foodArray;
    public List<Food> foodInInventory;

    void Start()
    {
        
    }

    void Update()
    {
        // 测试用代码
        //if(Input.GetKeyDown(KeyCode.G))
        //{
        //    Debug.Log("GenerateFoods");
        //    GenerateFoods(5);
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    Debug.Log("DropAllFoods");
        //    DropAllFoods();
        //}

    }

    public void GenerateFoods(int generateFoodNum)
    {

        // 先随机生成不同类型的食物
        for(int i = 0; i < generateFoodNum; i++)
        {
            // 从foodArray中随机获得要生成的食物的编号
            int foodID = Random.Range(0, foodArray.Length - 1);
            FoodType foodType = foodArray[foodID]; // 根据编号确定食物

            // TODO: 从对象池中获取对应的食物
            Food food = new Food();

            // 将物体添加入当前包裹的食物列表
            foodInInventory.Add(food);
        }
        // 重新排布所有的食物
        ReArrangeAllFoods();
    }

    public void ReArrangeAllFoods()
    {
        for(int i = 0; i < foodInInventory.Count; i++)
        {
            Food food = foodInInventory[i];
            // 在范围内随机生成食物的位置
            // TODO: 有可能会超过背包范围, 需要重新排布
            Vector2 foodPosition = new Vector2(Random.Range(0.0f, (float)width), Random.Range(0.0f, (float)height)); // TODO: 有可能会超出边界

            // 设置食物的位置
            food.transform.position = foodPosition;

            // 将食物的父物体设置为背包
            food.transform.parent = Instance.transform;
        }
    }

    public void DropAllFoods()
    {
        for (int i = 0; i < foodInInventory.Count; i++)
        {
            // 删除食物
            Destroy(foodInInventory[i].gameObject);
        }
        foodInInventory.Clear();
    }

    public void FoodLeave(Food food)
    {
        if(food != null && foodInInventory.Contains(food))
        {
            foodInInventory.Remove(food);
        }
    }
}
